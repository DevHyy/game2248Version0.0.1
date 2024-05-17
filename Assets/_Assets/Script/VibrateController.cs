using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateController : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void BtnClicked()
    {
        if (DataManager.Instance.UserData.IsVibrateTurnOn)
        {
            Vibrator.Vibrate(50);
        } 
    }
}
