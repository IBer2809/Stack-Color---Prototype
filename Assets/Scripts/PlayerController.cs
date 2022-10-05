using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] Renderer[] renderers;

    [SerializeField] bool isPlaying;
    [SerializeField] float speed;
    [SerializeField] float sideLerpSpeed;
    private Rigidbody rigidbody;

    [SerializeField] Transform stackPosition;
    private Transform parentTransform;
    [SerializeField] private Transform tileStack;

    private List<Transform> stackElements;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        color = gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.Playing)
        {
            MoveStraight();
        }
        if (Input.GetMouseButton(0))
        {
            MoveSide();
        }
    }

    Color GetColor()
    {
        return color;
    }
    void SetColor(Color settedColor)
    {
        color = settedColor;
        color.a = 1f;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetColor("_Color", color);
        }

        for(int i = 0;i<stackPosition.childCount;i++)
        {
            stackPosition.GetChild(i).GetComponent<Renderer>().material.color = settedColor;
        }
    }

    void MoveStraight()
    {
        rigidbody.velocity = Vector3.forward * speed;
    }

    void MoveSide()
    {

        transform.position = Vector3.Lerp(transform.position, new Vector3(SimpleInput.GetAxis("Horizontal"), transform.position.y, transform.position.z), sideLerpSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ColorFade")
        {
            SetColor(other.GetComponent<ColorFade>().GetColor());
            UIManager.Instance.SetFillColor();
        }

        if (other.tag == "Finish")
        {
            GameManager.Instance.Finish();
        }

        if (other.tag == "Tile")
        {
            
            color = gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color;
            Transform otherTransform = other.transform;
            if (color != otherTransform.GetComponent<Renderer>().material.color)
            {

                Debug.Log(parentTransform);
                if (stackPosition != null)
                {
                    if (stackPosition.childCount > 1)
                    {
                        parentTransform.position -= Vector3.up * (stackPosition.GetChild(stackPosition.childCount - 1).localScale.y);
                        /*stackPosition.position -= Vector3.up * stackPosition.GetChild(0).localScale.y;
                        Debug.Log(stackPosition.childCount);
                        Debug.Log(stackPosition.GetChild(0).gameObject);*/
                        Destroy(stackPosition.GetChild(stackPosition.childCount - 1).gameObject);
                    }
                    else
                    {
                        Destroy(parentTransform.gameObject);
                        GameManager.Instance.GameOver();
                    }
                }
                return;
            }
            else
            {
                CoinsManager.Instance.AddCoinsPermanent();
                Rigidbody rbother = otherTransform.GetChild(0).GetComponent<Rigidbody>();
                rbother.isKinematic = true;
                other.enabled = false;

                /*  otherTransform.position = stackPosition.position - Vector3.up * (otherTransform.localScale.y);
                  stackPosition.position += Vector3.up * (otherTransform.localScale.y);
                  otherTransform.SetParent(stackPosition);*/



                if (parentTransform == null)
                {
                    parentTransform = otherTransform;
                    parentTransform.position = stackPosition.position;
                    parentTransform.parent = stackPosition;
                }
                else
                {
                    parentTransform.position += Vector3.up * (otherTransform.localScale.y);
                    otherTransform.position = parentTransform.position - Vector3.up * (otherTransform.localScale.y);
                    otherTransform.parent = stackPosition;
                }
            }
        }
    }
   
}
