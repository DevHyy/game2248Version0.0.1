using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PanelSetting : MonoBehaviour
{
    public Button btnSound;
    public Button btnVibrate;
    public Button btnRestart;
    public Button btnClose;
    public Button btnstart;
    public Button btnHome;

    public Sprite volumOn;
    public Sprite volumOff;
    public Image imgSound;

    public Sprite vibrateOn;
    public Sprite vibrateOff;
    public Image imgVibrate;

    // Start is called before the first frame update
    void Start()
    {     
        imgSound.sprite = DataManager.Instance.UserData.IsSoundTurnOn ? volumOn : volumOff;
        imgVibrate.sprite = DataManager.Instance.UserData.IsVibrateTurnOn ? vibrateOn : vibrateOff;
        btnSound.onClick.AddListener(OnButtonSoundClick);
        btnVibrate.onClick.AddListener(OnButtonVibrateClick);
        btnRestart.onClick.AddListener(OnButtonRestartClick);
        btnClose.onClick.AddListener(OnButtonCloseClick);
        btnstart.onClick.AddListener(OnButtonStartClick);
        btnHome.onClick.AddListener(OnButtonHomeClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnButtonSoundClick()
    {
        if (DataManager.Instance.UserData.IsSoundTurnOn)
        {
            DataManager.Instance.UserData.IsSoundTurnOn = false;
            imgSound.sprite = volumOff;
        }
        else
        {
            DataManager.Instance.UserData.IsSoundTurnOn = true;
            imgSound.sprite = volumOn;
        }
    }
    void OnButtonVibrateClick()
    {
        if (DataManager.Instance.UserData.IsVibrateTurnOn)
        {
            DataManager.Instance.UserData.IsVibrateTurnOn = false;
            imgVibrate.sprite = vibrateOff;
        }
        else
        {
            DataManager.Instance.UserData.IsVibrateTurnOn = true;
            imgVibrate.sprite = vibrateOn;
            Vibrator.Vibrate(50);
        }
    }
    void OnButtonRestartClick()
    {
        //
        ShowPanelUnLockBlock.Instance.indexMain += 1;
        PanelGameOver.Instance.PanelRestartGame.SetActive(true);
    }

    void OnButtonCloseClick()
    {
        DOVirtual.DelayedCall(0.1f, () =>
        {
            ShowPanelUnLockBlock.Instance.indexMain -= 1;
            FunctionBonus.Instance.PanelSetting.SetActive(false);
           
        });

   
    }
    void OnButtonStartClick()
    {
        DOVirtual.DelayedCall(0.1f, () =>
        {
            ShowPanelUnLockBlock.Instance.indexMain -= 1;
            FunctionBonus.Instance.PanelSetting.SetActive(false);

        });
    }
    void OnButtonHomeClick()
    {
       // DOVirtual.DelayedCall(0.1f, () =>
       // {
          //  ShowPanelUnLockBlock.Instance.indexMain -= 1;
          //  FunctionBonus.Instance.PanelSetting.SetActive(false);
            // load scene Loading
            SceneManager.LoadScene("Loading");

        // });

       
    }

}
