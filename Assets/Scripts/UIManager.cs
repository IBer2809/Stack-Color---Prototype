using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Canvases")]
    public GameObject menuCanvas;
    public GameObject playingCanvas;
    public GameObject gameOverCanvas;
    public GameObject finishCanvas;


    public Text coinsText;
    public Text coinsPerGameText;
    public Text gameOverCoinsText;


    public FillColorSetup currentLvlFill;



    [Header("MainCanvasPanel")]
    public GameObject panelMenu;
    public GameObject panelSettings;


    public GameObject soundBtn;
    public GameObject vibrationBtn;
    private bool isMutedVolume = false;
    private bool isMutedVibration = false;


    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        GameManager.GameStateChanged += GameManager_GameStateChanged;

    }

    private void Start()
    {
        ShowMenuUI();
      
    }

    void OnDisable()
    {
        GameManager.GameStateChanged -= GameManager_GameStateChanged;
    }

   
    private void Update()
    {
        UpdateTextAndLvlFill();
    }

    private void UpdateTextAndLvlFill()
    {
        currentLvlFill.SetFill();
        coinsText.text = CoinsManager.Instance.Coins.ToString();
        coinsPerGameText.text = CoinsManager.Instance.CoinsPerGame.ToString();
        gameOverCoinsText.text = "+ " + CoinsManager.Instance.CoinsPerGame.ToString();
        /* powerUpText.text = CashManager.Instance.CostUpPower.ToString();
         cashUpText.text = CashManager.Instance.CostUpCash.ToString();
         lvlCashText.text = CashManager.Instance.LvlCash.ToString();
         lvlPowerText.text = CashManager.Instance.LvlPower.ToString();*/
    }

    void GameManager_GameStateChanged(GameState newState, GameState oldState)
    {
        Debug.Log(newState);
        if (newState == GameState.Menu)
        {
            ShowMenuUI();
        }
        else if (newState == GameState.Playing)
        {
            ShowPlayingUI();
        }
        else if (newState == GameState.GameOver)
        {
            ShowGameOverUI();
        }
        else if (newState == GameState.Finish)
        {
            ShowFinishUI();
        }
    }

    private void ShowMenuUI()
    {
        menuCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void ShowSettiings()
    {
        panelMenu.SetActive(false);
        panelSettings.SetActive(true);
    }

    public void HideSettiings()
    {
        panelSettings.SetActive(false);
        panelMenu.SetActive(true);
    }

    private void ShowPlayingUI()
    {
        menuCanvas.GetComponent<Canvas>().enabled = false;
        playingCanvas.GetComponent<Canvas>().enabled = true;
    }

    private void ShowGameOverUI()
    {
        playingCanvas.GetComponent<Canvas>().enabled = false;
        gameOverCanvas.GetComponent<Canvas>().enabled = true;
    }

    private void ShowFinishUI()
    {
        playingCanvas.GetComponent<Canvas>().enabled = false;
        finishCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

   

    public void RestartLevel()
    {
        GameManager.Instance.Restart();
    }

    /* public void NextLevel()
     {
         GameManager.Instance.NextLevel();
     }*/



    public void VibrationsOnOff()
    {

        //add vibration
        if (isMutedVibration)
        {
            vibrationBtn.transform.GetChild(0).gameObject.SetActive(false);
            vibrationBtn.transform.GetChild(1).gameObject.SetActive(true);
            isMutedVibration = !isMutedVibration;
        }
        else
        {
            vibrationBtn.transform.GetChild(0).gameObject.SetActive(true);
            vibrationBtn.transform.GetChild(1).gameObject.SetActive(false);
            isMutedVibration = !isMutedVibration;
        }
    }

    public void VolumeOnOff()
    {

        if (isMutedVolume)
        {
            soundBtn.transform.GetChild(0).gameObject.SetActive(false);
            soundBtn.transform.GetChild(1).gameObject.SetActive(true);
            isMutedVolume = !isMutedVolume;
            AudioListener.volume = 0f;

        }
        else
        {
            soundBtn.transform.GetChild(0).gameObject.SetActive(true);
            soundBtn.transform.GetChild(1).gameObject.SetActive(false);
            AudioListener.volume = 1f;
            isMutedVolume = !isMutedVolume;

        }
    }


    public void SetFillColor()
    {
        currentLvlFill.SetColor();
    }
}
