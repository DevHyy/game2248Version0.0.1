
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiobiumStudios;
public class DaillyRewardController : Singleton<DaillyRewardController>
{
    public DailyRewardsInterface dailyRewardsInterface;

    void OnEnable()
    {
        DailyRewards.GetInstance(0).onClaimPrize += OnClaimPrizeDailyRewards;
    }

    void OnDisable()
    {
        DailyRewards.GetInstance(0).onClaimPrize -= OnClaimPrizeDailyRewards;
    }

    // this is your integration function. Can be on Start or simply a function to be called
    public void OnClaimPrizeDailyRewards(int day)
    {
        //This returns a Reward object
        Reward myReward = DailyRewards.GetInstance().GetReward(day);

        // And you can access any property
        print(myReward.unit);   // This is your reward Unit name
        print(myReward.reward); // This is your reward count

      // var rewardsCount = PlayerPrefs.GetInt("MY_REWARD_KEY", 0);
        //rewardsCount += myReward.reward;

      //  PlayerPrefs.SetInt("MY_REWARD_KEY", rewardsCount);
       // PlayerPrefs.Save();
        DataManager.Instance.UserData.Diamond += myReward.reward;
        start.Instance.textScoreDiamond.SetText(DataManager.Instance.UserData.Diamond.ToString());


    }
}


