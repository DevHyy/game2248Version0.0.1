using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRewardFree : Singleton<PanelRewardFree> {
  public Button freeReward;
  public Button freeRewardGameOver;
  public GameObject PanelReward;
  public Button btnBackPanelReward;

  public Button btnViewAD;

  // public Text ScoreClaim;
  //  public GameObject PanelConfirmReward;
  // public Button btnBackPanelConfirmReward;
  // public Button btnConfirm;
  // Start is called before the first frame update
  void Start() {
    PanelClaimADS.Instance.panelClaimReward.SetActive(false);
    PanelReward.SetActive(false);
    freeReward.onClick.AddListener(OnButtonfreeRewardClick);
    freeRewardGameOver.onClick.AddListener(OnButtonfreeRewardGameOverClick);
    btnBackPanelReward.onClick.AddListener(OnButtonBackPanelRewardClick);
    btnViewAD.onClick.AddListener(OnButtonViewADClick);
    // btnBackPanelConfirmReward.onClick.AddListener(OnButtonBackPanelConfirmRewardClick);
    // btnConfirm.onClick.AddListener(OnButtonConfirmClick);
  }

  // Update is called once per frame
  void Update() {
  }

  void OnButtonfreeRewardClick() {
    if(AdsManager.Instance.CheckisInterstitialAd() == true) {
      //  PanelConfirmReward.SetActive(true);
      PanelClaimADS.Instance.panelClaimReward.SetActive(true);
      PanelClaimADS.Instance.SetTextClaimADS(160);
      ShowPanelUnLockBlock.Instance.indexMain += 1;
      DrawLine.Instance.scoreDiamond += 160;
      DataManager.Instance.UserData.Diamond = DrawLine.Instance.scoreDiamond;
      DrawLine.Instance.textscoreDiamond.SetText(DrawLine.Instance.scoreDiamond.ToString());
    }
    else {
      Debug.Log("Interstitial Ad is not ready yet");
    }
  }

  void OnButtonfreeRewardGameOverClick() {
    if (AdsManager.Instance.CheckisInterstitialAd() == true) {
      PanelClaimADS.Instance.panelClaimReward.SetActive(true);
      PanelClaimADS.Instance.SetTextClaimADS(160);
      ShowPanelUnLockBlock.Instance.indexMain += 1;
      DrawLine.Instance.scoreDiamond += 160;
      DataManager.Instance.UserData.Diamond = DrawLine.Instance.scoreDiamond;
      DrawLine.Instance.textscoreDiamond.SetText(DrawLine.Instance.scoreDiamond.ToString());
      PanelGameOver.Instance.TextScoreDiamond.SetText(DrawLine.Instance.scoreDiamond.ToString());
    }
    else {
      Debug.Log("Interstitial Ad is not ready yet");
    }
  }

  void OnButtonBackPanelRewardClick() {
    PanelReward.SetActive(false);
    FunctionBonus.Instance.canSwap = false;
    FunctionBonus.Instance.canBreak = false;
    ShowPanelUnLockBlock.Instance.indexMain -= 1;
  }

  void OnButtonViewADClick() {
    if(AdsManager.Instance.CheckisInterstitialAd() == true) {
      DrawLine.Instance.scoreDiamond += 160;
      DataManager.Instance.UserData.Diamond = DrawLine.Instance.scoreDiamond;
      DrawLine.Instance.textscoreDiamond.SetText(DrawLine.Instance.scoreDiamond.ToString());
      // PanelConfirmReward.SetActive(true);
      PanelClaimADS.Instance.panelClaimReward.SetActive(true);
      PanelClaimADS.Instance.SetTextClaimADS(160);
      PanelReward.SetActive(false);
      // after reward Diamond( View AD)  set bool Swap and Break false  
      FunctionBonus.Instance.canSwap = false;
      FunctionBonus.Instance.canBreak = false;
    }
    else {
      Debug.Log("Interstitial Ad is not ready yet");
    }
  }
  //  void OnButtonBackPanelConfirmRewardClick()
  // {
  //     PanelConfirmReward.SetActive(false);
  //     ShowPanelUnLockBlock.Instance.indexMain -= 1;
  // }
/*    void OnButtonConfirmClick()
    {
        //PanelConfirmReward.SetActive(false);
        PanelClaimADS.Instance.panelClaimReward.SetActive(false);
  
        ShowPanelUnLockBlock.Instance.indexMain -= 1;
    }
*/
}