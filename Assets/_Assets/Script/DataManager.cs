using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager>
{

    //
    public List<Unit> ListUnitBlock;
    public List<Color> ListColorBlock = new List<Color>(20);//20 element inputEditor
    public UserData UserData;

    protected override void Awake()
    {

        UserData.IsSoundTurnOn = true;
        UserData.IsVibrateTurnOn = true;

        if (PlayerPrefs.HasKey("UserData"))
        {
            string jsonData = PlayerPrefs.GetString("UserData");
            UserData = JsonUtility.FromJson<UserData>(jsonData);
        }
        ListUnitBlock.Add(new Unit() { idUnit = 0, nameUnit = " " });
        ListUnitBlock.Add(new Unit() { idUnit = 1, nameUnit = "K" });
        ListUnitBlock.Add(new Unit() { idUnit = 2, nameUnit = "M" });
        ListUnitBlock.Add(new Unit() { idUnit = 3, nameUnit = "B" });
        ListUnitBlock.Add(new Unit() { idUnit = 4, nameUnit = "a" });
        ListUnitBlock.Add(new Unit() { idUnit = 5, nameUnit = "b" });
        ListUnitBlock.Add(new Unit() { idUnit = 6, nameUnit = "c" });
        ListUnitBlock.Add(new Unit() { idUnit = 7, nameUnit = "d" });
        ListUnitBlock.Add(new Unit() { idUnit = 8, nameUnit = "e" });
        ListUnitBlock.Add(new Unit() { idUnit = 9, nameUnit = "f" });
        ListUnitBlock.Add(new Unit() { idUnit = 10, nameUnit = "g" });
        ListUnitBlock.Add(new Unit() { idUnit = 11, nameUnit = "h" });
        ListUnitBlock.Add(new Unit() { idUnit = 12, nameUnit = "i" });
        ListUnitBlock.Add(new Unit() { idUnit = 13, nameUnit = "j" });
        ListUnitBlock.Add(new Unit() { idUnit = 14, nameUnit = "k" });
        ListUnitBlock.Add(new Unit() { idUnit = 15, nameUnit = "l" });
        ListUnitBlock.Add(new Unit() { idUnit = 16, nameUnit = "m" });
        ListUnitBlock.Add(new Unit() { idUnit = 17, nameUnit = "n" });
        ListUnitBlock.Add(new Unit() { idUnit = 18, nameUnit = "o" });
        ListUnitBlock.Add(new Unit() { idUnit = 19, nameUnit = "p" });
        ListUnitBlock.Add(new Unit() { idUnit = 20, nameUnit = "q" });
        ListUnitBlock.Add(new Unit() { idUnit = 21, nameUnit = "r" });
        ListUnitBlock.Add(new Unit() { idUnit = 22, nameUnit = "s" });
        ListUnitBlock.Add(new Unit() { idUnit = 23, nameUnit = "t" });
        ListUnitBlock.Add(new Unit() { idUnit = 24, nameUnit = "u" });
        ListUnitBlock.Add(new Unit() { idUnit = 25, nameUnit = "v" });
        ListUnitBlock.Add(new Unit() { idUnit = 26, nameUnit = "w" });
        ListUnitBlock.Add(new Unit() { idUnit = 27, nameUnit = "x" });
        ListUnitBlock.Add(new Unit() { idUnit = 28, nameUnit = "y" });
        ListUnitBlock.Add(new Unit() { idUnit = 29, nameUnit = "z" });
        //
        ListUnitBlock.Add(new Unit() { idUnit = 30, nameUnit = "aa" });
        ListUnitBlock.Add(new Unit() { idUnit = 31, nameUnit = "ab" });
        ListUnitBlock.Add(new Unit() { idUnit = 32, nameUnit = "ac" });
        ListUnitBlock.Add(new Unit() { idUnit = 33, nameUnit = "ad" });
        ListUnitBlock.Add(new Unit() { idUnit = 34, nameUnit = "ae" });
        ListUnitBlock.Add(new Unit() { idUnit = 35, nameUnit = "af" });
        ListUnitBlock.Add(new Unit() { idUnit = 36, nameUnit = "ag" });
        ListUnitBlock.Add(new Unit() { idUnit = 37, nameUnit = "ah" });
        ListUnitBlock.Add(new Unit() { idUnit = 38, nameUnit = "ai" });
        ListUnitBlock.Add(new Unit() { idUnit = 39, nameUnit = "aj" });
        ListUnitBlock.Add(new Unit() { idUnit = 40, nameUnit = "ak" });
        ListUnitBlock.Add(new Unit() { idUnit = 41, nameUnit = "al" });
        ListUnitBlock.Add(new Unit() { idUnit = 42, nameUnit = "am" });
        ListUnitBlock.Add(new Unit() { idUnit = 43, nameUnit = "an" });
        ListUnitBlock.Add(new Unit() { idUnit = 44, nameUnit = "ao" });
        ListUnitBlock.Add(new Unit() { idUnit = 45, nameUnit = "ap" });
        ListUnitBlock.Add(new Unit() { idUnit = 46, nameUnit = "qq" });
        ListUnitBlock.Add(new Unit() { idUnit = 47, nameUnit = "ar" });
        ListUnitBlock.Add(new Unit() { idUnit = 48, nameUnit = "as" });
        ListUnitBlock.Add(new Unit() { idUnit = 49, nameUnit = "at" });
        ListUnitBlock.Add(new Unit() { idUnit = 50, nameUnit = "au" });
        ListUnitBlock.Add(new Unit() { idUnit = 51, nameUnit = "av" });
        ListUnitBlock.Add(new Unit() { idUnit = 52, nameUnit = "aw" });
        ListUnitBlock.Add(new Unit() { idUnit = 53, nameUnit = "ax" });
        ListUnitBlock.Add(new Unit() { idUnit = 54, nameUnit = "ay" });
        ListUnitBlock.Add(new Unit() { idUnit = 55, nameUnit = "az" });


        if (UserData.ListnumberUnit.Count == 0)
        {
            Debug.Log("ListnumberUnit Null");
            PanelUnit.Instance.SetListnumberUnit();
            PanelUnit.Instance.SettextnumberListUnit();
        }
        if (UserData.ListnumberUnit.Count == 56)
        {
            Debug.Log("Data ListnumberUnit not null");
            PanelUnit.Instance.SettextnumberListUnit();
        }

    }

    public void loadData()
    {

        if (PlayerPrefs.HasKey("UserData"))
        {
            string jsonData = PlayerPrefs.GetString("UserData");
            UserData = JsonUtility.FromJson<UserData>(jsonData);
        }

    }

    private void OnApplicationPause(bool pause)
    {
        string jsonData = JsonUtility.ToJson(UserData);
        PlayerPrefs.SetString("UserData", jsonData);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        string jsonData = JsonUtility.ToJson(UserData);
        PlayerPrefs.SetString("UserData", jsonData);
        PlayerPrefs.Save();
    }  
    // read function Vibration
}

[Serializable]
public class UserData
{  
    public BlockData HighestBlock;// -> list prefab, add new block, and block limited  
    // public BlockData DoubleHighestBlock;
    public BlockData BlockLimited;
    public BlockData BlockNewAdd;
    public List<BlockData> ListBlockSetStart;
    public List<int> ListnumberUnit;
    public double BestScoreTotal;
    public int UnitBestScoreTotal;
    public int  Diamond;
    public bool IsSoundTurnOn;
    public bool IsMusicTurnOn;
    public bool IsVibrateTurnOn;
    public bool canRestart;
    //
    public string PlayDateOld;
    public int PlayGameCount;
    public int BonusDiamond;
    public bool Tutorial;
    
    // account
    public int idAvatar;
    public string UserName; 

}


[Serializable]
public class Unit 
{   
    public int idUnit;
    public string nameUnit;
}

[Serializable]
public class BlockData
  {
    public double valueBlockData;
    public int idUnitBlockData;
    public int idColorData;
    public BlockData(double valueBlockData, int idUnitBlockData, int idColorData)
    {
        this.valueBlockData = valueBlockData;
        this.idUnitBlockData = idUnitBlockData;
        this.idColorData = idColorData;
    }
}













