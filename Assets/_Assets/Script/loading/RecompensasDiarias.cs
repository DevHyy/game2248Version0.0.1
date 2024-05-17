using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using TMPro;

public class RecompensasDiarias : Singleton<RecompensasDiarias>
{
    public string url = "www.google.com";
    //    public string urlFecha = "http://worldclockapi.com/api/json/est/now";
    public string urlFecha = "https://www.timeanddate.com/worldclock";
    // https://www.timeanddate.com/worldclock/
    public string LaFecha = "";
    // bool canClaim;
    public Button Claim;
    public Button Claimx2;
    public List<int> rewardcoin;

    public List<Image> state2;// can check
    public List<Image> state3;// can claim but not yet claim
                              //  public GameObject recompensadiariaPANEL;

    public Button ClosePannelDailyReward;

    public GameObject Claim1;
    public GameObject NextTimeReward;
    public TMP_Text TextTimeReward;


    public float moveTime;
    public float timer;
    [Obsolete]
    private void Start()
    {
        moveTime = 0.5f;
        timer = 1f;
        Debug.Log("Call Start RecompensasDiarias Pannel ");
        StartCoroutine(CheckInternet());
        Claim.onClick.AddListener(OnButtonClaimClick);
        ClosePannelDailyReward.onClick.AddListener(OnButtonClosePannelDailyReward);
        Claimx2.onClick.AddListener(OnButtonClaimx2Click);
        recompensadiaria1();
    }
    [System.Obsolete]
    private IEnumerator CheckInternet()
    {
        WWW www = new WWW(url);
        //UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www;
        if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("no hay conexcion");
        }
        else
        {
            Debug.Log("si hay conexcion a internet");
            //  StartCoroutine(CheckFecha());
        }
    }
    [System.Obsolete]
    private IEnumerator CheckFecha()
    {
        WWW www = new WWW(urlFecha);
        yield return www;
        Debug.Log(www.text);
        string[] splitDate = www.text.Split(new string[] { "currentDateTime\":\"" }, System.StringSplitOptions.None);
        //  LaFecha = splitDate[1].Substring(0, 10);
        //  Debug.Log("CurrentDateTime");
        // Debug.Log(LaFecha);
        if (splitDate.Length >= 2)
        {
            LaFecha = splitDate[1].Substring(0, 10);
            Debug.Log("CurrentDateTime");
            Debug.Log(LaFecha);
        }
        else
        {
            Debug.LogError("Unexpected data format. Unable to parse currentDateTime.");
        }
    }
    public void Update()
    {
        string dateOld = DataManager.Instance.UserData.PlayDateOld;
        if (string.IsNullOrEmpty(dateOld))
        {
        }
        else
        {
            DateTime _fechaahora = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DateTime _dateOld = Convert.ToDateTime(dateOld);
            TimeSpan x = _fechaahora.Subtract(_dateOld);
            Debug.Log("x  " + x);
            if (x.Days >= 1 && x.Days < 2)
            {
                Debug.Log("Can Claim");

                Claim1.SetActive(true);
                Claim.interactable = true;
                NextTimeReward.SetActive(false);
            }
            else if (x.Days < 1)
            {
                if (Claim1.activeSelf)
                { }
                else
                {
                    //DateTime manhana = _fechaahora.AddDays(1);
                    DateTime manhana = DateTime.Today.AddDays(1);
                    //today.AddDays(1).AddSeconds(-1);
                    TimeSpan timeToMidnight = manhana - _fechaahora;
                    // Debug.Log("timeToMidnight" + timeToMidnight.ToString());
                    /* while (timeToMidnight.TotalSeconds > 0)
                     {                            
                         //after 1 s reset text
                         Debug.Log("Show oclock timeReward");
                         TextTimeReward.SetText(timeToMidnight.ToString());              
                     }
                    */
                    //  Claim1.SetActive(true);
                    //   Claim.interactable = true;
                    NextTimeReward.SetActive(true);

                    moveTime -= Time.deltaTime;
                    if (moveTime >= 0) return;
                    moveTime = timer;
                    TextTimeReward.SetText(timeToMidnight.ToString());

                }
               
            } 
          /*  else if(x.Days >= 2)
                {    
                Debug.Log("Day >= 2 ");
                state2[0].gameObject.SetActive(true);
                Claim1.SetActive(true);
                Claim.interactable = true;
                NextTimeReward.SetActive(false);
                DataManager.Instance.UserData.PlayGameCount = 0;
                DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString("yyyy-MM-dd");
            }*/
        }
    }
    public void recompensadiaria1()
    {
        for (int i = 0; i < 5; i++)
        {
            state2[i].gameObject.SetActive(false);
            state3[i].gameObject.SetActive(false);
        }

        string dateOld = DataManager.Instance.UserData.PlayDateOld;

        // set all button state 1
        if (string.IsNullOrEmpty(dateOld))
        {
            Debug.Log(" DataDate Null ");
            Debug.Log(" firstDay recompensa ");
            //  DateTime _fechaahora1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));     
            DateTime _fechaahora1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            // DateTime _fechaahora2 = today.AddHours(-today.Hour).AddMinutes(-today.Minute).AddSeconds(-today.Second).AddMilliseconds(-today.Millisecond);
            DataManager.Instance.UserData.PlayDateOld = _fechaahora1.ToString("yyyy-MM-dd");
            // DataManager.Instance.UserData.PlayDateOld = LaFecha;
            DataManager.Instance.UserData.PlayGameCount = 0;
            state2[0].gameObject.SetActive(true);
            Claim1.SetActive(true);
            Claim.interactable = true;
            NextTimeReward.SetActive(false);
        }
        else
        {
            Debug.Log("Data Date Not null");
            DateTime _fechaahora = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DateTime _dateOld = Convert.ToDateTime(dateOld);

            TimeSpan x = _fechaahora.Subtract(_dateOld);
            Debug.Log("fechaahora" + _fechaahora);
            Debug.Log("dateOld" + _dateOld);
            Debug.Log(x);
            if (x.Days < 1)
            {
                Debug.Log("Day < 1 ");
                if (DataManager.Instance.UserData.PlayGameCount == 0)
                {
                    Debug.Log("Count = 0");
                    state2[0].gameObject.SetActive(true);
                    Claim1.SetActive(true);
                    Claim.interactable = true;
                    NextTimeReward.SetActive(false);
                }
                else if (DataManager.Instance.UserData.PlayGameCount == 1)
                {
                    Debug.Log("Count = 1");
                    state3[0].gameObject.SetActive(true);
                    state2[1].gameObject.SetActive(true);
                    //  Claim.interactable = false;
                    Claim1.SetActive(false);
                    Claim.interactable = false;
                    NextTimeReward.SetActive(true);
                }
                else if (DataManager.Instance.UserData.PlayGameCount == 2)
                {
                    Debug.Log("Count = 2");
                    state3[0].gameObject.SetActive(true);
                    state3[1].gameObject.SetActive(true);
                    state2[2].gameObject.SetActive(true);
                    //   Claim.interactable = false;
                    Claim1.SetActive(false);
                    Claim.interactable = false;
                    NextTimeReward.SetActive(true);
                }
                else if (DataManager.Instance.UserData.PlayGameCount == 3)
                {
                    Debug.Log("Count = 3");
                    state3[0].gameObject.SetActive(true);
                    state3[1].gameObject.SetActive(true);
                    state3[2].gameObject.SetActive(true);
                    state2[3].gameObject.SetActive(true);
                    //    Claim.interactable = false;
                    Claim1.SetActive(false);
                    Claim.interactable = false;
                    NextTimeReward.SetActive(true);
                }
                else if (DataManager.Instance.UserData.PlayGameCount == 4)
                {
                    Debug.Log("Count = 4");
                    state3[0].gameObject.SetActive(true);
                    state3[1].gameObject.SetActive(true);
                    state3[2].gameObject.SetActive(true);
                    state3[3].gameObject.SetActive(true);
                    state2[4].gameObject.SetActive(true);
                    //  Claim.interactable = false;
                    Claim1.SetActive(false);
                    Claim.interactable = false;
                    NextTimeReward.SetActive(true);
                }
                else if (DataManager.Instance.UserData.PlayGameCount >= 5)
                {
                    Debug.Log("Count >= 5");
                    state3[0].gameObject.SetActive(true);
                    state3[1].gameObject.SetActive(true);
                    state3[2].gameObject.SetActive(true);
                    state3[3].gameObject.SetActive(true);
                    state3[4].gameObject.SetActive(true);
                    //   Claim.interactable = false;
                    Claim1.SetActive(false);
                    Claim.interactable = false;
                    NextTimeReward.SetActive(true);
                }
            }
            if (x.Days >= 1 && x.Days < 2)
            {
                Debug.Log("Day >= 1  and Day < 2");
                int gameCount = DataManager.Instance.UserData.PlayGameCount;
                if (gameCount == 0)
                {
                    Debug.Log("Count = 0");
                    state2[0].gameObject.SetActive(true);
                    // DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString();
                    DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString("yyyy-MM-dd");
                    //  Claim.interactable = true;
                    Claim1.SetActive(true);
                    Claim.interactable = true;
                    NextTimeReward.SetActive(false);
                }
                else if (gameCount == 1)
                {
                    Debug.Log("Count = 1");
                    state3[0].gameObject.SetActive(true);
                    state2[1].gameObject.SetActive(true);
                    //Claim.interactable = true;
                    Claim1.SetActive(true);
                    Claim.interactable = true;
                    NextTimeReward.SetActive(false);
                }
                else if (gameCount == 2)
                {
                    Debug.Log("Count = 2");
                    state3[0].gameObject.SetActive(true);
                    state3[1].gameObject.SetActive(true);
                    state2[2].gameObject.SetActive(true);
                    // Claim.interactable = true;
                    Claim1.SetActive(true);
                    Claim.interactable = true;
                    NextTimeReward.SetActive(false);
                }
                else if (gameCount == 3)
                {
                    Debug.Log("Count = 3");
                    state3[0].gameObject.SetActive(true);
                    state3[1].gameObject.SetActive(true);
                    state3[2].gameObject.SetActive(true);
                    state2[3].gameObject.SetActive(true);
                    // Claim.interactable = true;
                    Claim1.SetActive(true);
                    Claim.interactable = true;
                    NextTimeReward.SetActive(false);
                }
                else if (gameCount == 4)
                {
                    Debug.Log("Count = 4");
                    state3[0].gameObject.SetActive(true);
                    state3[1].gameObject.SetActive(true);
                    state3[2].gameObject.SetActive(true);
                    state3[3].gameObject.SetActive(true);
                    state2[4].gameObject.SetActive(true);
                    //  Claim.interactable = true;
                    Claim1.SetActive(true);
                    Claim.interactable = true;
                    NextTimeReward.SetActive(false);
                }
                else if (gameCount >= 5)
                {
                    Debug.Log("Count = 5");
                    state3[0].gameObject.SetActive(true);
                    state3[1].gameObject.SetActive(true);
                    state3[2].gameObject.SetActive(true);
                    state3[3].gameObject.SetActive(true);
                    state2[4].gameObject.SetActive(true);
                    //  Claim.interactable = true;
                    Claim1.SetActive(true);
                    Claim.interactable = true;
                    NextTimeReward.SetActive(false);
                }
            }
            else if (x.Days >= 2)
            {
                // if missing a day reset reward
                //days1 can check but not yet check
                Debug.Log("Day >= 2 ");
                state2[0].gameObject.SetActive(true);
                // Claim.interactable = true;
                Claim1.SetActive(true);
                Claim.interactable = true;
                NextTimeReward.SetActive(false);
                DataManager.Instance.UserData.PlayGameCount = 0;
                // DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString();
                DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString("yyyy-MM-dd");

            }
        }
    }

    public void OnButtonClaimClick()
    {
        if (DataManager.Instance.UserData.PlayGameCount >= 5)
        {
            DataManager.Instance.UserData.Diamond += 224;
        }
        else
        {
            DataManager.Instance.UserData.Diamond += rewardcoin[DataManager.Instance.UserData.PlayGameCount];
        }
        start.Instance.textScoreDiamond.SetText(DataManager.Instance.UserData.Diamond.ToString());
        DataManager.Instance.UserData.PlayGameCount += 1;
        DateTime _fechaahora = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString("yyyy-MM-dd");
        //  Claim.interactable = false;
        recompensadiaria1();
        Claim1.SetActive(false);
        Claim.interactable = false;
        NextTimeReward.SetActive(true);
        Debug.Log("Claim interactive False");
    }

    public void OnButtonClaimx2Click()
    {
        if (DataManager.Instance.UserData.PlayGameCount >= 5)
        {
            DataManager.Instance.UserData.Diamond += 2 * 224;
        }
        else
        {
            DataManager.Instance.UserData.Diamond += 2 * rewardcoin[DataManager.Instance.UserData.PlayGameCount];
        }
        start.Instance.textScoreDiamond.SetText(DataManager.Instance.UserData.Diamond.ToString());
        DataManager.Instance.UserData.PlayGameCount += 1;
        DateTime _fechaahora = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString("yyyy-MM-dd");
        //  Claim.interactable = false;
        recompensadiaria1();
        Claim1.SetActive(false);
        Claim.interactable = false;
        NextTimeReward.SetActive(true);
        Debug.Log("Claim interactive False");
    }
    public void recompensapanel()// click daily reward
    {
        recompensadiaria1();
        start.Instance.recompensadiariaPANEL.SetActive(true);
    }
    void OnButtonClosePannelDailyReward()
    {
        start.Instance.recompensadiariaPANEL.SetActive(false);
    }
 /*   public void ResetDataDailyReward()
    {

        DateTime _fechaahora = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        state2[0].gameObject.SetActive(true);   
        Claim1.SetActive(true);
        Claim.interactable = true;
        NextTimeReward.SetActive(false);
        DataManager.Instance.UserData.PlayGameCount = 0;
               // DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString();
        DataManager.Instance.UserData.PlayDateOld = _fechaahora.ToString("yyyy-MM-dd");
    }*/
}
