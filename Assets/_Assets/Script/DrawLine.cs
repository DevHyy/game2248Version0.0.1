using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;


public class DrawLine : Singleton<DrawLine>
{
    public bool PanelMainCanActive;
    public Block blockPb;
    public Transform TransformListPB;
    // create class convert unit for bestscore and totalbestscore to view 
    public Block highestBlock;
    private Block firstValueBlock;
    public Block DoubleHighestBlock;
    //
    public Block topBlock;
    private LineRenderer lineRend;
    private Vector3 touchPos;
    private Vector3 startTouchPos;

    private Block block2;
    private Block block;

    public Button blockCurrentTop;
    RaycastHit2D hit;
    public double maxDistance;// distance to limit 8 block around this block, set value> 1.5 size block 
    public double minDistance;// distance to dont spam this block by raycast, set value  equal 0.5 size block
    public List<Block> blocks = new List<Block>();
    //private

    public double bestScore;
    public double bestScoreCurrent;
    public int unitbestScoreCurrent;
    public double BestScoreTotal;
    public int unitBestScoreTotal;
    public int scoreDiamond;
    public TMP_Text textScoreCurrent;
    public TMP_Text textScoreTotal;
    public TMP_Text textScoreTotal1;
    public TMP_Text textscoreDiamond;
    public int indexNewLimitBlock;
    public int  indexNewAddBlock;
    public int p;
    public int idBlock0; 
    public int numberClickToSwap;

    public Block blockSwap1;
    public Block blockSwap2;
    public float moveTime;
    public float timer;
    public GameObject FxMergeBlock;
  //  public bool checkShowText;

