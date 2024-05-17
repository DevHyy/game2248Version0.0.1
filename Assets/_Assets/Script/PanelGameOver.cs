using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelGameOver : Singleton<PanelGameOver>
{
    public GameObject Panelgameover;
    public GameObject PanelRestartGame;
    public Button swapBlock;
    public Button breakBlock;
    public Button closePanelgameover;
    public Button btnNoThank;
    public TMP_Text TextSwapBonusDiamond;
    public TMP_Text TextBreakBonusDiamond;
    public TMP_Text TextScoreDiamond;
    public void Start()
    {

        TextSwapBonusDiamond.SetText(DataManager.Instance.UserData.BonusDiamond.ToString());
        TextBreakBonusDiamond.SetText(DataManager.Instance.UserData.BonusDiamond.ToString());
        TextScoreDiamond.SetText(DataManager.Instance.UserData.Diamond.ToString());
        Panelgameover.SetActive(false);
        PanelRestartGame.SetActive(false);
        swapBlock.onClick.AddListener(OnButtonswapBlockClick);
        breakBlock.onClick.AddListener(OnButtonbreakBlockClick);
        closePanelgameover.onClick.AddListener(OnButtonClosePanelGameOverClick);
        btnNoThank.onClick.AddListener(OnButtonNoThankClick);
    }
    public void OnButtonswapBlockClick()
    {
        if (DataManager.Instance.UserData.Diamond >= DataManager.Instance.UserData.BonusDiamond)
        {
            FunctionBonus.Instance.canSwap = true;
            FunctionBonus.Instance.canBreak = false;       
            Panelgameover.SetActive(false);
            ShowPanelUnLockBlock.Instance.indexMain -= 1;

            FunctionBonus.Instance.SwapBlock.SetActive(true);

            FunctionBonus.Instance.SetActiveBonusOn();        
        }
    }
    public void OnButtonbreakBlockClick()
    {
        if (DataManager.Instance.UserData.Diamond >= DataManager.Instance.UserData.BonusDiamond)
        {
                FunctionBonus.Instance.canBreak = true;
                FunctionBonus.Instance.canSwap = false;
                Panelgameover.SetActive(false);
                ShowPanelUnLockBlock.Instance.indexMain -= 1;
                FunctionBonus.Instance.Hammer.SetActive(true);
                FunctionBonus.Instance.IconHammer.transform.position = FunctionBonus.Instance.Hammerst.position;

            FunctionBonus.Instance.SetActiveBonusOn();
        }
        }
    public void OnButtonClosePanelGameOverClick()
    {
        Debug.Log("CLICK CLOSE PANELGAMEOVER ");
        ShowPanelUnLockBlock.Instance.indexMain += 1;
        PanelRestartGame.SetActive(true);
    }
    public void OnButtonNoThankClick()
    {
        Debug.Log("CLICK NoThank ");
        ShowPanelUnLockBlock.Instance.indexMain += 1;
        PanelRestartGame.SetActive(true);
    }
}
