using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PanelClaimADS : Singleton<PanelClaimADS>
{
    // public TMP_Text ScoreClaim;
    public GameObject panelClaimReward;
    public Text ScoreClaim;
    public Button btnOk;
   // public Button btnBack;
    void Start()
    {

        btnOk.onClick.AddListener(OnButtonOkClick);
       // btnBack.onClick.AddListener(OnButtonBackClick);
    }
    void Update()
    {   
        
    }
    public void SetTextClaimADS(int ScoreClaims)
    {
        //  ScoreClaim.SetText(ScoreClaims.ToString());

        ScoreClaim.text = ScoreClaims.ToString();
    }
    void OnButtonOkClick()
    {
      //  NewBlockUnlockController.Instance.PanelClaimADS.SetActive(false);
        panelClaimReward.SetActive(false);
        ShowPanelUnLockBlock.Instance.indexMain -= 1;
    }
  //  void OnButtonBackClick()
   // {
  //      NewBlockUnlockController.Instance.PanelClaimADS.SetActive(false);
   // }
}
