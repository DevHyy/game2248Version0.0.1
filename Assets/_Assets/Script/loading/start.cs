using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using NiobiumStudios;

public class start : Singleton<start> {
  // Start is called before the first frame update
  public Button btHighestBlock;

  public Button btStart;

  //public Button backPanelReward;
  public Button confirmPanelReward;

  public TMP_Text highestBlockStart;
  public TMP_Text textScore;
  public TMP_Text textScoreDiamond;
  public Button bt1;
  public Button btnAccount;
  public Button btnSettings;
  public GameObject Panel1;
  public GameObject Panel2;
  public GameObject Panel3;
  public GameObject Panelstart;
  public GameObject PanelUnit;
  public GameObject PanelReward;
  public GameObject PanelSettingLoading;
  public Sprite volumOn;
  public Sprite volumOff;

  public Button btnfreeReward;
  public Button btndailyReward;

  public Text ScoreDiamond;

  public GameObject recompensadiariaPANEL;

  // public GameObject PanelRewardAccessstoreParent;
  void Start() {
    Panelstart.SetActive(true);
    Panel1.SetActive(false);
    Panel2.SetActive(false);
    Panel3.SetActive(false);
    PanelUnit.SetActive(false);
    PanelReward.SetActive(false);
    PanelSettingLoading.SetActive(false);
    recompensadiariaPANEL.SetActive(false);
    //DaillyRewardController.Instance.dailyRewardsInterface.canvas.gameObject.SetActive(!DataManager.Instance.UserData.Tutorial);     
    //  PanelRewardAccessstoreParent.SetActive(false);
    if (DataManager.Instance.UserData.Tutorial) {
      if (DataManager.Instance.UserData.HighestBlock.valueBlockData == 128) {
        Debug.Log("Call tutorial");
        Panel1.SetActive(true);
        Panelstart.SetActive(false);
        //  PanelRewardAccessstoreParent.SetActive(false);
        //  PanelRewardAccessstore.SetActive(false);
      }
      else {
        //  Debug.Log("Dont Call Tutorial Show Reward ");
        //  PanelRewardAccessstore.SetActive(true);
      }
      //DataManager.Instance.UserData.Tutorial = false;       
    }
    else {
      // set if chua claim thif show , if done claim false
      // PanelRewardAccessstoreParent.SetActive(true);
    }

    btStart.onClick.AddListener(OnButtonStartClick);
    bt1.onClick.AddListener(OnButtonbt1Click);
    btnAccount.onClick.AddListener(OnButtonbt2Click);
    btnSettings.onClick.AddListener(OnButtonbt3Click);
    btnfreeReward.onClick.AddListener(OnButtonfreeRewardClick);
    btndailyReward.onClick.AddListener(OnButtondailyRewardClick);
    // backPanelReward.onClick.AddListener(OnButtonBackRewardClick);
    confirmPanelReward.onClick.AddListener(OnButtonconfirmPanelRewardClick);
    if (DataManager.Instance.UserData == null) {
      textScore.SetText("0");
      textScoreDiamond.SetText("0");
    }
    else {
      // show text equal data
      textScore.SetText(Math.Round(DataManager.Instance.UserData.BestScoreTotal).ToString() + DataManager.Instance
        .ListUnitBlock[DataManager.Instance.UserData.UnitBestScoreTotal].nameUnit);
      textScoreDiamond.SetText(DataManager.Instance.UserData.Diamond.ToString());
    }

    // create button set clolor set text set value
    if (DataManager.Instance.UserData.HighestBlock != null) {
      Debug.Log("DATA HIGHESTBLOCK IS NOT NULL");
      highestBlockStart.SetText(Math.Floor(DataManager.Instance.UserData.HighestBlock.valueBlockData).ToString() +
                                DataManager.Instance
                                  .ListUnitBlock[DataManager.Instance.UserData.HighestBlock.idUnitBlockData].nameUnit);
      //  btHighestBlock.image.color = new Color((((float)Math.Floor(DataManager.Instance.UserData.HighestBlock.valueBlockData) * 1043) % 16383) / 16383, ((((float)DataManager.Instance.UserData.HighestBlock.idUnitBlockData * 1053) % 16383) + 2567) / 16383, ((((float)Math.Floor(DataManager.Instance.UserData.HighestBlock.valueBlockData) * 143) + 457) % 16383) / 16383, 1);
      btHighestBlock.image.color =
        DataManager.Instance.ListColorBlock[DataManager.Instance.UserData.HighestBlock.idColorData % 20];
      if (DataManager.Instance.UserData.HighestBlock.idColorData % 20 == 3 ||
          DataManager.Instance.UserData.HighestBlock.idColorData % 20 == 4 ||
          DataManager.Instance.UserData.HighestBlock.idColorData % 20 == 11 ||
          DataManager.Instance.UserData.HighestBlock.idColorData % 20 == 15) {
        highestBlockStart.color = Color.black;
      }
      else {
        highestBlockStart.color = Color.white;
      }
    }
    else {
      Debug.Log("DATA HIGHESTBLOCK IS NULLLLL");
    }
    // textScore.SetText(Math.Floor(DataManager.Instance.UserData.HighestBlock.valueBlockData).ToString() + DataManager.Instance.ListUnitBlock[DataManager.Instance.UserData.HighestBlock.idUnitBlockData].nameUnit);
  }

