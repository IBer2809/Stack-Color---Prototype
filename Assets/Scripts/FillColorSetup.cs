using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillColorSetup : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private Image fill;
    void Start()
    {
        fill = gameObject.transform.GetComponent<Image>();
        SetColor();
    }

    public void SetColor()
    {
        fill.color = player.transform.GetChild(0).GetComponent<Renderer>().material.color;
    }

    public void SetFill()
    {
        fill.fillAmount = ScoreManager.Instance.LvlFill();
    }


}
