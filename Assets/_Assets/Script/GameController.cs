using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using DG.Tweening;
public class GameController : Singleton<GameController>
{
    public GameObject panelTextFX;
    public GameObject panelHighestBlock;
    public TMP_Text TextHighestBlock;
    public Block blockPb;
    public Block liminatedBlock;
    public Block newBlockAdd; 
    [SerializeField] private List<Transform> listtransform;
    public Transform RootBlocks;
    private GameObject emptyGameobject;
    public GameObject Grids;
    private int columnCount = 5;
    private int rowCount = 8;
    public List<GameObject> colBlock1 = new List<GameObject>();
    public List<GameObject> colBlock2 = new List<GameObject>();
    public List<GameObject> colBlock3 = new List<GameObject>();
    public List<GameObject> colBlock4 = new List<GameObject>();
    public List<GameObject> colBlock5 = new List<GameObject>();
    public List<List<GameObject>> AllColBlock = new List<List<GameObject>>();
    public bool canInstanceGrid;     
    public List<Block> listPbAddBlock;
    public Transform TransformListPB;
    public int valuecheckEnd;
    public bool checkEnd;
    public bool canSetMaskHighest;
    public bool  canMoveBlocktoGrid;

    public int DiamondRewardUnLockNewBlock;

    public int countShowDoubleBlock;
    void Awake()
    {
        emptyGameobject = new GameObject();
        AllColBlock.Add(colBlock1);
        AllColBlock.Add(colBlock2);
        AllColBlock.Add(colBlock3);
        AllColBlock.Add(colBlock4);
        AllColBlock.Add(colBlock5);
    }
    void Start()
    {

        countShowDoubleBlock = 0;
        canMoveBlocktoGrid = true;
        panelTextFX.SetActive(false);
        DiamondRewardUnLockNewBlock = 0;
        canSetMaskHighest = true;
        canInstanceGrid = false;

        if (DataManager.Instance.UserData.BonusDiamond == 0)
        {
            Debug.Log("DataManager.BonusDiamond equal 0");
            DataManager.Instance.UserData.BonusDiamond = 100;
        }
        else { }
        if (DataManager.Instance.UserData.BlockLimited == null)
        {
            Debug.Log("BlockLimitedNULL");
            Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
            blockClone.naturalPart = 1;       
            blockClone.valueBlock = blockClone.naturalPart;
            blockClone.idUnitBlock = -1;
            blockClone.transform.parent = TransformListPB;
            blockClone.name = "BlockLimited" + blockClone.naturalPart + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit;
            blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);
            liminatedBlock = blockClone;
            // reset value in data
            DataManager.Instance.UserData.BlockLimited.valueBlockData = liminatedBlock.valueBlock;
            DataManager.Instance.UserData.BlockLimited.idUnitBlockData = liminatedBlock.idUnitBlock;
            DataManager.Instance.UserData.BlockLimited.idColorData = liminatedBlock.idColor;
        }
        else
        {
            // dont null instance and set value 
            Debug.Log("BlockLimited NOT NULL");
            Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
            blockClone.naturalPart = DataManager.Instance.UserData.BlockLimited.valueBlockData;
            blockClone.valueBlock = blockClone.naturalPart;
            blockClone.idColor = DataManager.Instance.UserData.BlockLimited.idColorData;
            blockClone.idUnitBlock = DataManager.Instance.UserData.BlockLimited.idUnitBlockData;
            if (DataManager.Instance.UserData.BlockLimited.valueBlockData == 1)
            {
                blockClone.idColor = -1;
            }
            else
            {
                  SetColorBlock(blockClone); 
            }                
            blockClone.name = "BlockLimited" + blockClone.naturalPart + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit;
            blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);
            blockClone.transform.parent = TransformListPB;
            liminatedBlock = blockClone;
        }
        if (DataManager.Instance.UserData.BlockNewAdd == null)
        {
            Debug.Log("BlockNewAddNULL");
            Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
            blockClone.naturalPart = 0;     
            blockClone.valueBlock = blockClone.naturalPart;
            blockClone.idUnitBlock = 0;
            blockClone.transform.parent = TransformListPB;
            blockClone.name = "BlockLimited" + blockClone.naturalPart + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit;
            blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);
            newBlockAdd = blockClone;
            // reset value in data
            DataManager.Instance.UserData.BlockNewAdd.valueBlockData = newBlockAdd.valueBlock;
            DataManager.Instance.UserData.BlockNewAdd.idUnitBlockData = newBlockAdd.idUnitBlock;
            DataManager.Instance.UserData.BlockNewAdd.idColorData = newBlockAdd.idColor;
        }
        else
        {
            // dont null instance and set value 
            Debug.Log("BlockNewAdd NOT NULL");
            Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
            blockClone.naturalPart = DataManager.Instance.UserData.BlockNewAdd.valueBlockData;
            blockClone.valueBlock = blockClone.naturalPart;
            blockClone.idUnitBlock = DataManager.Instance.UserData.BlockNewAdd.idUnitBlockData;
            blockClone.idColor = DataManager.Instance.UserData.BlockNewAdd.idColorData;
            SetColorBlock(blockClone);
            blockClone.name = "BlockLimited" + blockClone.naturalPart + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit;
            blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);
            blockClone.transform.parent = TransformListPB;
            newBlockAdd = blockClone;
        }
        InstanceListPB(liminatedBlock);      
        GridsBlocks();
        valuecheckEnd = 0;
    }
    void Update()
    {
        if (canInstanceGrid)
        {
            if (canMoveBlocktoGrid)
            {
                for (int i = 0; i < AllColBlock.Count; i++)
                {
                    for (int j = 0; j < AllColBlock[i].Count; j++)
                    {
                        // if grid[i,j] is empty

                        if (AllColBlock[i][j].transform.childCount == 0)
                        {
                            if (j != 0)
                            {
                                AllColBlock[i][j - 1].transform.GetChild(0).transform.parent = AllColBlock[i][j].transform;
                            }
                        }
                        // if the first grid of current column is empty
                        if (AllColBlock[i][0].transform.childCount == 0)
                        {
                            // Position of first grid of current column
                            Vector3 pos2 = new Vector3(AllColBlock[i][0].transform.position.x,
                            AllColBlock[i][0].transform.position.y + 20f, 100f);
                            // Instantiate
                            int randomIndex = Random.Range(0, listPbAddBlock.Count);// choose radom in list to instance prefab// fix after
                            if (randomIndex < listPbAddBlock.Count)// add
                            {
                                // why block missing
                                Block blockClone = Instantiate(listPbAddBlock[randomIndex], pos2, Quaternion.identity).GetComponent<Block>();
                                // set block's parent to first grid of current column
                                blockClone.transform.parent = AllColBlock[i][0].transform;
                                blockClone.transform.localScale = new Vector3(1, 1, 1);
                            }
                        }
                    }
                }
            }
            else { }
            int k = 0;
            
            if(checkEnd)
            { 
            for (int i = 0; i < AllColBlock.Count; i++)
          
                {
                for (int j = 0; j < AllColBlock[i].Count; j++)
                {
                    if (AllColBlock[i][j].transform.childCount != 0)
                    {
                        k += 1;
                    }
                    if (k == 40)
                    {

                       // ShowPanelUnLockBlock.Instance.setListPBStart();
                        Debug.Log("CAN CHECK END GAME");


                        checkEndGame();
                    }
                }
            }
            }
            int k_1 = 0;

            for (int i = 0; i < AllColBlock.Count; i++)
            {
                for (int j = 0; j < AllColBlock[i].Count; j++)
                {
                    if (AllColBlock[i][j].transform.childCount != 0)
                    {
                        k_1 += 1;
                    }
                    if (k_1 == 40)
                    {
                        ShowPanelUnLockBlock.Instance.setListPBStart();

                        if (canSetMaskHighest)
                        {  
                            SetMaskHighestBlock();
                        }
                     
                    }
                }
            }
        }
    }
    public void GridsBlocks()
    {
        // Create Grids
        for (int col = 0; col < 5; col++)
        {
            for (int row = 0; row < 8; row++)
            {
                Vector3 pos = new Vector3(listtransform[(row) * 5 + col].position.x,
                  listtransform[(row) * 5 + col].position.y, 100f);
                // Instantiate grid
                GameObject grid = Instantiate(emptyGameobject, pos, Quaternion.identity);
                // set name to know position
                grid.name = "Grid " + col + "," + row;
                // add to current column list
                grid.transform.SetParent(Grids.transform);
                grid.transform.localScale = new Vector3(1, 1, 1);
                AllColBlock[col].Add(grid);
            }
        }     
        if (DataManager.Instance.UserData.ListBlockSetStart != null && DataManager.Instance.UserData.ListBlockSetStart.Count == 40)
        {
            if (DataManager.Instance.UserData.canRestart == false )// if canrestart false
            {
                Debug.Log("canRestart false");
                Debug.Log("Count LisBlockSetStart" + DataManager.Instance.UserData.ListBlockSetStart.Count);
                Debug.Log("LIST BLOCK SET START NOT NULL");
                for (int col = 0; col < AllColBlock.Count; col++)
                {
                    for (int row = 0; row < AllColBlock[col].Count; row++)
                    {
                        Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
                        blockClone.naturalPart = DataManager.Instance.UserData.ListBlockSetStart[(row) * 5 + col].valueBlockData;
                        blockClone.valueBlock = blockClone.naturalPart;
                        blockClone.idUnitBlock = DataManager.Instance.UserData.ListBlockSetStart[(row) * 5 + col].idUnitBlockData;
                        blockClone.idColor = DataManager.Instance.UserData.ListBlockSetStart[(row) * 5 + col].idColorData;
                        SetColorBlock(blockClone);
                        blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);
                        blockClone.transform.parent = listtransform[(row) * 5 + col];
                        blockClone.transform.parent = AllColBlock[col][row].gameObject.transform;
                        blockClone.name += col + "" + row;
                        blockClone.transform.localPosition = Vector3.zero;
                        blockClone.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                canInstanceGrid = true;
            }
            else 
            {            
                for (int col = 0; col < AllColBlock.Count; col++)
                {
                    for (int row = 0; row < AllColBlock[col].Count; row++)
                    {
                        List<int> listindex = new List<int>() { 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 6 };
                        int randomIndex = Random.Range(0, listindex.Count);
                        int valuerandomIndex = listindex[randomIndex];
                        Debug.Log("value Random Index" + valuerandomIndex);
                        if (valuerandomIndex < listPbAddBlock.Count)
                        {
                            Block blockClone = Instantiate(listPbAddBlock[valuerandomIndex], Vector3.zero, Quaternion.identity).GetComponent<Block>();
                            SetColorBlock(blockClone);
                            blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);

                            blockClone.transform.parent = listtransform[(row) * 5 + col];
                            blockClone.transform.parent = AllColBlock[col][row].gameObject.transform;// go to element get to gameobject  to transfprm and instantiate block 
                            blockClone.name += col + "" + row;
                            blockClone.transform.localPosition = Vector3.zero;
                            blockClone.transform.localScale = new Vector3(1, 1, 1);
                          }
                    }
                }                
                canInstanceGrid = true;
                DataManager.Instance.UserData.canRestart = false;
            }
        }
        if (DataManager.Instance.UserData.ListBlockSetStart.Count == 0)
        {
            Debug.Log("LIST BLOCK SET START NULL");
            for (int col = 0; col < AllColBlock.Count; col++)
            {
                for (int row = 0; row < AllColBlock[col].Count; row++)
                {
                    List<int> listindex = new List<int>() { 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 6 };
                  
                   int randomIndex = Random.Range(0, listindex.Count);
                    int valuerandomIndex = listindex[randomIndex];
                    Debug.Log("value Random Index" + valuerandomIndex);
                    if (valuerandomIndex < listPbAddBlock.Count)
                        {
                        Block blockClone = Instantiate(listPbAddBlock[valuerandomIndex], Vector3.zero, Quaternion.identity).GetComponent<Block>();

                        blockClone.transform.parent = listtransform[(row) * 5 + col];
                        blockClone.transform.parent = AllColBlock[col][row].gameObject.transform;// go to element get to gameobject  to transfprm and instantiate block 
                        blockClone.name += col + "" + row; 
                        blockClone.transform.localPosition = Vector3.zero;
                        blockClone.transform.localScale = new Vector3(1, 1, 1);
                        DataManager.Instance.UserData.ListBlockSetStart.Add(new BlockData(blockClone.valueBlock, blockClone.idUnitBlock, blockClone.idColor));
                    }
                }
            }
            canInstanceGrid = true;
        }
    }
    public void InstanceListPB(Block blockLiminated)
    {
        for (int i = 0; i < 7; i++)
        {
            Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
            blockClone.valueBlock = blockLiminated.valueBlock * Math.Pow(2, i + 1);
            blockClone.naturalPart = blockClone.valueBlock;
            blockClone.idUnitBlock = blockLiminated.idUnitBlock;
            blockClone.idColor = blockLiminated.idColor + 1*i +1;
            UnitConvert.Instance.convertUnit(blockClone);
            SetColorBlock(blockClone);
            blockClone.name = "Block" + blockClone.naturalPart + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit;
            blockClone.transform.parent = TransformListPB;
            listPbAddBlock.Add(blockClone);
        }
    }
    public void SetNewHighestBlock(Block finalBlock)
    {  
        int subfinalBlock = finalBlock.idUnitBlock -listPbAddBlock[0].idUnitBlock;
        int subhighestBlock = DrawLine.Instance.highestBlock.idUnitBlock - listPbAddBlock[0].idUnitBlock;
        //if (finalBlock.valueBlock * Math.Pow(Math.Pow(10, 3), subfinalBlock) > DrawLine.Instance.highestBlock.valueBlock * Math.Pow(Math.Pow(10, 3), subhighestBlock))
        if (finalBlock.idColor > DrawLine.Instance.highestBlock.idColor)

            {
            countShowDoubleBlock = 0;
            for (DrawLine.Instance.indexNewLimitBlock = 0; DrawLine.Instance.indexNewLimitBlock < 100; DrawLine.Instance.indexNewLimitBlock++)
                {
                    DrawLine.Instance.highestBlock.valueBlock = DrawLine.Instance.highestBlock.valueBlock * 2;
                    DrawLine.Instance.highestBlock.naturalPart = DrawLine.Instance.highestBlock.valueBlock;
                    DrawLine.Instance.highestBlock.idColor = DrawLine.Instance.highestBlock.idColor + 1; 
                    UnitConvert.Instance.convertUnit(DrawLine.Instance.highestBlock);
                    SetColorBlock(DrawLine.Instance.highestBlock);
                    Debug.Log("INDEX newliminatedBlock" + DrawLine.Instance.indexNewLimitBlock);
                    if (DrawLine.Instance.highestBlock.valueBlock - 1 <= finalBlock.valueBlock && DrawLine.Instance.highestBlock.valueBlock + 1 >= finalBlock.valueBlock && DrawLine.Instance.highestBlock.idUnitBlock == finalBlock.idUnitBlock)
                    {
                        DrawLine.Instance.highestBlock.name = "Block" + DrawLine.Instance.highestBlock.naturalPart + DataManager.Instance.ListUnitBlock[DrawLine.Instance.highestBlock.idUnitBlock].nameUnit; ;
                        DrawLine.Instance.highestBlock.text.SetText(Math.Floor(DrawLine.Instance.highestBlock.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[DrawLine.Instance.highestBlock.idUnitBlock].nameUnit);                
                        Debug.Log("INDEX newliminatedBlock" + DrawLine.Instance.indexNewLimitBlock);         
                        DiamondRewardUnLockNewBlock =  5 * (DrawLine.Instance.indexNewLimitBlock + 1);                                       
                        Debug.Log(" DiamondRewardUnLockNewBlock = " + DiamondRewardUnLockNewBlock);
                        ShowPanelUnLockBlock.Instance.GetnewStateListPB(DrawLine.Instance.highestBlock);
                        break;
                    }
                }
            Debug.Log("ADD 5 DIAMOND TO TOTAL ");
           // DrawLine.Instance.scoreDiamond += 5 * (DrawLine.Instance.indexNewLimitBlock + 1); // get value () to claim Xscore 
            DrawLine.Instance.textscoreDiamond.SetText(DrawLine.Instance.scoreDiamond.ToString());
            // SetMaskHighestBlock();
            canSetMaskHighest = true;
        }
        // else if (finalBlock.valueBlock * Math.Pow(Math.Pow(10, 3), subfinalBlock) - 1 <= DrawLine.Instance.highestBlock.valueBlock * Math.Pow(Math.Pow(10, 3), subhighestBlock) && finalBlock.valueBlock * Math.Pow(Math.Pow(10, 3), subfinalBlock) + 1 >= DrawLine.Instance.highestBlock.valueBlock * Math.Pow(Math.Pow(10, 3), subhighestBlock))

        else if (finalBlock.idColor == DrawLine.Instance.highestBlock.idColor)

        {
         
            if (countShowDoubleBlock == 0)
            {
                ShowPanelUnLockBlock.Instance.ShowPanelDoubleBlock(finalBlock);
              
                countShowDoubleBlock += 1;
                Debug.Log("CountShowBlock" + countShowDoubleBlock);
            }
            else 
            { 
            //
            }
            canSetMaskHighest = true;
        }
        else
        {
            //
        }
    }
    public void SetColorBlock(Block blockRoot)
    {
        blockRoot.colorBlock = DataManager.Instance.ListColorBlock[blockRoot.idColor % 20];
        if (blockRoot.idColor % 20 == 3 || blockRoot.idColor % 20 == 4 || blockRoot.idColor % 20 == 11 || blockRoot.idColor % 20 == 15)
        {
            blockRoot.text.color = Color.black;
        }
        else
        {
            blockRoot.text.color = Color.white;
        }

    }
    public void checkEndGame()
    {
        valuecheckEnd = 0;

        for (int j = 0; j < 5; j ++)
        {
            for (int i = 0; i < 8; i++)
            {
                /*
                // ss hang ngang
                if (j + 1 < 5 && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock == AllColBlock[j + 1][i].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == AllColBlock[j + 1][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock)
                {
                    valuecheckEnd += 1;
                }
                // ss hang doc
                if (i + 1 < 8 && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock == AllColBlock[j][i+1].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == AllColBlock[j][i+1].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock)
                {
                    valuecheckEnd += 1;
                }
                //ss cheo1
                if (i + 1 < 8 && j + 1 < 5 && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock == AllColBlock[j + 1][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == AllColBlock[j + 1][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock)
                {
                    valuecheckEnd += 1;
                }
                //ss cheo2
                if (i + 1 < 8 && j - 1 >= 0  && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock == AllColBlock[j - 1][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == AllColBlock[j - 1][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock)
                {
                    valuecheckEnd += 1;
                }
                */

                // ss hang ngang
                if (j + 1 < 5 && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idColor == AllColBlock[j + 1][i].transform.GetChild(0).gameObject.GetComponent<Block>().idColor && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == AllColBlock[j + 1][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock)
                {
                    valuecheckEnd += 1;
                }
                // ss hang doc
                if (i + 1 < 8 && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idColor == AllColBlock[j][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().idColor && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == AllColBlock[j][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock)
                {
                    valuecheckEnd += 1;
                }
                //ss cheo1
                if (i + 1 < 8 && j + 1 < 5 && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idColor == AllColBlock[j + 1][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().idColor && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == AllColBlock[j + 1][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock)
                {
                    valuecheckEnd += 1;
                }
                //ss cheo2
                if (i + 1 < 8 && j - 1 >= 0 && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idColor == AllColBlock[j - 1][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().idColor && AllColBlock[j][i].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == AllColBlock[j - 1][i + 1].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock)
                {
                    valuecheckEnd += 1;
                }

            }    
        }         
        if (valuecheckEnd == 0)
        {
            checkEnd = true;           
            if (PanelGameOver.Instance.Panelgameover.activeSelf)
            {
                Debug.Log("GAME OVER ACTIVE TRUE");
                // not can check end 
                checkEnd = false;
            }
            else 
            {
                Debug.Log("GAME OVER ACTIVE FALSE");
                ShowPanelUnLockBlock.Instance.indexMain += 1;
                PanelGameOver.Instance.TextScoreDiamond.SetText(DataManager.Instance.UserData.Diamond.ToString());
                PanelGameOver.Instance.Panelgameover.SetActive(true);
                checkEnd = false;
            }                    
        }
        else if (valuecheckEnd > 0)
        {
            Debug.Log("GAME NOT OVER VALUE > 0");
            checkEnd = false;
        }
    }
    public void SetMaskHighestBlock()
    {    
        for (int col = 0; col < AllColBlock.Count; col++)
        {
            for (int row = 0; row < AllColBlock[col].Count; row++)
            {
                if (AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == DrawLine.Instance.highestBlock.idUnitBlock && AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock + 1 >= DrawLine.Instance.highestBlock.valueBlock && AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock - 1 <= DrawLine.Instance.highestBlock.valueBlock)
                {
                    if (AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>().idUnitBlock == 0 && AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>().valueBlock < 512)
                    {
                        AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>().Mask.gameObject.SetActive(false);
                    }
                    else
                    { 
                   //    Debug.Log("SET MASK HIGHEST BLOCK TRUE");
                       AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>().Mask.gameObject.SetActive(true);
                    }                 
                }
                else
                {
                 //   Debug.Log("SET MASK HIGHEST BLOCK FALSE");
                    AllColBlock[col][row].transform.GetChild(0).gameObject.GetComponent<Block>().Mask.gameObject.SetActive(false);
                }
            }
        }
    }
}
