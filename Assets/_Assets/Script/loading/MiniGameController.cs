using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class MiniGameController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnMiniGame;
    public GameObject PanelMiniGame;

    public Button btnMiniGame1; 

    private void Awake()
    {
        PanelMiniGame.SetActive(false);
    }

    void Start()
    {
        PanelMiniGame.SetActive(false);
        btnMiniGame.onClick.AddListener(OnButtonMiniGameClick);
        btnMiniGame1.onClick.AddListener(OnButtonMiniGame1Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonMiniGameClick()
    {
        // show panel
        PanelMiniGame.SetActive(true);
    }
    public void OnButtonMiniGame1Click()
    {
        // load Scene
        LoadLevelGame1();
    }
    private void LoadLevelGame1()
    {
        SceneManager.LoadScene("Game1");
    }
}
