using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class Game1Controller : Singleton<Game1Controller>
{
    // Start is called before the first frame update
    public Block blockPb;
    [SerializeField] private List<Transform> listtransform;
    private GameObject emptyGameobject;
    public GameObject Grids;
    public List<GameObject> colBlock1 = new List<GameObject>();
    public List<GameObject> colBlock2 = new List<GameObject>();
    public List<GameObject> colBlock3 = new List<GameObject>();
    public List<GameObject> colBlock4 = new List<GameObject>();
    public List<GameObject> colBlock5 = new List<GameObject>();
    public List<List<GameObject>> AllColBlock = new List<List<GameObject>>();
    public bool canInstanceGrid;
    public List<Block1> listPbAddBlock;
    public Transform TransformListPB;
    public int valuecheckEnd;
    public bool checkEnd;
    public bool canMoveBlocktoGrid;
    public DataManagerMiniGame1 Data => DataManagerMiniGame1.Instance;

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
        canMoveBlocktoGrid = true;
        canInstanceGrid = false;
        // create List PB
        // setgris fromdata
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void InstanceListPB()
    {
        for (int i = 0; i < 7; i++)
        {
            Block1 blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block1>();
            blockClone.valueBlock = Math.Pow(2, i + 1);
            blockClone.naturalPart = blockClone.valueBlock;
            blockClone.idUnitBlock = 0;
            blockClone.idColor = i;
            UnitConvert1.Instance.convertUnit(blockClone);
            SetColorBlock(blockClone);
            blockClone.name = "Block" + blockClone.naturalPart + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit;
            blockClone.transform.parent = TransformListPB;
            listPbAddBlock.Add(blockClone);
        }
    }

    public void SetColorBlock(Block1 blockRoot)
    {
        blockRoot.colorBlock = DataManager.Instance.ListColorBlock[blockRoot.idColor % 20];
        if (blockRoot.idColor % 20 == 3 || blockRoot.idColor % 20 == 4 || blockRoot.idColor % 20 == 11 || blockRoot.idColor % 20 == 15)
        {
            blockRoot.text.color = Color.black;         
        }
        else
        {
           // Debug.Log("white");
        }


    }

    public void GridsBlocks()
    {
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

    
        for (int col = 0; col < AllColBlock.Count; col++)
        {
            for (int row = 0; row < AllColBlock[col].Count; row++)
            {
                Block1 blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block1>();
              //  blockClone.naturalPart = DataManager.Instance.UserData.ListBlockSetStart[(row) * 5 + col].valueBlockData;
              
                blockClone.naturalPart = Data.CurLevel.ColBlocks[col].BlockDatas[row].valueBlockData;
                blockClone.valueBlock = blockClone.naturalPart;
                blockClone.idUnitBlock = Data.CurLevel.ColBlocks[col].BlockDatas[row].idUnitBlockData;
                blockClone.idColor = Data.CurLevel.ColBlocks[col].BlockDatas[row].idColorData;
                blockClone.item = Data.CurLevel.ColBlocks[col].BlockDatas[row].item;
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
}
