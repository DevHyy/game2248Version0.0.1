using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class UnitConvert : Singleton<UnitConvert>
{
   
    // in function start add element in the list 

    public void convertUnit(Block blockConvert)
    {
        //convert value unit less than to unit more than
        // if block has unit NULL
        if (blockConvert.idUnitBlock == 0)
        {
         //   int n = 0;
            if (blockConvert.valueBlock >= 10000)
            {
               // n = -1;
                blockConvert.valueBlock = Math.Round(blockConvert.valueBlock * Math.Pow(10, -3),6);
                blockConvert.naturalPart = blockConvert.valueBlock;
                blockConvert.idUnitBlock = 1;
            }   

        }
        // with block has unit equal or more than K
        if(blockConvert.idUnitBlock> 0)
        {
           // int n = 0;
            // with block has unit more than K (1000)
            while (blockConvert.valueBlock >= 1000)
            {
                //  n = n - 1;
                // blockConvert.valueBlock = Math.Round(blockConvert.valueBlock * Math.Pow(Math.Pow(10, 3), n), 6);
                blockConvert.valueBlock = Math.Round(blockConvert.valueBlock * Math.Pow(10, -3), 6);
                blockConvert.naturalPart= blockConvert.valueBlock;
                blockConvert.idUnitBlock += 1;// search name unit in list by index equal idUnit
                //     blockConvert.idUnitBlock.nameUnit = DataManager.Instance.ListUnitBlock[blockConvert.UnitBlock.idUnit].nameUnit;
                Debug.Log("value and idunit after convert : "+ blockConvert.valueBlock+ blockConvert.idUnitBlock);
            }
        }
        gettextBlock(blockConvert);
        Debug.Log("go to block convertUnit");
    }
    public void gettextBlock(Block blockConvert)
    {
        blockConvert.text.SetText(Math.Floor(blockConvert.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockConvert.idUnitBlock].nameUnit);
        GameController.Instance.SetColorBlock(blockConvert);
        Debug.Log("TextBlock" + Math.Floor(blockConvert.naturalPart).ToString() + DataManager.Instance.ListUnitBlock[blockConvert.idUnitBlock].nameUnit);
    }
    /* public bool compareValueBlock(Block block1, Block block2)// so sanh bang
     {
         // add dk value block >= 1 check value 2 block > = 1and <= 1000  true   go to ss  dont go to convert
       if(block1.valueBlock >= 1 && block2.valueBlock >= 1)
       {
         if (block1.idUnitBlock != block2.idUnitBlock)// khac unit  convert
         {
             if (block1.idUnitBlock <= block2.idUnitBlock)
             {
                 int sub = block2.idUnitBlock - block1.idUnitBlock;

                 //   if (block1.valueBlock == block2.valueBlock * Math.Pow(Math.Pow(10, 3), sub))
                 if (block1.valueBlock * Math.Pow(Math.Pow(10, 3),(-1)*sub)- 0.5 <= block2.valueBlock && block1.valueBlock * Math.Pow(Math.Pow(10, 3), (-1) * sub) + 0.5 >= block2.valueBlock)
                 {
                     return true;
                 }
                 return false;  
             }
             else
             {
                 int sub = block1.idUnitBlock - block2.idUnitBlock;
                 // if (block2.valueBlock == block1.valueBlock * Math.Pow(Math.Pow(10, 3), sub))
                 if (block2.valueBlock * Math.Pow(Math.Pow(10, 3), (-1) * sub) - 0.5 <= block1.valueBlock && block2.valueBlock * Math.Pow(Math.Pow(10, 3), (-1) * sub) + 0.5 >= block1.valueBlock)

                 {
                     return true;
                 }
                 return false;
             }
         }
         // if (block1.naturalPart == block2.naturalPart && block1.idUnitBlock == block2.idUnitBlock)
         if (block1.naturalPart >= block2.naturalPart - 0.5 && block1.naturalPart <= block2.naturalPart + 0.5 && block1.idUnitBlock == block2.idUnitBlock)
         {
             return true;
         }
         else
         {
             return false;
         }
       }
       else
       {
             return false;
         }

     }
    */
    public bool compareValueBlock(Block block1, Block block2)// so sanh bang
    {
        // add dk value block >= 1 check value 2 block > = 1and <= 1000  true   go to ss  dont go to convert
        convertUnit(block1);
        convertUnit(block2);
        if (block1.idColor ==block2.idColor)
        {         
                return true;          
        }
        else
        {
            return false;        
        }
    }


    public bool compareValueDoubleBlock(Block block1, Block block2)// ss gap 2 
    {
        // add dk value block >= 1 check value 2 block > = 1and <= 1000  true   go to ss  dont go to convert
        convertUnit(block1);
        convertUnit(block2);
        if ((block1.idColor + 1) == block2.idColor)
        {
           
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
    public bool compareValueBlock(Block block1, Block block2)// so sanh bang
    {
        // add dk value block >= 1 check value 2 block > = 1and <= 1000  true   go to ss  dont go to convert
        convertUnit(block1);
        convertUnit(block2);
        if (block1.valueBlock >= 1 && block2.valueBlock >= 1 && block2.idUnitBlock >= block1.idUnitBlock)
        {
            int sub = block2.idUnitBlock - block1.idUnitBlock;
            if (block1.valueBlock * Math.Pow(Math.Pow(10, 3), (-1) * sub) - 0.5 <= block2.valueBlock && block1.valueBlock * Math.Pow(Math.Pow(10, 3), (-1) * sub) + 0.5 >= block2.valueBlock)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }


    public bool compareValueDoubleBlock(Block block1, Block block2)// ss gap 2 
    {
        // add dk value block >= 1 check value 2 block > = 1and <= 1000  true   go to ss  dont go to convert
        convertUnit(block1);
        convertUnit(block2);
        if (block1.valueBlock >= 1 && block2.valueBlock >= 1 && block2.idUnitBlock >= block1.idUnitBlock)
        {
            int sub = block2.idUnitBlock - block1.idUnitBlock;
            if (block2.naturalPart >= (2 * block1.naturalPart) * Math.Pow(Math.Pow(10, 3), (-1) * sub) - 0.5 && block2.naturalPart <= (2 * block1.naturalPart) * Math.Pow(Math.Pow(10, 3), (-1) * sub) + 0.5)

            //  if (block1.valueBlock * Math.Pow(Math.Pow(10, 3), (-1) * sub) - 0.5 <= block2.valueBlock && block1.valueBlock * Math.Pow(Math.Pow(10, 3), (-1) * sub) + 0.5 >= block2.valueBlock)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    */
    // convert unit of block

      //changeto ss id color 
    public void convertUnitListBlock(Block blocktarget, Block blockRoot)
    {
        int IdUnitblockTarget = blocktarget.idUnitBlock;
        int IdUnitblockRoot = blockRoot.idUnitBlock;
        int n = IdUnitblockRoot - IdUnitblockTarget;
        blockRoot.naturalPart = blockRoot.naturalPart * Math.Pow(Math.Pow(10, 3), n);//
        blockRoot.valueBlock = blockRoot.naturalPart;
        blockRoot.idUnitBlock = blocktarget.idUnitBlock;       
        // if create list return n dont convert
    }
    public void convertUnitscoretotal(double valuescore, int idUnitscore)
    {
      //  while (valuescore >= 1000000000)
          while (valuescore >= 1000000)
            {
            valuescore = Math.Round(valuescore * Math.Pow(10, -3), 6);
            idUnitscore += 1;
        }
        DrawLine.Instance.BestScoreTotal = valuescore;
        DrawLine.Instance.unitBestScoreTotal = idUnitscore; 
        DrawLine.Instance.textScoreTotal.SetText(Math.Round(DrawLine.Instance.BestScoreTotal).ToString() + DataManager.Instance.ListUnitBlock[DrawLine.Instance.unitBestScoreTotal].nameUnit);     
    }
    public void convertUnitscoreCurrent(double valuescore, int idUnitscore)
    {
        if (idUnitscore == 0)
        {       
            if (valuescore >= 10000)
            {
                valuescore = Math.Round(valuescore * Math.Pow(10, -3), 6);
                idUnitscore += 1;
            }
        }  
        if (idUnitscore > 0)
        {
            while (valuescore >= 1000)
            {
                valuescore = Math.Round(valuescore * Math.Pow(10, -3), 6);
                idUnitscore += 1;
            }
        }
        DrawLine.Instance.bestScoreCurrent = valuescore;
        DrawLine.Instance.unitbestScoreCurrent = idUnitscore;
        DrawLine.Instance.textScoreCurrent.SetText(Math.Floor(DrawLine.Instance.bestScoreCurrent).ToString() + DataManager.Instance.ListUnitBlock[DrawLine.Instance.unitbestScoreCurrent].nameUnit);
    }
    public void AddScoreTotal(double BestScoreTotal, int UnitBestScoreTotal, double bestScoreCurrrent, int UnitbestScoreCurrent)
    {

       // int UnitMin = 0;
        if (UnitBestScoreTotal <= UnitbestScoreCurrent)
        {
            int sub = UnitbestScoreCurrent - UnitBestScoreTotal;
            bestScoreCurrrent = bestScoreCurrrent * Math.Pow(Math.Pow(10, 3), sub);
            UnitbestScoreCurrent = UnitbestScoreCurrent - sub;
                    
        }
        else {
            int sub = UnitBestScoreTotal - UnitbestScoreCurrent;
            BestScoreTotal = BestScoreTotal * Math.Pow(Math.Pow(10, 3), sub);
            UnitBestScoreTotal = UnitBestScoreTotal - sub;            
         }
        BestScoreTotal = BestScoreTotal + bestScoreCurrrent;
        convertUnitscoretotal(BestScoreTotal, UnitBestScoreTotal);
    }
}