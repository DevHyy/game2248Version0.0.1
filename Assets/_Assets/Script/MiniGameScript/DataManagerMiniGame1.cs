using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataManagerMiniGame1 : Singleton<DataManagerMiniGame1>
{
    //
    // public List<Unit> ListUnitBlock;
    // public List<Color> ListColorBlock = new List<Color>(20);//20 element inputEditor
    public List<LevelData> Levels;
    public UserDataMiniGame1 UserDataMiniGame1;
    public LevelData CurLevel => Levels[UserDataMiniGame1.Level];
    protected override void Awake()
    {

       // UserDataMiniGame1.IsSoundTurnOn = true;
       // UserDataMiniGame1.IsVibrateTurnOn = true;

        if (PlayerPrefs.HasKey("UserDataMiniGame1"))
        {
            string jsonData = PlayerPrefs.GetString("UserDataMiniGame1");
            UserDataMiniGame1 = JsonUtility.FromJson<UserDataMiniGame1>(jsonData);
        }

    }

    public void loadData()
    {

        if (PlayerPrefs.HasKey("UserDataMiniGame1"))
        {
            string jsonData = PlayerPrefs.GetString("UserDataMiniGame1");
            UserDataMiniGame1 = JsonUtility.FromJson<UserDataMiniGame1>(jsonData);
        }

    }

    private void OnApplicationPause(bool pause)
    {
        string jsonData = JsonUtility.ToJson(UserDataMiniGame1);
        PlayerPrefs.SetString("UserDataMiniGame1", jsonData);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        string jsonData = JsonUtility.ToJson(UserDataMiniGame1);
        PlayerPrefs.SetString("UserDataMiniGame1", jsonData);
        PlayerPrefs.Save();
    }
    // read function Vibration
}

[Serializable]
public class UserDataMiniGame1
{
    public int Level;
  //  public List<BlockData> ListBlockSetStart;
}

[Serializable]
public class LevelData
{
    public List<ColData> ColBlocks;
}
[Serializable]

public class ColData
{
    public List<BlockDataMiniGame> BlockDatas; 
  
}
[Serializable]
public class BlockDataMiniGame
{
    public double valueBlockData;
    public int idUnitBlockData;
    public int idColorData;
    public bool item;
             
    public BlockDataMiniGame(double valueBlockData, int idUnitBlockData, int idColorData, bool item)
    {
        this.valueBlockData = valueBlockData;
        this.idUnitBlockData = idUnitBlockData;
        this.idColorData = idColorData;
        this.item = item;
    }
}
