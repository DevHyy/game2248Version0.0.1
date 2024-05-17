using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class Block1 : MonoBehaviour
{
    //  public Image imgBack;
    public Button btnBlock;
    public Color colorBlock;
    public TMP_Text text;//(set naturalPart + UnitBlock)
    public double valueBlock;
    public double naturalPart;
    public int idUnitBlock;
 //   public Image Mask;
    public Image MaskShadow;
    public int idColor;
    public bool item;
    public Image MaskItem;

    public void Start()
    {
        //   MaskHighest = false;
        //   Mask.gameObject.SetActive(false);
        if(item)
        {
            MaskItem.gameObject.SetActive(true);
        }
        else
        {
            MaskItem.gameObject.SetActive(false);
        }

    }
    private void Update()
    {
        if (idColor >= 0)
        {
            if (DataManager.Instance.ListColorBlock.Count == 20)
            {
                Debug.Log("Count DataManager ListColorBlock" + DataManager.Instance.ListColorBlock.Count);
                btnBlock.image.color = DataManager.Instance.ListColorBlock[idColor % 20];
            }
        }
    }
}
