using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class ColorFade : MonoBehaviour
{
    [SerializeField] Color color;
    // Start is called before the first frame update
    void Start()
    {
        Color tempColor = color;
        tempColor.a = 0.5f;
        Renderer renderer = transform.GetChild(0).GetComponent<Renderer>();
        renderer.material.SetColor("_Color", tempColor);
    }

   
    
    public Color GetColor()
    {
        return color;
    }

   
    
}
