using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class NewBlockUnlockController : Singleton<NewBlockUnlockController>
{
    // text rewardcurr
    // text scoreCliam
    public Button btnClaimADS;
    public Button btnClaim;
    public TMP_Text TextDiamondReward;
    public TMP_Text TextScore;
    //   public GameObject PanelClaimADS;
    //   public PanelClaimADS panelClaimADS;
    // Start is called before the first frame update
    void OnEnable()
    {
        // Mỗi khi panel được kích hoạt, gọi StartSpin()
     //   ShowPanelSpinWheel();
    //    SpinWheel.Instance.caculatorDistance();
      //  SpinWheel.Instance.StartSpin();
    }
    void Start()
    {
     //   PanelClaimADS.GetComponent<PanelClaimADS>();
     //   PanelClaimADS.Instance.panelClaimReward.SetActive(false);
       // ShowPanelSpinWheel(); 
      //  SpinWheel.Instance.caculatorDistance();     
        btnClaim.onClick.AddListener(OnButtonClaimClick);
        btnClaimADS.onClick.AddListener(OnButtonClaimADSClick);
    }

    public void ShowPanelSpinWheel()
    {
        btnClaimADS.interactable = true;
        btnClaim.interactable = true;
       // SpinWheel.Instance.StartSpin();
        TextDiamondReward.SetText(GameController.Instance.DiamondRewardUnLockNewBlock.ToString());
    }
    // Update is called once per frame
    void Update()
    {
      //  TextScore.SetText(.To);
    }

    void OnButtonClaimClick()
    {
        Debug.Log("OnClickClaim");
        SpinWheel.Instance.Arrow.DOKill();
        DrawLine.Instance.scoreDiamond += GameController.Instance.DiamondRewardUnLockNewBlock;
        DataManager.Instance.UserData.Diamond = DrawLine.Instance.scoreDiamond;
        // set text 
        DrawLine.Instance.textscoreDiamond.SetText(DrawLine.Instance.scoreDiamond.ToString());
        btnClaimADS.interactable = false;
        btnClaim.interactable = false;
        // can call panel reward
        ShowPanelUnLockBlock.Instance.PanelUnlockBlock.SetActive(false);
        ShowPanelUnLockBlock.Instance.indexMain -= 1;


    }
    void OnButtonClaimADSClick()
    {
       // SpinWheel.Instance.StopArrow = true;
    //   TextScore.SetText(SpinWheel.Instance.CheckResult().ToString());
        // check result, up date text
       Debug.Log("OnClickClaimADS");
      //  Debug.Log("Value Score from Check ReSult = " + SpinWheel.Instance.CheckResult());

       Debug.Log("ScoreDiamond before ClainADS =" + DrawLine.Instance.scoreDiamond);

        //  DrawLine.Instance.scoreDiamond += GameController.Instance.DiamondRewardUnLockNewBlock * SpinWheel.Instance.vlCheckResult;
        //    DrawLine.Instance.scoreDiamond += GameController.Instance.DiamondRewardUnLockNewBlock * SpinWheel.Instance.CheckResult();
        // delay call 1 s show panel Reward 
        //
        SpinWheel.Instance.Arrow.DOPause();
        SpinWheel.Instance.Arrow.DOKill();
        int result = SpinWheel.Instance.CheckResult();
        Debug.Log("value check result" + result);
        int ScoreClaim = GameController.Instance.DiamondRewardUnLockNewBlock * result;
        DrawLine.Instance.scoreDiamond += GameController.Instance.DiamondRewardUnLockNewBlock * result;
        DataManager.Instance.UserData.Diamond = DrawLine.Instance.scoreDiamond;
        // set text 
        DrawLine.Instance.textscoreDiamond.SetText(DrawLine.Instance.scoreDiamond.ToString());
        //
        Debug.Log("ScoreDiamond after ClainADS =" + DrawLine.Instance.scoreDiamond);
        btnClaimADS.interactable = false;
        btnClaim.interactable = false;
        PanelClaimADS.Instance.panelClaimReward.SetActive(true);
        ShowPanelUnLockBlock.Instance.indexMain += 1;
        //panelClaimADS.SetTextClaimADS(ScoreClaim);
        // PanelClaimADS.GetComponent<PanelClaimADS>().SetTextClaimADS(ScoreClaim);     
        PanelClaimADS.Instance.SetTextClaimADS(ScoreClaim);
    }
}
