using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelAvatar : Singleton<PanelAvatar>
{
    // Start is called before the first frame update
    // list button image
    public List<GameObject> listMask;
    public TMP_InputField textInput;
    public Button btnSave;
    public Button btnClose;
    public int idAvatarCurrent;

    void Start()
    {
        idAvatarCurrent = DataManager.Instance.UserData.idAvatar;
        stateCurrentMask();
        if (DataManager.Instance.UserData.UserName == null)
        {
            // Debug.Log
            textInput.text= "NoUserName";
           // textInput.SetText("NoName");
        }
        else
        {
            // set input from Data
            Debug.Log("UserDataNot Null");
            textInput.text = DataManager.Instance.UserData.UserName;
          //  textInput.SetText(DataManager.Instance.UserData.UserName.text);
        }
        btnSave.onClick.AddListener(ButtonSaveOnClick);
        btnClose.onClick.AddListener(ButtonCloseOnClick);

    }
    // Update is called once per frame
    void Update()
    {

    }
    void ButtonSaveOnClick()
    {
        // save re set data save to data and reset infor in Panel Unit
        // Close Panel 
        Debug.Log("Save Data");
        Debug.Log(textInput.text);
        DataManager.Instance.UserData.UserName = textInput.text;
        Debug.Log("UserName" + DataManager.Instance.UserData.UserName);
        PanelUnit.Instance.UserName.text =  DataManager.Instance.UserData.UserName;
        DataManager.Instance.UserData.idAvatar = idAvatarCurrent;
        PanelUnit.Instance.ImageAvatar.sprite = PanelUnit.Instance.listAvatar[idAvatarCurrent];

        PanelUnit.Instance.PanelEditAcount.SetActive(false);
    }
    void ButtonCloseOnClick()
    {
        // close panel
        UITransitionPanel uitransition = PanelUnit.Instance.PanelEditAcount.GetComponentInChildren<UITransitionPanel>();
        if (uitransition != null)
        {
            uitransition.OnCloseClick();
        }
    }
    // OnClick Button truyen id vao
    public void OnClickSelectAvatar(int idAvatar)
    {
        idAvatarCurrent = idAvatar;
        for (int i = 0; i < listMask.Count; i++)
        {
            if (i == idAvatar)
            {
                listMask[i].SetActive(true);
            }
            else
            {
                listMask[i].SetActive(false);
            }

        }
    }
    public void stateCurrentMask()
    {
        for (int i = 0; i < listMask.Count; i++)
        {
            if (i == DataManager.Instance.UserData.idAvatar)
            {
                listMask[i].SetActive(true);
            }
            else
            {
                listMask[i].SetActive(false);
            }

        }

    }
}
