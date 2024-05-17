using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;


public class ShowPanelUnLockBlock : Singleton<ShowPanelUnLockBlock>
{

    // Start is called before the first frame update
    public Button btRootBlock;
    public Button btRootBlockDown;
    public Button btTargetBlock;

    public Button btHighestBlock;
    public Button btLiminatedBlock;
    public Button btNewAddBlock;
    public GameObject PanelUnlockBlock;
    public TMP_Text TextUnlockBlock;
    public GameObject PanelAddNewBlock;
    public TMP_Text TextAddNewBlock;
    public GameObject PanelBlockLiminated;
    public TMP_Text TextBlockLiminated;

    public GameObject PanelDoubleBlock;
    public TMP_Text TextbtRootBlock;
    public TMP_Text TextbtRootBlockDown;

    public TMP_Text TextbtTargetBlock;

    public bool canInstanBlockLimited2;
    public bool canInstanBlockLimited4;

    public Button btCloseHighestBlock;
    public Button btCloseLiminatedBlock;
    public Button btCloseNewAddBlock;
    public Button btCloseDoubleBlock;
    public int indexMain;

    //public GameObject PanelFreeDiamond;
   // public Button btnCloseReward;
    void Start()
    {
        indexMain = 0;
        PanelUnlockBlock.SetActive(false);
        PanelAddNewBlock.SetActive(false);
        PanelBlockLiminated.SetActive(false);
        PanelDoubleBlock.SetActive(false);
 
        btCloseDoubleBlock.onClick.AddListener(OnButtonDoubleBlockClick);
        btCloseHighestBlock.onClick.AddListener(OnButtonCloseHighestClick);
        btCloseLiminatedBlock.onClick.AddListener(OnButtonCloseLiminatedClick);
        btCloseNewAddBlock.onClick.AddListener(OnButtonCloseNewAddClick);    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("IndexMain"+ indexMain);
        if (indexMain == 0)
        {
            Debug.Log("INDEXMAIN EQUAL 0");
            DrawLine.Instance.PanelMainCanActive = true;
        }
        else
        {
            Debug.Log("INDEXMAIN NOT EQUAL 0");
            DrawLine.Instance.PanelMainCanActive = false;
        }
    }
    public void GetnewStateListPB(Block highestBlock)
    {
      
        Debug.Log("GO TO GET NEW STATE LIST PB");


        if (highestBlock.idUnitBlock == 0 && highestBlock.valueBlock < 2048)
        {
            Debug.Log(" VALUE < 2048 ");
            ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor );
        }
        else if (highestBlock.idUnitBlock == 0 && highestBlock.valueBlock >= 2048 && highestBlock.valueBlock < 10000)
        {
            if (GameController.Instance.liminatedBlock.valueBlock == 1)
            {
                Debug.Log("VALUEBLOCK EQUAL 2048");
                InstanceNewBlockEliminated(2, 0, 0);

                ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor);
                ShowPanelAddNewBlock(GameController.Instance.newBlockAdd.idUnitBlock, GameController.Instance.newBlockAdd.valueBlock, GameController.Instance.newBlockAdd.idColor);
                ShowPanelBlockLiminated(GameController.Instance.liminatedBlock.idUnitBlock, GameController.Instance.liminatedBlock.valueBlock, GameController.Instance.liminatedBlock.idColor);

            }
            else
            {
                //show highest block
                ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor);

            }
        }
        else if (highestBlock.idUnitBlock == 1 && highestBlock.valueBlock >= 16 && highestBlock.valueBlock < 131)
        {
            Debug.Log("VALUE FROM 16 K TO 131K ");
            if (GameController.Instance.liminatedBlock.valueBlock == 1 || GameController.Instance.liminatedBlock.valueBlock == 2)
            {
                InstanceNewBlockEliminated(4, 0, 1);
                ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor);
                ShowPanelAddNewBlock(GameController.Instance.newBlockAdd.idUnitBlock, GameController.Instance.newBlockAdd.valueBlock, GameController.Instance.newBlockAdd.idColor);
                ShowPanelBlockLiminated(GameController.Instance.liminatedBlock.idUnitBlock, GameController.Instance.liminatedBlock.valueBlock, GameController.Instance.liminatedBlock.idColor);

            }
            else
            {
                ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor);
            }
        }
        else if (highestBlock.idUnitBlock == 1 && highestBlock.valueBlock == 131.072)
        {
            Debug.Log("BLOCK LIMINATED 8");
            InstanceNewBlockEliminated(8, 0, 2);
            ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor);
            ShowPanelAddNewBlock(GameController.Instance.newBlockAdd.idUnitBlock, GameController.Instance.newBlockAdd.valueBlock, GameController.Instance.newBlockAdd.idColor);
            ShowPanelBlockLiminated(GameController.Instance.liminatedBlock.idUnitBlock, GameController.Instance.liminatedBlock.valueBlock, GameController.Instance.liminatedBlock.idColor);


        }
        else if (highestBlock.idUnitBlock == 1 && highestBlock.valueBlock == 262.144)
        {
            Debug.Log("BLOCK LIMINATED 16");
            InstanceNewBlockEliminated(16, 0, 3);
            ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor);
            ShowPanelAddNewBlock(GameController.Instance.newBlockAdd.idUnitBlock, GameController.Instance.newBlockAdd.valueBlock, GameController.Instance.newBlockAdd.idColor);
            ShowPanelBlockLiminated(GameController.Instance.liminatedBlock.idUnitBlock, GameController.Instance.liminatedBlock.valueBlock, GameController.Instance.liminatedBlock.idColor);


        }
        else if (highestBlock.idUnitBlock == 1 && highestBlock.valueBlock == 524.288)
        {
            Debug.Log("BLOCK LIMINATED 32");
            InstanceNewBlockEliminated(32, 0, 4);
            ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor);
            ShowPanelAddNewBlock(GameController.Instance.newBlockAdd.idUnitBlock, GameController.Instance.newBlockAdd.valueBlock, GameController.Instance.newBlockAdd.idColor);
            ShowPanelBlockLiminated(GameController.Instance.liminatedBlock.idUnitBlock, GameController.Instance.liminatedBlock.valueBlock, GameController.Instance.liminatedBlock.idColor);

        }
        else
        {
            Debug.Log("VALUE MORE THAN 131K ");
           if (DrawLine.Instance.indexNewLimitBlock >= 0)
            {
                GameController.Instance.liminatedBlock.valueBlock = GameController.Instance.listPbAddBlock[DrawLine.Instance.indexNewLimitBlock].valueBlock;
                GameController.Instance.liminatedBlock.naturalPart = GameController.Instance.liminatedBlock.valueBlock;
                GameController.Instance.liminatedBlock.idUnitBlock = GameController.Instance.listPbAddBlock[DrawLine.Instance.indexNewLimitBlock].idUnitBlock;
                GameController.Instance.liminatedBlock.idColor = GameController.Instance.listPbAddBlock[DrawLine.Instance.indexNewLimitBlock].idColor;

                GameController.Instance.liminatedBlock.text.SetText(GameController.Instance.liminatedBlock.naturalPart.ToString() + DataManager.Instance.ListUnitBlock[GameController.Instance.liminatedBlock.idUnitBlock].nameUnit);
                GameController.Instance.liminatedBlock.name = GameController.Instance.liminatedBlock.name = "BlockLimited" + GameController.Instance.liminatedBlock.naturalPart + DataManager.Instance.ListUnitBlock[GameController.Instance.liminatedBlock.idUnitBlock].nameUnit;
                GameController.Instance.SetColorBlock(GameController.Instance.liminatedBlock);
                Debug.Log("LIMITED BLOcK " + GameController.Instance.liminatedBlock.naturalPart.ToString() + DataManager.Instance.ListUnitBlock[GameController.Instance.liminatedBlock.idUnitBlock].nameUnit);

                OverrideListPB(GameController.Instance.liminatedBlock);      
                GameController.Instance.newBlockAdd = GameController.Instance.listPbAddBlock[GameController.Instance.listPbAddBlock.Count - 1];
                Debug.Log("LIMITED BLOcK " + GameController.Instance.liminatedBlock.naturalPart.ToString() + DataManager.Instance.ListUnitBlock[GameController.Instance.liminatedBlock.idUnitBlock].nameUnit);
              //  DestroyOldBlockLiminatedinBoard();
                ShowPanelUnlockBlock(highestBlock.idUnitBlock, highestBlock.valueBlock, highestBlock.idColor);
                ShowPanelAddNewBlock(GameController.Instance.newBlockAdd.idUnitBlock, GameController.Instance.newBlockAdd.valueBlock, GameController.Instance.newBlockAdd.idColor);
                ShowPanelBlockLiminated(GameController.Instance.liminatedBlock.idUnitBlock, GameController.Instance.liminatedBlock.valueBlock, GameController.Instance.liminatedBlock.idColor);
            }
        }     
        DataManager.Instance.UserData.BlockNewAdd.valueBlockData = GameController.Instance.newBlockAdd.valueBlock;
        DataManager.Instance.UserData.BlockNewAdd.idUnitBlockData = GameController.Instance.newBlockAdd.idUnitBlock;
        DataManager.Instance.UserData.BlockNewAdd.idColorData = GameController.Instance.newBlockAdd.idColor;

        DataManager.Instance.UserData.HighestBlock.valueBlockData = DrawLine.Instance.highestBlock.valueBlock;
        DataManager.Instance.UserData.HighestBlock.idUnitBlockData = DrawLine.Instance.highestBlock.idUnitBlock;
        DataManager.Instance.UserData.HighestBlock.idColorData = DrawLine.Instance.highestBlock.idColor;

        DataManager.Instance.UserData.BlockLimited.valueBlockData = GameController.Instance.liminatedBlock.valueBlock;
        DataManager.Instance.UserData.BlockLimited.idUnitBlockData = GameController.Instance.liminatedBlock.idUnitBlock;
        DataManager.Instance.UserData.BlockLimited.idColorData = GameController.Instance.liminatedBlock.idColor;


    }
    public void InstanceNewBlockEliminated(int valueblock,int idunitblock, int idColorblock)
    {
        GameController.Instance.liminatedBlock.valueBlock = valueblock;
        GameController.Instance.liminatedBlock.naturalPart = GameController.Instance.liminatedBlock.valueBlock;
        GameController.Instance.liminatedBlock.idUnitBlock = idunitblock;
        GameController.Instance.liminatedBlock.idColor = idColorblock;
        GameController.Instance.liminatedBlock.text.SetText(GameController.Instance.liminatedBlock.naturalPart.ToString());
        GameController.Instance.liminatedBlock.name = GameController.Instance.liminatedBlock.name = "BlockLimited" + GameController.Instance.liminatedBlock.naturalPart + DataManager.Instance.ListUnitBlock[GameController.Instance.liminatedBlock.idUnitBlock].nameUnit;
        GameController.Instance.SetColorBlock(GameController.Instance.liminatedBlock);
        OverrideListPB(GameController.Instance.liminatedBlock);
        GameController.Instance.newBlockAdd = GameController.Instance.listPbAddBlock[GameController.Instance.listPbAddBlock.Count - 1];
      //  DestroyOldBlockLiminatedinBoard();
    }
    public void ShowPanelDoubleBlock(Block highestBlock)
    {
        indexMain += 1;
        DOVirtual.DelayedCall(1.0f, () =>
        {                
          PanelDoubleBlock.SetActive(true);
          SoundController.Instance.PlaySoundShowPanel();
          TextbtRootBlockDown.SetText(Math.Floor(highestBlock.valueBlock).ToString() + DataManager.Instance.ListUnitBlock[highestBlock.idUnitBlock].nameUnit);
          btRootBlockDown.image.color = DataManager.Instance.ListColorBlock[highestBlock.idColor % 20];
            if (highestBlock.idColor % 20 == 3 || highestBlock.idColor % 20 == 4 || highestBlock.idColor % 20 == 11 || highestBlock.idColor % 20 == 15)
            {
                TextbtRootBlockDown.color = Color.black;
            }
            else
            {
                TextbtRootBlockDown.color = Color.white;
           
            }

            TextbtRootBlock.SetText(Math.Floor(highestBlock.valueBlock).ToString() + DataManager.Instance.ListUnitBlock[highestBlock.idUnitBlock].nameUnit);
          btRootBlock.image.color = DataManager.Instance.ListColorBlock[highestBlock.idColor % 20];
            if (highestBlock.idColor % 20 == 3 || highestBlock.idColor % 20 == 4 || highestBlock.idColor % 20 == 11 || highestBlock.idColor % 20 == 15)
            {
                TextbtRootBlock.color = Color.black;
            }
            else
            {
                TextbtRootBlock.color = Color.white;
            }

            DrawLine.Instance.DoubleHighestBlock.valueBlock = highestBlock.valueBlock * 2;
          DrawLine.Instance.DoubleHighestBlock.idUnitBlock = highestBlock.idUnitBlock;
          DrawLine.Instance.DoubleHighestBlock.idColor = highestBlock.idColor + 1;
          UnitConvert.Instance.convertUnit(DrawLine.Instance.DoubleHighestBlock);
        //
        TextbtTargetBlock.SetText(Math.Floor(DrawLine.Instance.DoubleHighestBlock.valueBlock).ToString() + DataManager.Instance.ListUnitBlock[DrawLine.Instance.DoubleHighestBlock.idUnitBlock].nameUnit);
        btTargetBlock.image.color = DataManager.Instance.ListColorBlock[DrawLine.Instance.DoubleHighestBlock.idColor % 20];
            if (DrawLine.Instance.DoubleHighestBlock.idColor % 20 == 3 || DrawLine.Instance.DoubleHighestBlock.idColor % 20 == 4 || DrawLine.Instance.DoubleHighestBlock.idColor % 20 == 11 || DrawLine.Instance.DoubleHighestBlock.idColor % 20 == 15)
            {
                TextbtTargetBlock.color = Color.black;
            }
            else
            {
                TextbtTargetBlock.color = Color.white;
            }

        });
    }

    public void ShowPanelUnlockBlock(int idUnitBlock, double valueBlock, int idColorBlock)
    {

        indexMain += 1;
        DOVirtual.DelayedCall(1.5f, () =>
        { 
          PanelUnlockBlock.SetActive(true);
          SoundController.Instance.PlaySoundShowUnlock();
          NewBlockUnlockController.Instance.ShowPanelSpinWheel();
          TextUnlockBlock.SetText(Math.Floor(valueBlock).ToString() + DataManager.Instance.ListUnitBlock[idUnitBlock].nameUnit);
          btHighestBlock.image.color = DataManager.Instance.ListColorBlock[idColorBlock % 20];
            if (idColorBlock % 20 == 3 || idColorBlock % 20 == 4 || idColorBlock % 20 == 11 || idColorBlock % 20 == 15)
            {
                TextUnlockBlock.color = Color.black;
            }
            else
            {
                TextUnlockBlock.color = Color.white;
            }
        });
    }
    public void ShowPanelAddNewBlock(int idUnitBlock, double valueBlock, int idColorBlock)
    {
        indexMain += 1;
        DOVirtual.DelayedCall(1.5f, () =>
        {      
          PanelAddNewBlock.SetActive(true);
         // SoundController.Instance.PlaySoundShowPanel();
          TextAddNewBlock.SetText(Math.Floor(valueBlock).ToString() + DataManager.Instance.ListUnitBlock[idUnitBlock].nameUnit);
          btNewAddBlock.image.color = DataManager.Instance.ListColorBlock[idColorBlock % 20];
            if (idColorBlock % 20 == 3 || idColorBlock % 20 == 4 || idColorBlock % 20 == 11 || idColorBlock % 20 == 15)
            {
                TextAddNewBlock.color = Color.black;
            }
            else
            {
                TextAddNewBlock.color = Color.white;
            }

        });
    }
    public void ShowPanelBlockLiminated(int idUnitBlock, double valueBlock, int idColorBlock)
    {
        indexMain += 1;
        DOVirtual.DelayedCall(1.5f, () =>
        {
          PanelBlockLiminated.SetActive(true);
         // SoundController.Instance.PlaySoundPanelLiminated();
          TextBlockLiminated.SetText(Math.Floor(valueBlock).ToString() + DataManager.Instance.ListUnitBlock[idUnitBlock].nameUnit);
          btLiminatedBlock.image.color = DataManager.Instance.ListColorBlock[idColorBlock % 20];
            if (idColorBlock % 20 == 3 || idColorBlock % 20 == 4 || idColorBlock % 20 == 11 || idColorBlock % 20 == 15)
            {
                TextBlockLiminated.color = Color.black;
            }
            else
            {
                TextBlockLiminated.color = Color.white;
            }
        });
    }
    public void OnButtonCloseHighestClick()
    {
        PanelUnlockBlock.SetActive(false);
        indexMain -= 1;

    }
    public void OnButtonCloseLiminatedClick()
    {
        PanelBlockLiminated.SetActive(false);
             
        DOVirtual.DelayedCall(0.5f, () =>
        { 
            DestroyOldBlockLiminatedinBoard();
            indexMain -= 1;
        });
        }
    public void OnButtonCloseNewAddClick()
    {
        PanelAddNewBlock.SetActive(false);
        indexMain -= 1;
        //UITransitionPanel uitransition = PanelAddNewBlock.GetComponentInChildren<UITransitionPanel>();
        //if (uitransition != null)
        //{
        //    uitransition.OnCloseClick();
        //}
    }
    public void OnButtonDoubleBlockClick()
    {
        //PanelDoubleBlock.SetActive(false);
        indexMain -= 1;
        UITransitionPanel uitransition = PanelDoubleBlock.GetComponentInChildren<UITransitionPanel>();
        if (uitransition != null)
        {
            uitransition.OnCloseClick();
        }
    }
    public void DestroyOldBlockLiminatedinBoard()
    {
        for (int col = 0; col < GameController.Instance.AllColBlock.Count; col++)
        {
            for (int row = 0; row < GameController.Instance.AllColBlock[col].Count; row++)
            {
               Block blockst = GameController.Instance.AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>();   
                   if (blockst.valueBlock <= GameController.Instance.liminatedBlock.valueBlock && blockst.idUnitBlock == GameController.Instance.liminatedBlock.idUnitBlock || blockst.idUnitBlock < GameController.Instance.liminatedBlock.idUnitBlock)
                   {
                       Debug.Log("Block need Destroy" + blockst.valueBlock);
                    Destroy(GameController.Instance.AllColBlock[col][row].transform.GetChild(0).gameObject);              
                   }
            }           
        }
    }

    public void setListPBStart()
    {
        Debug.Log("SET LIST PB START");
        for (int col = 0; col < GameController.Instance.AllColBlock.Count; col++)
        {
            for (int row = 0; row < GameController.Instance.AllColBlock[col].Count; row++)
            {
                Block blockst = GameController.Instance.AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>();
                DataManager.Instance.UserData.ListBlockSetStart[(row) * 5 + col].valueBlockData = blockst.valueBlock;
                DataManager.Instance.UserData.ListBlockSetStart[(row) * 5 + col].idUnitBlockData = blockst.idUnitBlock;
                DataManager.Instance.UserData.ListBlockSetStart[(row) * 5 + col].idColorData = blockst.idColor;
            }
        }
    }
    public void OverrideListPB(Block blockLiminated)
    {
        //insted of clear and instance new list => override
        for (int i = 0; i < 7; i++)
        {
            Debug.Log("OVERRIDE LISTOPB");
            GameController.Instance.listPbAddBlock[i].valueBlock = blockLiminated.valueBlock * Math.Pow(2, i + 1);// list 
            GameController.Instance.listPbAddBlock[i].naturalPart = GameController.Instance.listPbAddBlock[i].valueBlock;
            GameController.Instance.listPbAddBlock[i].idUnitBlock = blockLiminated.idUnitBlock;
            GameController.Instance.listPbAddBlock[i].idColor = blockLiminated.idColor + i + 1;
            UnitConvert.Instance.convertUnit(GameController.Instance.listPbAddBlock[i]);// in here has set text
            GameController.Instance.SetColorBlock(GameController.Instance.listPbAddBlock[i]);
            GameController.Instance.listPbAddBlock[i].name = "Block" + GameController.Instance.listPbAddBlock[i].naturalPart + DataManager.Instance.ListUnitBlock[GameController.Instance.listPbAddBlock[i].idUnitBlock].nameUnit;
        }
    }

}

