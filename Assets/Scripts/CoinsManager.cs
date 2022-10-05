using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance { get; private set; }

    public int PermanentCoins { get; private set; }

    public int Coins { get; private set; }

    public int CoinsPerGame { get; private set; }

    public int CostUpCash { get; private set; }

    public int CostUpPower { get; private set; }

    public int LvlCash { get; private set; }

    public int LvlPower { get; private set; }



    public static event Action<int> CoinsPerGameUpdated = delegate { };
    public static event Action<int> PermanentCoinsUpdated = delegate { };
    public static event Action<int> CoinsUpdated = delegate { };

    public static event Action<int> CashUpUpdated = delegate { };
    public static event Action<int> PowerUpUpdated = delegate { };

    public static event Action<int> LvlCashUpdated = delegate { };
    public static event Action<int> LvlPowerUpdated = delegate { };

    private const string COINSPERGAME = "COINSPERGAME";
    private const string PERMANENTCOINS = "PERMANENTCOINS";
    private const string COINS = "COINS";

    private const string COSTUPCASH = "COSTUPCASH";
    private const string COSTUPPOWER = "COSTUPPOWER";

    private const string LVLCASH = "LVLCASH";
    private const string LVLPOWER = "LVLPOWER";



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
        CoinsPerGame = PlayerPrefs.GetInt(COINSPERGAME, 0);
        PermanentCoins = PlayerPrefs.GetInt(PERMANENTCOINS, 1);
        Coins = PlayerPrefs.GetInt(COINS, 0);
        CostUpCash = PlayerPrefs.GetInt(COSTUPCASH, 200);
        CostUpPower = PlayerPrefs.GetInt(COSTUPPOWER, 200);
        LvlCash = PlayerPrefs.GetInt(LVLCASH, 0);
        LvlPower = PlayerPrefs.GetInt(LVLPOWER, 0);
    }

    public void AddCoinsPermanent()
    {
        CoinsPerGame += PermanentCoins;
        // Fire event
        CoinsPerGameUpdated(CoinsPerGame);
    }


    public void AddPermanentCoinsToAllCash()
    {
        Coins += CoinsPerGame;
        PlayerPrefs.SetInt(COINS, Coins);
        Debug.Log(Coins);
        CoinsUpdated(Coins);

    }

    public void BuyPowerUp()
    {
        if (Coins < CostUpPower)
        {
            //playercontroller add force
        }
        else
        {
            Coins -= CostUpPower;
            CostUpPower += 52;
            LvlPower++;
            PlayerPrefs.SetInt(LVLPOWER, LvlPower);
            PlayerPrefs.SetInt(COINS, Coins);
            PlayerPrefs.SetInt(COSTUPPOWER, CostUpPower);

        }


        CoinsUpdated(Coins);
        PowerUpUpdated(CostUpPower);

    }

    public void BuySpeedUp()
    {
        if (Coins < CostUpCash)
        {

        }
        else
        {
            PermanentCoins += 1;
            Coins -= CostUpCash;
            CostUpCash += 52;
            LvlCash++;
            PlayerPrefs.SetInt(PERMANENTCOINS, PermanentCoins);
            PlayerPrefs.SetInt(LVLCASH, LvlCash);
            PlayerPrefs.SetInt(COINS, Coins);
            PlayerPrefs.SetInt(COSTUPCASH, CostUpCash);
        }

        PermanentCoinsUpdated(PermanentCoins);
        CoinsUpdated(Coins);
        CashUpUpdated(CostUpCash);
    }

    public void BuyResultUp()
    {

    }
}
