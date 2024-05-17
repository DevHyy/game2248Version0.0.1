using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTextFXController : Singleton<PanelTextFXController>
{
    public void checkTextFX(int CountListBlock)
    {
        Debug.Log("Call CheckFX");
        if (CountListBlock >= 5 && CountListBlock < 7)
        {
            GameController.Instance.panelTextFX.SetActive(true);
            PanelTextFX.Instance.SetTextFX(0);
            DrawLine.Instance.scoreDiamond += 1;

        }
        else if (CountListBlock >= 7 && CountListBlock < 10)
        {

            GameController.Instance.panelTextFX.SetActive(true);
            PanelTextFX.Instance.SetTextFX(1);
            DrawLine.Instance.scoreDiamond += 5;

        }
        else if (CountListBlock >= 10 && CountListBlock < 15)
        {

            GameController.Instance.panelTextFX.SetActive(true);
            PanelTextFX.Instance.SetTextFX(2);
            DrawLine.Instance.scoreDiamond += 10;

        }
        else if (CountListBlock >= 15 && CountListBlock < 20)
        {
            GameController.Instance.panelTextFX.SetActive(true);
            PanelTextFX.Instance.SetTextFX(3);
            DrawLine.Instance.scoreDiamond += 20;

        }
        else if (CountListBlock >= 20)
        {
            GameController.Instance.panelTextFX.SetActive(true);
            PanelTextFX.Instance.SetTextFX(4);
            DrawLine.Instance.scoreDiamond += 50;
        }
        else
        {

        }
        DrawLine.Instance.textscoreDiamond.SetText(DrawLine.Instance.scoreDiamond.ToString());
    }


}
