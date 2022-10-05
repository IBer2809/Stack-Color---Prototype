using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentLvl { get; private set; }

    public int NextLvl { get; private set; }


    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject finishPosition;


    public static event Action<int> CurrentLvlUpdated = delegate { };
    public static event Action<int> NextLvlUpdated = delegate { };

    private const string CURRENTLVL = "CURRENTLVL";
    private const string NEXTLVL = "NEXTLVL";

    void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Reset();

    }

    public void Reset()
    {
        CurrentLvl = PlayerPrefs.GetInt(CURRENTLVL, 0);
        NextLvl = PlayerPrefs.GetInt(NEXTLVL, 1);



    }

    public void UpdateLvl()
    {
        CurrentLvl++;
        NextLvl++;
        PlayerPrefs.SetInt(CURRENTLVL, CurrentLvl);
        PlayerPrefs.SetInt(NEXTLVL, NextLvl);
        CurrentLvlUpdated(CurrentLvl);
        NextLvlUpdated(NextLvl);
    }


    public float LvlFill()
    {
        return 1 - Vector3.Distance(player.transform.position, finishPosition.transform.position) / 100f;
    }


}

