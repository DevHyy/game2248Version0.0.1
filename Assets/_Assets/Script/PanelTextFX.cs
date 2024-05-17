using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelTextFX : Singleton<PanelTextFX>
{
    // Start is called before the first frame update

    public List<Sprite> listSpriteTextFX;
    public GameObject ObjImageTextFX;
    public Image imageTextFX;

    // showpanel call function truyeenf vaof id 
    void Start()
    {
        if (listSpriteTextFX.Count >= 0)
        {
            imageTextFX.sprite = listSpriteTextFX[0];
        }
        else { }
     
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetTextFX(int idTextFX)
     {
        imageTextFX.sprite = listSpriteTextFX[idTextFX];
        SoundController.Instance.PlaySoundShowText();
        ObjImageTextFX.transform.DOScale(1.5f, 0.2f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).Play().OnComplete(()=> {
            DOVirtual.DelayedCall(0.2f, () =>
            {
                GameController.Instance.panelTextFX.SetActive(false);
              //  DrawLine.Instance.checkShowText = true;
            }); 
        });

        // scale after 2 s  set false active
    }
}
