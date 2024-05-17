using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSettingLoading : MonoBehaviour
{
    public Button btnVibration;
    public Image imageVibration;
    public Sprite vibrationOn;
    public Sprite vibrationOff;

    public Button btnSoundEffect;
    public Image imageSoundEffect;
    public Sprite SoundEffectOn;
    public Sprite SoundEffectOff;


    public Button btnBackPanelSetting;
    // Start is called before the first frame update
    void Start()
    {      
        imageSoundEffect.sprite = DataManager.Instance.UserData.IsSoundTurnOn ? SoundEffectOn : SoundEffectOff;
        imageVibration.sprite = DataManager.Instance.UserData.IsVibrateTurnOn ? vibrationOn : vibrationOff;

        btnSoundEffect.onClick.AddListener(OnButtonSoundEffectClick);
        btnVibration.onClick.AddListener(OnButtonVibrationClick);
        btnBackPanelSetting.onClick.AddListener(OnButtonBackPanelSettingClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonSoundEffectClick()
    {
        Debug.Log("Click On button SoundEffect");        
        if (DataManager.Instance.UserData.IsSoundTurnOn)
        {
            DataManager.Instance.UserData.IsSoundTurnOn = false;
            imageSoundEffect.sprite = SoundEffectOff;
          //  imageSoundEffect.SetNativeSize();
        }
        else
        {
            DataManager.Instance.UserData.IsSoundTurnOn = true;
            imageSoundEffect.sprite = SoundEffectOn;
           // imageSoundEffect.SetNativeSize();
        }
    }
   public void OnButtonVibrationClick()
    {
        Debug.Log("On click button Vibration");
        if (DataManager.Instance.UserData.IsVibrateTurnOn)
        {
            DataManager.Instance.UserData.IsVibrateTurnOn = false;
            imageVibration.sprite = vibrationOff;
        }
        else
        {
            DataManager.Instance.UserData.IsVibrateTurnOn = true;
            imageVibration.sprite = vibrationOn;
            Vibrator.Vibrate(50);
        }
    }
    void OnButtonBackPanelSettingClick()
    {
        UITransitionPanel uitransition = transform.GetComponentInChildren<UITransitionPanel>();
        if (uitransition != null)
        {
            uitransition.OnCloseClick();
        }
    }
}
