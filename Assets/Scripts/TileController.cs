using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{

    [SerializeField] Color color;

    private void Start()
    {
        color = gameObject.transform.GetComponent<Renderer>().material.color;
    }
    public Color GetColor()
    {
        return color;
    }

}
