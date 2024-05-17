using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PanelUnit : Singleton<PanelUnit>
{
    public List<TMP_Text> ListtextnumberUnit;
    public Button btnBack;
    public Text UserName;
    public Button btnEditInfor;
    public GameObject PanelEditAcount;
    public Image ImageAvatar;
    public List<Sprite> listAvatar;



 
 
    // click button set Panel true, save  luwu nam  luwu id avater to data  set avarta and name from Panel Unit
    // close Panel
    // choj 1 caiso avatar call function duyet list set mawtj na set get id  gans id avater cho bien current id avatarand set mask true  , if  click done gans bieens  id avata vaof Datamanager, luwu text nam
    void Start()
    {
        if (listAvatar.Count > 0 )
        {
            ImageAvatar.sprite = listAvatar[DataManager.Instance.UserData.idAvatar];
            // set Native size
        }
        else
        {            
        }
    
        if (DataManager.Instance.UserData.UserName == null)
        {
            // Debug.Log
            UserName.text = "NoUserName";
            // textInput.SetText("NoName");
        }
        else
        {
            // set input from Data
            Debug.Log("UserDataNot Null");
            UserName.text = DataManager.Instance.UserData.UserName;
            //  textInput.SetText(DataManager.Instance.UserData.UserName.text);
        }

        btnBack.onClick.AddListener(ButtonBackOnClick);
        btnEditInfor.onClick.AddListener(ButtonEditOnClick);
    }

    void Update()
    {

    }
    public void SetListnumberUnit()
    {
        for (int i = 0; i < 56; i++)
        {
            DataManager.Instance.UserData.ListnumberUnit.Add(0);
        }
    }
    public void RestartListnumberUnit()
    {
        for (int i = 0; i < 56; i++)
        {
            DataManager.Instance.UserData.ListnumberUnit[i] = 0;
        }
    }
    public void IncreasenumberUnit(int idUnit)
    { // add to Unit has value equal idUnit 1 number
        if (idUnit < 56)
        {
            Debug.Log("value unit" + DataManager.Instance.UserData.ListnumberUnit[idUnit]);
            DataManager.Instance.UserData.ListnumberUnit[idUnit] += 1;
            Debug.Log("Increase 1 unit" + DataManager.Instance.UserData.ListnumberUnit[idUnit]);
            if (idUnit>=1)
            {
                ListtextnumberUnit[idUnit-1].SetText(DataManager.Instance.UserData.ListnumberUnit[idUnit].ToString());
            }
        }
        else
        {
            Debug.Log("Null index ");
        }

    }
    public void SettextnumberListUnit()
    {
        for (int i = 0; i < 55; i++)
        {
            Debug.Log("Settext number ListUnit");
            ListtextnumberUnit[i].SetText(DataManager.Instance.UserData.ListnumberUnit[i+1].ToString());
        }
    }
    void ButtonBackOnClick()
    {
        Debug.Log("Button Back On Click");
        start.Instance.PanelUnit.SetActive(false);
    }
    void ButtonEditOnClick()
    {
        PanelEditAcount.SetActive(true);
        PanelAvatar.Instance.textInput.text = UserName.text;
        PanelAvatar.Instance.idAvatarCurrent = DataManager.Instance.UserData.idAvatar;
        PanelAvatar.Instance.stateCurrentMask();
      //  PanelAvatar.Instance.textInput.text = UserName.text;// or set equal Data
        // truyen idAvatar, call set text form data  toi panelAcount  
    }
}
