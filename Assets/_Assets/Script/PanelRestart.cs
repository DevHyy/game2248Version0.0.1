using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class PanelRestart : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnYes;
    public Button btnNo;
    public Button Close;
    void Start()
    {
        btnYes.onClick.AddListener(OnButtonYesClick);
        btnNo.onClick.AddListener(OnButtonNoClick);
        Close.onClick.AddListener(OnButtonCloseClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnButtonYesClick()
    {
        // call function restart Game
        ShowPanelUnLockBlock.Instance.indexMain -= 2;
        PanelGameOver.Instance.Panelgameover.SetActive(false);
        PanelGameOver.Instance.PanelRestartGame.SetActive(false);
        RestartGame();
    }
    void OnButtonNoClick()
    {
        ShowPanelUnLockBlock.Instance.indexMain -= 1;
        PanelGameOver.Instance.PanelRestartGame.SetActive(false);
    }
    void OnButtonCloseClick()
    {
        ShowPanelUnLockBlock.Instance.indexMain -= 1;
        PanelGameOver.Instance.PanelRestartGame.SetActive(false);
    }

    public void RestartGame()
    {
        Debug.Log("RESTART GAME");
        DataManager.Instance.UserData.canRestart = true;
        //Reset Diamond
        DataManager.Instance.UserData.Diamond = 0;
        // Reset BestScoreTotal
        DataManager.Instance.UserData.BestScoreTotal = 0;
        DataManager.Instance.UserData.UnitBestScoreTotal = 0;
        //Reset
        DataManager.Instance.UserData.BonusDiamond = 100;
        //ResetLiminated Block
        GameController.Instance.liminatedBlock.naturalPart = 1;
        GameController.Instance.liminatedBlock.valueBlock = 1;
        GameController.Instance.liminatedBlock.idUnitBlock = 0;
        GameController.Instance.liminatedBlock.idColor = -1;

        //GameController.Instance.SetColorBlock(GameController.Instance.liminatedBlock);
        GameController.Instance.liminatedBlock.name = "BlockLimited" + "1";
        GameController.Instance.liminatedBlock.text.SetText("1");
        DataManager.Instance.UserData.BlockLimited.valueBlockData = GameController.Instance.liminatedBlock.valueBlock;
        DataManager.Instance.UserData.BlockLimited.idUnitBlockData = GameController.Instance.liminatedBlock.idUnitBlock;
        DataManager.Instance.UserData.BlockLimited.idColorData = GameController.Instance.liminatedBlock.idColor;

        DrawLine.Instance.highestBlock.naturalPart = 128;
        DrawLine.Instance.highestBlock.valueBlock = 128;
        DrawLine.Instance.highestBlock.idUnitBlock = 0;
        DrawLine.Instance.highestBlock.idColor = 6;

        GameController.Instance.SetColorBlock(DrawLine.Instance.highestBlock);
        DrawLine.Instance.highestBlock.name = "BlockLimited" + "128";
        DrawLine.Instance.highestBlock.text.SetText("128");
        DataManager.Instance.UserData.HighestBlock.valueBlockData = DrawLine.Instance.highestBlock.valueBlock;
        DataManager.Instance.UserData.HighestBlock.idUnitBlockData = DrawLine.Instance.highestBlock.idUnitBlock;
        DataManager.Instance.UserData.HighestBlock.idColorData = DrawLine.Instance.highestBlock.idColor;

        // Reset List PB Instance Liminated
        ShowPanelUnLockBlock.Instance.OverrideListPB(GameController.Instance.liminatedBlock);
        // Reset BlockNewAdd
        GameController.Instance.newBlockAdd = GameController.Instance.listPbAddBlock[GameController.Instance.listPbAddBlock.Count - 1];
        DataManager.Instance.UserData.BlockNewAdd.valueBlockData = GameController.Instance.newBlockAdd.valueBlock;
        DataManager.Instance.UserData.BlockNewAdd.idUnitBlockData = GameController.Instance.newBlockAdd.idUnitBlock;
        DataManager.Instance.UserData.BlockNewAdd.idColorData = GameController.Instance.newBlockAdd.idColor;

        PanelUnit.Instance.RestartListnumberUnit();
        // reset DailyReward
        DateTime _fechaahora = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));   
        DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString("yyyy-MM-dd");
        DataManager.Instance.UserData.PlayGameCount = 0;

        //  DaillyRewardController.Instance.dailyRewardsInterface.buttonReset = true;
        DaillyRewardController.Instance.dailyRewardsInterface.funRestart();

        SceneManager.LoadScene("Loading");
    }

}
