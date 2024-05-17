using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class tutorials : Singleton<tutorials>
{
    public Transform point1;
    public Transform point2;
    public Block blockPb;
    private LineRenderer lineRend;
    private Block block2;
    private Block block;
    RaycastHit2D hit;
    private Vector3 touchPos;
    private Vector3 startTouchPos;
    public List<Block> blocks = new List<Block>();
    public double BestScoreCrr;
    public double bestScore;
    public GameObject handTutorial;
    [SerializeField] public float moveTime;
    public float timer;
    public int p;
    public int idBlock0;

    void Start()
    {
        Debug.Log("CLONE BLOCK");
        Block blockClone = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
        blockClone.naturalPart = 2;
        blockClone.valueBlock = 2;
        blockClone.idUnitBlock = 0;
        blockClone.idColor = 0;
        blockClone.colorBlock = DataManager.Instance.ListColorBlock[blockClone.idColor % 20];
        blockClone.text.SetText(blockClone.naturalPart.ToString());
       //  blockClone.text.SetText(Math.Floor(blockClone.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone.idUnitBlock].nameUnit);
         blockClone.transform.parent = point1;
        blockClone.transform.localPosition = Vector3.zero;
        blockClone.transform.localScale = new Vector3(1, 1, 1);
        Block blockClone1 = Instantiate(blockPb, Vector3.zero, Quaternion.identity).GetComponent<Block>();
        blockClone1.naturalPart = 2;
        blockClone1.valueBlock = 2; 
        blockClone1.idUnitBlock = 0;
        blockClone1.idColor = 0;
        blockClone1.colorBlock = DataManager.Instance.ListColorBlock[blockClone1.idColor % 20];
        blockClone1.text.SetText(blockClone1.naturalPart.ToString());
        //blockClone1.text.SetText(Math.Floor(blockClone1.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockClone1.idUnitBlock].nameUnit);
        blockClone1.transform.parent = point2;
        blockClone1.transform.localPosition = Vector3.zero;
        blockClone1.transform.localScale = new Vector3(1, 1, 1);
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;
        bestScore = 0;
        BestScoreCrr = 0;
        moveTime = 1;
        timer = 2.5f;      
    }
    void Update()
    {
        moveHand();

        if (blocks.Count > 0)
        {          
        }
      
            if (Input.touches.Length > 0)
            {
              lineRend.enabled = true;
               
                Touch touch = Input.GetTouch(0);
               
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100));   
                    if (blocks.Count == 0)
                    {

                        hit = Physics2D.Raycast(startTouchPos, Vector2.zero);
                        if (hit.collider != null)
                        {
                            Debug.Log("hitCollider not null");                          
                            block = hit.collider.gameObject.GetComponent<Block>();
                            Debug.Log("name block collider" + hit.collider.gameObject.name);
                            blocks.Add(block);
                            SoundController.Instance.PlaySoundCollect();
                        block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                            block.transform.DOScale(1, 0.1f);
                        });
                        BestScoreCrr = CaculatorBestScore(blocks);
                            block2 = block;
                        }
                    }
                    else
                    {
                        blocks.Clear();
                      
                    }

                }
                if (touch.phase == TouchPhase.Moved)
                {
                    touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100));

                    hit = Physics2D.Raycast(touchPos, Vector2.zero);
                    if (hit.collider != null)
                    {

                        block = hit.collider.gameObject.GetComponent<Block>();
                        renderLineCurrent(blocks, touchPos);
                        if (blocks.Count == 0)
                        {                         
                            blocks.Add(block);
                        SoundController.Instance.PlaySoundCollect();
                        block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                            block.transform.DOScale(1, 0.1f);
                        });
                        BestScoreCrr =  CaculatorBestScore(blocks);
                            block2 = block;
                        }
                        else
                        {
                    
                            Vector2 vectorDistancePos = new Vector2(blocks[blocks.Count - 1].transform.position.x - touchPos.x, blocks[blocks.Count - 1].transform.position.y - touchPos.y);
                            float DistancePos = vectorDistancePos.magnitude;
                            Debug.Log("DistancePos" + DistancePos);
                          //  if (DistancePos < 117 && DistancePos > 60 )
                           // if (DistancePos < 1.17 && DistancePos > 0.60)
                            if (DistancePos < 1.35 && DistancePos > 0.50)
                            {
                                Debug.Log("DistancePos in DK" + DistancePos);
                                if (blocks.Count == 1)
                                {
                                   if (block.valueBlock == blocks[0].valueBlock )
                                    {
                                     blocks.Add(block);
                                    SoundController.Instance.PlaySoundCollect();
                                     renderLineCurrent(blocks, touchPos);
                                    block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                                        block.transform.DOScale(1, 0.1f);
                                    });
                                    // CaculatorBestScore(blocks);
                                    BestScoreCrr =  CaculatorBestScore(blocks);
                                    block2 = block;
                                    }
                                }

                                else
                                {
                                  
                                    if (blocks.Contains(block))
                                    {
                                        if (block == blocks[blocks.Count - 2])
                                        {
                                            Debug.Log("Block check again");
                                            blocks.RemoveAt(blocks.Count - 1);
                                        SoundController.Instance.PlaySoundCollect();
                                        block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                                            block.transform.DOScale(1, 0.1f);
                                        });
                                        BestScoreCrr =  CaculatorBestScore(blocks);
                                            block2 = block;
                                        }
                                        renderLineCurrent(blocks, touchPos);
                                    }
                                    else
                                    {

                                        if (block.valueBlock == block2.valueBlock || block.valueBlock == 2 * block2.valueBlock)
                                        {
                                            Debug.Log("value block 2 equal or double block1");
                                            blocks.Add(block);
                                        SoundController.Instance.PlaySoundCollect();
                                            renderLineCurrent(blocks, touchPos);
                                        block.transform.DOScale(1.1f, 0.1f).OnComplete(() => {
                                            block.transform.DOScale(1, 0.1f);
                                        });
                                        BestScoreCrr = CaculatorBestScore(blocks);
                                            block2 = block;
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
                else if (touch.phase == TouchPhase.Ended) 
                {

                    if (blocks.Count > 0)
                    {                    
                       
                            for (int i = 0; i < blocks.Count - 1; i++)
                            {
                                Debug.Log("Destroy Game Object ");
                                Destroy(blocks[i].gameObject);
                            }
                            SoundController.Instance.PlaySoundCollectF();
                            Block blockfinal = blocks[blocks.Count - 1];
                            blockfinal.idUnitBlock = 0; 
                            blockfinal.valueBlock = BestScoreCrr;
                            blockfinal.naturalPart = blockfinal.valueBlock;
                            blockfinal.idColor = p +idBlock0;
                            blockfinal.colorBlock = DataManager.Instance.ListColorBlock[blockfinal.idColor];
                            blockfinal.transform.DOScale(1.2f, 0.1f).OnComplete(() => {
                            blockfinal.transform.DOScale(1, 0.1f);
                             });
                            blockfinal.name = "Block" + blockfinal.naturalPart + DataManager.Instance.ListUnitBlock[blockfinal.idUnitBlock].nameUnit;
                            blockfinal.text.SetText(blockfinal.naturalPart.ToString());
                        
                        removeRenderLineCurrent(blocks.Count + 1);
                        blocks.Clear();
                        lineRend.enabled = false;
                        if (BestScoreCrr == 4)
                        {
                        DOVirtual.DelayedCall(1f, () =>
                        {
                            start.Instance.Panel1.SetActive(false);
                            start.Instance.Panel2.SetActive(true);
                        });
                        
                    }
                      
                 }                            
            }

        }
    }


    public double CaculatorBestScore(List<Block> blocks)
    {
        Debug.Log("CaculatorBestCurrent");
     
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

        return (bestScore);
    }

    public void renderLineCurrent(List<Block> blocks, Vector3 touchPos)
    {
        Debug.Log("Linerendercurrent");
        lineRend.positionCount = blocks.Count + 1;

        if (blocks.Count == 0)
        {
            // no action
        }

        else
        {

            for (int i = 0; i < blocks.Count; i++)
            {
                lineRend.SetPosition(i, new Vector3(blocks[i].transform.position.x, blocks[i].transform.position.y, 100f));
            }
            lineRend.SetPosition(blocks.Count, new Vector3(touchPos.x, touchPos.y, 100f));
        }

    }

    public void removeRenderLineCurrent(int numberVertextRenderline)
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
    public void moveHand()
    {
        moveTime -= Time.deltaTime;
        if (moveTime >= 0) return;
        moveTime = timer;
        handTutorial.transform.DOMove( point1.position, 0.2f).OnComplete(() => {
            handTutorial.transform.DOMove(point2.position, 1f);
        });
    }

}