   // public int indexFinalBlock;
    void Start()
    {
        if (DataManager.Instance.UserData == null)
        {
            BestScoreTotal = 0;
            unitBestScoreTotal = 0;
            scoreDiamond = 0;
          //  DataManager.Instance.UserData.BonusDiamond = 100;// add 15
        }
        else {

            BestScoreTotal = DataManager.Instance.UserData.BestScoreTotal;
            unitBestScoreTotal = DataManager.Instance.UserData.UnitBestScoreTotal;
            scoreDiamond = DataManager.Instance.UserData.Diamond;
        }    

        if (DataManager.Instance.UserData.HighestBlock == null)
        {
            Debug.Log("HighestBlockNULL");
            Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
            blockClone.naturalPart = 128;
            blockClone.valueBlock = blockClone.naturalPart;
            blockClone.idUnitBlock = 0;
            blockClone.idColor = 6;
            GameController.Instance.SetColorBlock(blockClone);
            blockClone.transform.parent = TransformListPB;           
            blockClone.name = "BlockHighest" + blockClone.naturalPart + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit;
            blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);
            highestBlock = blockClone;
            DataManager.Instance.UserData.HighestBlock.valueBlockData = highestBlock.valueBlock;
            DataManager.Instance.UserData.HighestBlock.idUnitBlockData = highestBlock.idUnitBlock;
            DataManager.Instance.UserData.HighestBlock.idColorData = highestBlock.idColor;
        }
        else 
        {            
              Debug.Log("HighestBlockNOTNULL");
            Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
            blockClone.naturalPart = DataManager.Instance.UserData.HighestBlock.valueBlockData;
            blockClone.valueBlock = blockClone.naturalPart;
            blockClone.idUnitBlock = DataManager.Instance.UserData.HighestBlock.idUnitBlockData;
            blockClone.idColor = DataManager.Instance.UserData.HighestBlock.idColorData;
            GameController.Instance.SetColorBlock(blockClone);
            blockClone.name = "BlockLimited" + blockClone.naturalPart + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit;
            blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);
            blockClone.transform.parent = TransformListPB;
            highestBlock = blockClone;
        }
        // instance topBlock
        topBlock = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
        topBlock.transform.parent = TransformListPB;
        // set transform parent
         topBlock.name = "TopBlock" + topBlock.naturalPart + DataManager.Instance.ListUnitBlock[topBlock.idUnitBlock].nameUnit;
          Block blockClone1 = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
          blockClone1.naturalPart = DataManager.Instance.UserData.HighestBlock.valueBlockData * 2;
          blockClone1.valueBlock = blockClone1.naturalPart;
          blockClone1.idUnitBlock = DataManager.Instance.UserData.HighestBlock.idUnitBlockData;
         blockClone1.idColor = DataManager.Instance.UserData.HighestBlock.idColorData;
         UnitConvert.Instance.convertUnit(blockClone1);
         GameController.Instance.SetColorBlock(blockClone1);
          blockClone1.name = "DoubleHighestBlock" + blockClone1.naturalPart + DataManager.Instance.ListUnitBlock[blockClone1.idUnitBlock].nameUnit;
          blockClone1.text.SetText(Math.Floor(blockClone1.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone1.idUnitBlock].nameUnit);
          blockClone1.transform.parent = TransformListPB;
          DoubleHighestBlock = blockClone1;
        indexNewLimitBlock = 0;
        // maxDistance = 114;//117
        // minDistance = 60;  //37, 60
        maxDistance = 120;//117
        minDistance = 50;  //37, 60
        lineRend = GetComponent<LineRenderer>(); 
        lineRend.enabled = false;
        textScoreTotal.enabled = true;
        PanelMainCanActive = true;
        blockCurrentTop.gameObject.SetActive(false);
        textscoreDiamond.SetText(scoreDiamond.ToString());
        textScoreTotal.SetText(Math.Round(BestScoreTotal).ToString() + DataManager.Instance.ListUnitBlock[unitBestScoreTotal].nameUnit);
        textScoreTotal1.SetText(Math.Round(BestScoreTotal).ToString() + DataManager.Instance.ListUnitBlock[unitBestScoreTotal].nameUnit);
        numberClickToSwap = 0;
    }
    // Update is called once per frame
    void Update()
    {
        checkEnds();
        if (blocks.Count > 0)
        {
           // Debug.Log("CountBlocks>0");
            firstValueBlock = blocks[0];
        }
        if (FunctionBonus.Instance.canSwap || FunctionBonus.Instance.canBreak)// canSwap or can break true
        {
            Debug.Log("CAN SWAP OR BREAK");
            if (PanelMainCanActive)
            {
                if (Input.touches.Length > 0)
                {                               
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        startTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100));
                        if (FunctionBonus.Instance.canSwap)// if false reset numberClickToSwwap
                        {
                            // check dk diamond > limit
                            if (scoreDiamond >= DataManager.Instance.UserData.BonusDiamond)
                            {
                                hit = Physics2D.Raycast(startTouchPos, Vector2.zero);
                                if (hit.collider != null)
                                {
                                    SoundController.Instance.PlaySoundCollect();
                                    Block blockSwap = hit.collider.gameObject.GetComponent<Block>();
                                    if (numberClickToSwap == 0)
                                    {
                                        blockSwap1 = blockSwap;
                                        blockSwap1.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
                                        {
                                            // blockfinal.transform.DOScale(1, 0.1f);
                                        });
                                        // DOscale block
                                        numberClickToSwap++;
                                    }
                                    else if (numberClickToSwap == 1)
                                    {
                                        blockSwap2 = blockSwap;
                                        if (blockSwap2 == blockSwap1)
                                        {
                                            numberClickToSwap = 1;
                                        }
                                        else
                                        {
                                            blockSwap2.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
                                            {
                                                Debug.Log("Swap 2 block");
                                                Block tempBlock = blockSwap1;
                                                blockSwap1 = blockSwap2;
                                                blockSwap2 = tempBlock;
                                                // Swap the block parents.
                                                Transform tempTransformParent1 = blockSwap1.transform.parent;
                                                Transform tempTransformParent2 = blockSwap2.transform.parent;
                                                blockSwap1.transform.parent = tempTransformParent2;
                                                blockSwap2.transform.parent = tempTransformParent1;
                                                scoreDiamond = scoreDiamond - DataManager.Instance.UserData.BonusDiamond;
                                                textscoreDiamond.SetText(scoreDiamond.ToString());
                                                DataManager.Instance.UserData.Diamond = scoreDiamond;
                                                numberClickToSwap = 0;
                                                blockSwap1.transform.DOScale(1f, 0.2f);
                                                blockSwap2.transform.DOScale(1f, 0.2f);
                                                // Delay0.1s
                                                DOVirtual.DelayedCall(0.2f, () =>
                                                {
                                                    FunctionBonus.Instance.SwapBlock.SetActive(false);
                                                    FunctionBonus.Instance.SetActiveBonusOff();
                                                    FunctionBonus.Instance.canSwap = false;
                                                });
                                             //   FunctionBonus.Instance.canSwap = false;
                                            });
                                        }
                                        //GameController.Instance.checkEnd = true;
                                    }
                                }

                            }
                            else
                            {
                              
                            }
                        }
                        else
                        {
                            numberClickToSwap = 0;                    
                        }
                        if (FunctionBonus.Instance.canBreak)
                        {
                            if (scoreDiamond >= DataManager.Instance.UserData.BonusDiamond)
                            {
                                hit = Physics2D.Raycast(startTouchPos, Vector2.zero);
                                if (hit.collider != null)
                                {
                                    SoundController.Instance.PlaySoundCollect();
                                    Block blockBreak = hit.collider.gameObject.GetComponent<Block>();
                            
                                    Vector3 posBreak  = new Vector3(blockBreak.transform.position.x, blockBreak.transform.position.y, FunctionBonus.Instance.IconHammer.transform.position.z);
                                    FunctionBonus.Instance.IconHammer.transform.DOMove(posBreak, 0.5f).OnComplete(() => {
                                        FunctionBonus.Instance.IconHammerAnim.transform.DORotate(new Vector3(0, 0, 33), 0.3f, RotateMode.LocalAxisAdd)
                                        .SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(0.2f).OnComplete(() =>
                                        {
                                            Destroy(blockBreak.gameObject);

                                            DOVirtual.DelayedCall(0.4f, () =>
                                            {
                                                FunctionBonus.Instance.Hammer.SetActive(false);
                                                FunctionBonus.Instance.SetActiveBonusOff();
                                                FunctionBonus.Instance.canBreak = false;
                                            }); 
                                            
                                        });
              
                                    });
                                    
                                    scoreDiamond = scoreDiamond - DataManager.Instance.UserData.BonusDiamond;
                                    textscoreDiamond.SetText(scoreDiamond.ToString());
                                    DataManager.Instance.UserData.Diamond = scoreDiamond;
                                    //DOVirtual.DelayedCall(1.0f, () =>
                                    //{
                                        
                                    //}); 
                                   // FunctionBonus.Instance.canBreak = false;
                                }
                            }
                            else
                            {
                                FunctionBonus.Instance.canBreak = false;
                            }
                        }
                    }
                }
            }
        }
        // cant swap and cant break
        else
        {
            Debug.Log("CAN NOT SWAP OR BREAK");
            if (PanelMainCanActive)
            {
                if (Input.touches.Length > 0)
                {
                    lineRend.enabled = true;
                    // textScoreCurrent.enabled = true;
                    textScoreTotal.enabled = false;
                    Touch touch = Input.GetTouch(0);
                    //Debug.Log("touchLenght>0");
                    if (touch.phase == TouchPhase.Began)
                    {
                        startTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100));                      
                        if (blocks.Count == 0)
                        {
                            hit = Physics2D.Raycast(startTouchPos, Vector2.zero);
                            if (hit.collider != null)
                            {
                                block = hit.collider.gameObject.GetComponent<Block>();
                                //DoScale
                                blocks.Add(block);
                                SoundController.Instance.PlaySoundCollect();
                                block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                                    block.transform.DOScale(1, 0.1f);
                                });
                                CaculatorBestScore(blocks);
                                bestScoreCurrent = CaculatorBestScore(blocks);
                                UnitConvert.Instance.convertUnitscoreCurrent(bestScoreCurrent, blocks[0].idUnitBlock);
                                block2 = block;
                                // Debug.Log("count of list blocks" + blocks.Count);
                                // });
                                // });
                            }
                            Debug.Log("hitCollider null");
                        }
                        else
                        {
                            blocks.Clear();
                            Debug.Log("ClearListBlocks");
                        }
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        GameController.Instance.canSetMaskHighest = false;
                        touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100));
                        hit = Physics2D.Raycast(touchPos, Vector2.zero);
                        //  renderLineCurrent(blocks, touchPos);
                        if (hit.collider != null)
                        {
                            block = hit.collider.gameObject.GetComponent<Block>();
                            renderLineCurrent(blocks, touchPos);
                            if (blocks.Count == 0)
                            {
                                // Debug.Log("Touch move BlockCount equal 0");
                                blocks.Add(block);
                                SoundController.Instance.PlaySoundCollect();
                                block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                                    block.transform.DOScale(1, 0.1f);
                                });
                                CaculatorBestScore(blocks);
                                bestScoreCurrent = CaculatorBestScore(blocks);
                                UnitConvert.Instance.convertUnitscoreCurrent(bestScoreCurrent, blocks[0].idUnitBlock);
                                block2 = block;
                                //   });
                                //  });                          
                            }
                            else
                            {
                                Vector2 vectorDistancePos = new Vector2(blocks[blocks.Count - 1].transform.position.x - touchPos.x, blocks[blocks.Count - 1].transform.position.y - touchPos.y);
                                float DistancePos = vectorDistancePos.magnitude;
                                //  Debug.Log("DistancePos" + DistancePos);
                                int absDistanceX = Convert.ToInt32(Math.Abs(blocks[blocks.Count - 1].transform.position.x - touchPos.x));
                                int absDistanceY = Convert.ToInt32(Math.Abs(blocks[blocks.Count - 1].transform.position.y - touchPos.y));
                                if (absDistanceX < maxDistance && absDistanceX > minDistance && absDistanceY < maxDistance || absDistanceY < maxDistance && absDistanceY > minDistance && absDistanceX < maxDistance)
                                {
                                    // Debug.Log("DistancePos" + DistancePos);
                                    if (blocks.Count == 1)
                                    {
                                        // if (block.valueBlock == firstValueBlock.valueBlock && block.idUnitBlock == firstValueBlock.idUnitBlock) // element2 == value of first element
                                        if (UnitConvert.Instance.compareValueBlock(block, firstValueBlock))
                                        {
                                            blocks.Add(block);
                                            SoundController.Instance.PlaySoundCollect();
                                            renderLineCurrent(blocks, touchPos);
                                            block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                                                block.transform.DOScale(1, 0.1f);
                                            });
                                            CaculatorBestScore(blocks);
                                            bestScoreCurrent = CaculatorBestScore(blocks);
                                            UnitConvert.Instance.convertUnitscoreCurrent(bestScoreCurrent, blocks[0].idUnitBlock);
                                            double bsc = CaculatorBestScore(blocks);
                                                                                  
                                            block2 = block;
                                            //Convert
                                            topBlock.valueBlock = bestScoreCurrent;
                                            topBlock.idUnitBlock = unitbestScoreCurrent;
                                            UnitConvert.Instance.convertUnit(topBlock);
                                            blockCurrentTop.image.color = DataManager.Instance.ListColorBlock[(idBlock0 + p) % 20];

                                            if ((idBlock0 + p) % 20 == 3 || (idBlock0 + p) % 20 == 4 || (idBlock0 + p) % 20 == 11 || (idBlock0 + p) % 20 == 15)
                                            {
                                                textScoreCurrent.color = Color.black;
                                            }
                                            else
                                            {
                                                textScoreCurrent.color = Color.white;
                                            }
                                            blockCurrentTop.gameObject.SetActive(true);
                                            //   });
                                            // });
                                        }
                                    }
                                    else //blocks has more than 1 element
                                    {
                                        //objects.Contains(go2)
                                        if (blocks.Contains(block))
                                        {
                                            if (block == blocks[blocks.Count - 2])
                                            {
                                                Debug.Log("Block check again");
                                                UnitConvert.Instance.convertUnit(blocks[blocks.Count - 1]);
                                                Debug.Log("ReConvert Unit Block finish");
                                                blocks.RemoveAt(blocks.Count - 1);
                                                // reconvert block unit 
                                                SoundController.Instance.PlaySoundCollect();
                                                block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                                                    block.transform.DOScale(1, 0.1f);
                                                });
                                                CaculatorBestScore(blocks);
                                                bestScoreCurrent = CaculatorBestScore(blocks);
                                                UnitConvert.Instance.convertUnitscoreCurrent(bestScoreCurrent, blocks[0].idUnitBlock);
                                                block2 = block;
                                                if (blocks.Count > 1)
                                                {
                                                    topBlock.valueBlock = bestScoreCurrent;
                                                    topBlock.idUnitBlock = unitbestScoreCurrent;
                                                    UnitConvert.Instance.convertUnit(topBlock);
                                                    blockCurrentTop.image.color = DataManager.Instance.ListColorBlock[(idBlock0 + p) % 20];
                                                    if ((idBlock0 + p) % 20 == 3 || (idBlock0 + p) % 20 == 4 || (idBlock0 + p) % 20 == 11 || (idBlock0 + p) % 20 == 15)
                                                    {
                                                        textScoreCurrent.color = Color.black;
                                                    }
                                                    else
                                                    {
                                                        textScoreCurrent.color = Color.white;
                                                    }

                                                    blockCurrentTop.gameObject.SetActive(true);
                                                }
                                                else
                                                {
                                                    blockCurrentTop.gameObject.SetActive(false);
                                                }
                                                //   });
                                                // });                                       
                                            }
                                            renderLineCurrent(blocks, touchPos);
                                        }
                                        else
                                        {
                                            //if ( block.valueBlock == block2.valueBlock || block.valueBlock == 2 * block2.valueBlock)
                                            if (UnitConvert.Instance.compareValueDoubleBlock(block2, block) || UnitConvert.Instance.compareValueBlock(block2, block))
                                            {
                                                blocks.Add(block);
                                                SoundController.Instance.PlaySoundCollect();
                                                renderLineCurrent(blocks, touchPos);
                                                block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                                                    block.transform.DOScale(1, 0.1f);
                                                });
                                                // block.transform.DOScale(1, 0.1f).OnComplete(() => {
                                                CaculatorBestScore(blocks);
                                                bestScoreCurrent = CaculatorBestScore(blocks);
                                                UnitConvert.Instance.convertUnitscoreCurrent(bestScoreCurrent, blocks[0].idUnitBlock);
                                                double bsc = CaculatorBestScore(blocks);
                                                        
                                                block2 = block;
                                                topBlock.valueBlock = bestScoreCurrent;
                                                topBlock.idUnitBlock = unitbestScoreCurrent;
                                                UnitConvert.Instance.convertUnit(topBlock);
                                                blockCurrentTop.image.color = DataManager.Instance.ListColorBlock[(idBlock0 + p) % 20];
                                                if ((idBlock0 + p) % 20 == 3 || (idBlock0 + p) % 20 == 4 || (idBlock0 + p) % 20 == 11 || (idBlock0 + p) % 20 == 15)
                                                {
                                                    textScoreCurrent.color = Color.black;
                                                }
                                                else
                                                {
                                                    textScoreCurrent.color = Color.white;
                                                }
                                                blockCurrentTop.gameObject.SetActive(true);
                                                // });
                                                // });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            renderLineCurrent(blocks, touchPos);
                        }    
                    }
                    else if (touch.phase == TouchPhase.Ended) //|| hit.collider == null)
                    {
                       // int CountListBlock = blocks.Count;
                        if (blocks.Count > 0)
                        {
                            if (blocks.Count == 1)
                            {
                                textScoreTotal.SetText(Math.Round(BestScoreTotal).ToString() + DataManager.Instance.ListUnitBlock[unitBestScoreTotal].nameUnit);
                            }
                            else
                            {
                                // checkShowText = false;
                                int CountListBlock = blocks.Count;
                                GameController.Instance.canMoveBlocktoGrid = false;                              
                                ////////////////////                               
                             

                                SoundController.Instance.PlaySoundCollectF();
                                if (DataManager.Instance.UserData.IsVibrateTurnOn)
                                {
                                    Vibrator.Vibrate(50);
                                }

                                Block blockfinal = blocks[blocks.Count - 1];
                                blockfinal.idUnitBlock = blocks[0].idUnitBlock;
                                blockfinal.valueBlock = bestScore;
                                blockfinal.naturalPart = bestScore;
                                // set idColor 
                                blockfinal.idColor = idBlock0 + p;
                                Debug.Log("Id Block[0] = " + idBlock0);
                                Debug.Log(" IdColor blockfinal = " + blockfinal.idColor);
                                UnitConvert.Instance.convertUnit(blockfinal);
                                PanelUnit.Instance.IncreasenumberUnit(blockfinal.idUnitBlock);
                                GameController.Instance.SetColorBlock(blockfinal);

                                DoMoveBlockFinal(CountListBlock);

                                blockfinal.transform.DOScale(1.2f, 0.1f).OnComplete(() =>
                                {
                                    blockfinal.transform.DOScale(1, 0.1f);
                            
                                });
                                Debug.Log("R" + blockfinal.colorBlock.r + "G" + blockfinal.colorBlock.g + "B" + blockfinal.colorBlock.b);
                                blockfinal.name = "Block" + blockfinal.naturalPart + DataManager.Instance.ListUnitBlock[blockfinal.idUnitBlock].nameUnit;
                                Debug.Log("NAMEBLOCK" + blockfinal.naturalPart + DataManager.Instance.ListUnitBlock[blockfinal.idUnitBlock].nameUnit);
                                blockfinal.text.SetText((Math.Floor(blockfinal.naturalPart)).ToString() + DataManager.Instance.ListUnitBlock[blockfinal.idUnitBlock].nameUnit);

                                Debug.Log("IDcolor FInalBlock" + blockfinal.idColor);
                                Debug.Log("IDcolor HighestBlock " + highestBlock.idColor);
                                if (blockfinal.idColor > highestBlock.idColor)

                                {
                                    Debug.Log("IDcolor FianlBLOCK > id Color HighestBlock");
                                    if (blockfinal.idColor == 10)
                                    {
                                        DataManager.Instance.UserData.BonusDiamond = 240;
                                        Debug.Log("2048 BonusDiamond" + DataManager.Instance.UserData.BonusDiamond);
                                    }
                                    else if (blockfinal.idColor > 10)
                                    {
                                        if (blockfinal.idColor % 2 == 0)//chan
                                        {
                                            DataManager.Instance.UserData.BonusDiamond = 240 + 10 * (blockfinal.idColor - 10) / 2;
                                            Debug.Log("2048 BonusDiamond" + DataManager.Instance.UserData.BonusDiamond);

                                        }
                                        
                                        else {//le 
                                        }
                                    }
                                    else { }
                                    FunctionBonus.Instance.TextSwapBonusDiamond.SetText(DataManager.Instance.UserData.BonusDiamond.ToString());
                                    FunctionBonus.Instance.TextBreakBonusDiamond.SetText(DataManager.Instance.UserData.BonusDiamond.ToString());
                                    PanelGameOver.Instance.TextSwapBonusDiamond.SetText(DataManager.Instance.UserData.BonusDiamond.ToString());
                                    PanelGameOver.Instance.TextBreakBonusDiamond.SetText(DataManager.Instance.UserData.BonusDiamond.ToString());
                                }
                                else
                                {
                                }
                                GameController.Instance.SetNewHighestBlock(blockfinal);                                
                                UnitConvert.Instance.AddScoreTotal(BestScoreTotal, unitBestScoreTotal, bestScoreCurrent, unitbestScoreCurrent);                              
                            }
                            textScoreTotal.SetText(Math.Round(BestScoreTotal).ToString() + DataManager.Instance.ListUnitBlock[unitBestScoreTotal].nameUnit);
                            textScoreTotal1.SetText(Math.Round(BestScoreTotal).ToString() + DataManager.Instance.ListUnitBlock[unitBestScoreTotal].nameUnit);
                            removeRenderLineCurrent(blocks.Count + 1);
                            blocks.Clear();
                            lineRend.enabled = false;
                            // textScoreCurrent.enabled = false;
                            textScoreTotal.enabled = true;
                            blockCurrentTop.gameObject.SetActive(false);
                            DataManager.Instance.UserData.BestScoreTotal = BestScoreTotal;
                            DataManager.Instance.UserData.UnitBestScoreTotal = unitBestScoreTotal;
                            DataManager.Instance.UserData.Diamond = scoreDiamond;
                        }
                    }
                }
            }
        }
    }
    public double CaculatorBestScore(List<Block> blocks)
    {

        Debug.Log("CaculatorBestCurrent");
        if (blocks.Count > 0)
        {
            for(int i = 1; i<blocks.Count; i++)
            {
                UnitConvert.Instance.convertUnitListBlock(blocks[0], blocks[i]);
            }       
            double Sum = 0;
            bestScore = 0;
            p = 0;
            idBlock0 = blocks[0].idColor;
            for (int k = 0; k < blocks.Count; k++)
            {

                if (k == 0)
                {
                    Sum = blocks[0].valueBlock;
                    bestScore = blocks[0].valueBlock;
                }
                else
                {
                    Sum = Sum + blocks[k].valueBlock;

                    if (Sum > bestScore)
                    {
                        p = p + 1;
                    }
                    bestScore = Math.Round(blocks[0].valueBlock * Math.Pow(2, p), 9);
                }
            }
        }       
        bestScoreCurrent = bestScore;
        return (bestScore);
    }
    public void renderLineCurrent(List<Block> blocks, Vector3 touchPos)
    {
        Debug.Log("Linerendercurrent");
        lineRend.positionCount = blocks.Count + 1;  
        if (blocks.Count == 0)
        { 
        }
        else 
        {            
            for (int i=0; i<blocks.Count; i++ ) 
            {
                lineRend.SetPosition(i, new Vector3(blocks[i].transform.position.x, blocks[i].transform.position.y, 102f));
            }            
            lineRend.SetPosition(blocks.Count, new Vector3(touchPos.x, touchPos.y, 102f));
        }    
     }
    public void removeRenderLineCurrent(int numberVertextRenderline)// set equal blocks.Count + 1
    {
        if (numberVertextRenderline > 1)
        {
            for (int i = 0; i < numberVertextRenderline; i++)
            {
                lineRend.positionCount = numberVertextRenderline;          
                lineRend.SetPosition(i, new Vector3(-1000f, 0f, 102f));
            }
        }
    }
    public void checkEnds()
    {
        moveTime -= Time.deltaTime;
        if (moveTime >= 0) return;
        moveTime = timer;
        if (FunctionBonus.Instance.canBreak || FunctionBonus.Instance.canSwap)
        {

        }
        else {             
              if (GameController.Instance.checkEnd == false)
              {
                 GameController.Instance.checkEnd = true;
              }
        }
    }
   
  
    void DoMoveBlockFinal(int CountListBlock)
    {
    
        if (CountListBlock > 0)
        {
            GameObject fxBlock = Instantiate(FxMergeBlock);
            fxBlock.transform.position = new Vector3(blocks[CountListBlock - 1].transform.position.x, blocks[CountListBlock - 1].transform.position.y, 0);

            for (int i = 0; i < CountListBlock - 1; i++)
            {
                Block block = blocks[i];
               // Debug.Log($"blocks[{i}] = {blocks[i].gameObject.name}");
                block.gameObject.transform.DOMove(blocks[CountListBlock - 1].transform.position, 0.2f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        Debug.Log("Block  destroyed......................"); 
                        Destroy(block.gameObject);
                        if (i.Equals(CountListBlock - 1))
                        {

                            //  blocks[CountListBlock - 1].transform.DOScale(1.2f, 0.1f).OnComplete(() =>
                            // {
                            //     blocks[CountListBlock - 1].transform.DOScale(1, 0.1f);

                            //  });
                           
                            GameController.Instance.canMoveBlocktoGrid = true;
                           
                                PanelTextFXController.Instance.checkTextFX(CountListBlock);
                            
                        }

                    });
           
            }           
        }
    }     
}
