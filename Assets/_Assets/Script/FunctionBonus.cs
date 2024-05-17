using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FunctionBonus : Singleton<FunctionBonus>
{
	// Start is called before the first frame update
	public Button ButtonSwap;
	public Button ButtonBreak;
	public Button ButtonSetting;
	public bool canSwap;
	public bool canBreak;
	public GameObject PanelSetting;
	public TMP_Text TextSwapBonusDiamond;
	public TMP_Text TextBreakBonusDiamond;
	public GameObject ScoreDiamond;
	public GameObject Score;
	//	public GameObject TopBlock;
	public GameObject TopScore;
	public GameObject ObjSwap;
	public GameObject ObjBreak;
	public GameObject ObjSetting;
	public GameObject ObjFreeReward;
	public Button btnBackBonus;
	public GameObject Hammer;
	public GameObject IconHammer;
	public GameObject IconHammerAnim;

	public Transform Hammerst;
	public GameObject SwapBlock;
	public GameObject BlockRed;
	public GameObject BlockYellow;
	public Transform transformPoint_1;
	public Transform transformPoint_2;
	public int statusSwap; 
	public float moveTime;
	public float timer;

	void Start()
	{
		moveTime = 0.5f;
		timer = 2f;
		btnBackBonus.gameObject.SetActive(false);
		TextSwapBonusDiamond.SetText(DataManager.Instance.UserData.BonusDiamond.ToString());
		TextBreakBonusDiamond.SetText(DataManager.Instance.UserData.BonusDiamond.ToString());
		canSwap = false;
		canBreak = false;
		PanelSetting.SetActive(false);
		//Button btnSwap = ButtonSwap.GetComponent<Button>();
		ButtonSwap.onClick.AddListener(ButtonSwapOnClick);
		ButtonBreak.onClick.AddListener(ButtonBreakOnClick);
		ButtonSetting.onClick.AddListener(ButtonSettingOnClick);
		btnBackBonus.onClick.AddListener(ButtonBackOnClick);
		Hammer.SetActive(false);
		SwapBlock.SetActive(false);
		// get transform start for 2 icon block swap
	    statusSwap = 1;
		BlockRed.transform.position = transformPoint_1.position;
		BlockYellow.transform.position = transformPoint_2.position;	
	}
	void ButtonBackOnClick()
	{
		if (canSwap)
		{
			canSwap = false;
			DrawLine.Instance.numberClickToSwap = 0;
			SwapBlock.SetActive(false);
			if (DrawLine.Instance.blockSwap1 != null)
			{
				DrawLine.Instance.blockSwap1.transform.DOScale(1f, 0.2f);
			}
			if (DrawLine.Instance.blockSwap2 != null)
			{
				DrawLine.Instance.blockSwap2.transform.DOScale(1f, 0.2f);
			}
			//   false icon swap
		}
		if (canBreak)
		{
			canBreak = false;
			Hammer.SetActive(false);
		}
		SetActiveBonusOff();
		GameController.Instance.checkEnd = true;
	}
	void ButtonSwapOnClick()
	{
		Debug.Log("Clickon button Swap!");
		if (canSwap)
		{
			canSwap = false;
			DrawLine.Instance.numberClickToSwap = 0;
			if (DrawLine.Instance.blockSwap1 != null)
			{
				DrawLine.Instance.blockSwap1.transform.DOScale(1f, 0.2f);
			}
			if (DrawLine.Instance.blockSwap2 != null)
			{
				DrawLine.Instance.blockSwap2.transform.DOScale(1f, 0.2f);
			}
		}
		else
		{
			canSwap = true;
			canBreak = false;

			if (DrawLine.Instance.scoreDiamond < DataManager.Instance.UserData.BonusDiamond)
			{
				PanelRewardFree.Instance.PanelReward.SetActive(true);
				ShowPanelUnLockBlock.Instance.indexMain += 1;
			}
			else
			{
				SwapBlock.SetActive(true);
				SetActiveBonusOn();
			}
		}
	}

	void ButtonBreakOnClick()
	{
		Debug.Log("Clickon button Break!");
		if (canBreak)
		{
			canBreak = false;
		}
		else
		{
			canBreak = true;
			canSwap = false;
			DrawLine.Instance.numberClickToSwap = 0;
			if (DrawLine.Instance.scoreDiamond < DataManager.Instance.UserData.BonusDiamond)
			{
				PanelRewardFree.Instance.PanelReward.SetActive(true);
				ShowPanelUnLockBlock.Instance.indexMain += 1;
			}
			else
			{
				Hammer.SetActive(true);
				//	Hammer.transform.position = Hammerst.position;
				IconHammer.transform.position = Hammerst.position;
				SetActiveBonusOn();
			}

		}
	}
	public void SetActiveBonusOn()
	{
		btnBackBonus.gameObject.SetActive(true);
		ScoreDiamond.SetActive(false);
		Score.SetActive(false);
		TopScore.SetActive(false);
		ObjSwap.SetActive(false);
		ObjBreak.SetActive(false);
		ObjSetting.SetActive(false);
		ObjFreeReward.SetActive(false);
	}
	public void SetActiveBonusOff()
	{
		ScoreDiamond.SetActive(true);
		Score.SetActive(true);
		TopScore.SetActive(true);
		ObjSwap.SetActive(true);
		ObjBreak.SetActive(true);
		ObjSetting.SetActive(true);
		ObjFreeReward.SetActive(true);
		btnBackBonus.gameObject.SetActive(false);
	}
	void ButtonSettingOnClick()
	{
		DOVirtual.DelayedCall(0.2f, () =>
		 {
			 ShowPanelUnLockBlock.Instance.indexMain += 1;
			 PanelSetting.SetActive(true);
			 // set renderline false
		 });
	}
	// Update is called once per frame
	void Update()
	{
		// if gameobject  SwapBlock true  call funciton domover 2 block
		moveTime -= Time.deltaTime;
		if (moveTime >= 0) return;
		moveTime = timer;
		if (SwapBlock.activeSelf)
        {
         swapiconBlock();
        }
		
	}
	// need set add icon (Bonus) in footted pannel 
	public void swapiconBlock()
	{
		if (statusSwap == 1)
		{
			BlockRed.transform.DOMove(transformPoint_2.position, 0.5f).SetEase(Ease.InOutQuad);
			BlockYellow.transform.DOMove(transformPoint_1.position, 0.5f).SetEase(Ease.InOutQuad);
			statusSwap = 0;
		}
	    else if (statusSwap == 0)
		{
			BlockRed.transform.DOMove(transformPoint_1.position, 0.5f).SetEase(Ease.InOutQuad);
			BlockYellow.transform.DOMove(transformPoint_2.position, 0.5f).SetEase(Ease.InOutQuad);
			statusSwap = 1;
		}
		else 
		{
			statusSwap = 1;
		}
    }
}