  // Update is called once per frame
  void Update() {
  }


  public void OnButtonStartClick() {
    //  LoadScene();
    LoadLevel();
  }

  private void LoadLevel() {
    SceneManager.LoadScene("GamePlayScene");
  }

  /*
   public void OnButtonbt3Click()
   {
       Debug.Log("Click On button 3");
       //  ResetGame();
       if (DataManager.Instance.UserData.IsSoundTurnOn)
       {           
           DataManager.Instance.UserData.IsSoundTurnOn = false;
           bt3.image.sprite = volumOff;
       }
       else
       {
           DataManager.Instance.UserData.IsSoundTurnOn = true;
           bt3.image.sprite = volumOn;
       }
   }*/
  public void OnButtonbt3Click() {
    Debug.Log("Click On button Setting");
    PanelSettingLoading.SetActive(true);
  }

  public void OnButtonbt1Click() {
    Debug.Log("Click On button 1");
  }

  public void OnButtonbt2Click() {
    Debug.Log("Click On button Account");
    PanelUnit.SetActive(true);
  }

  public void ResetGame() {
    Debug.Log("RESET GAME");
    //  PlayerPrefs.DeleteAll();

    // Load the sceneLoading
    // SceneManager.LoadScene("Loading");
  }

  public void OnButtonfreeRewardClick() {
    if (AdsManager.Instance.CheckisInterstitialAd() == true) {
      Debug.Log("Reward 160 Diamond");
      // DrawLine.Instance.scoreDiamond += 160 ;
      // DataManager.Instance.UserData.Diamond = DrawLine.Instance.scoreDiamond;
      DataManager.Instance.UserData.Diamond += 160;
      textScoreDiamond.SetText(DataManager.Instance.UserData.Diamond.ToString());
      PanelReward.SetActive(true);
      ScoreDiamond.text = "160";
    }
    else {
      Debug.Log("Interstitial Ad is not ready");
    }
  }
  //public void OnButtonBackRewardClick()
  // {
  //    PanelReward.SetActive(false);

  //  }
  public void OnButtonconfirmPanelRewardClick() {
    UITransitionPanel uitransition = PanelReward.GetComponentInChildren<UITransitionPanel>();
    if (uitransition != null) {
      uitransition.OnCloseClick();
    }
  }

  public void OnButtondailyRewardClick() {
    //  recompensadiariaPANEL.SetActive(true);
    DaillyRewardController.Instance.dailyRewardsInterface.canvas.gameObject.SetActive(true);
  }
}