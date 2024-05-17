using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpinWheel : Singleton<SpinWheel>
{
    public List<int> Score;
    public List<Transform> PointRewardX;
    public List<Transform> Pos;// element point start and point end
    public Transform Arrow;
    //  public bool StopArrow;

    private float Distance_01;
    private float Distance_02;
    private float Distance_03;
    private float Distance_04;
    private float Distance_05;
    private float Distance_06;
    private float Distance_07;
    private float DistanceArrow;
    //public int vlCheckResult;
    //private void Start()
    //{
    //    //  StopArrow = false;
    //    // vlCheckResult = 1;


    //}

    //void OnEnable()
    //{
        
    //    //StartCoroutine( StartSpin());

    //}
    public void caculatorDistance()
    {
        if (PointRewardX.Count == 8)
        {

            Distance_01 = Mathf.Abs(PointRewardX[0].position.x - PointRewardX[1].position.x);
            Distance_02 = Mathf.Abs(PointRewardX[0].position.x - PointRewardX[2].position.x);
            Distance_03 = Mathf.Abs(PointRewardX[0].position.x - PointRewardX[3].position.x);
            Distance_04 = Mathf.Abs(PointRewardX[0].position.x - PointRewardX[4].position.x);
            Distance_05 = Mathf.Abs(PointRewardX[0].position.x - PointRewardX[5].position.x);
            Distance_06 = Mathf.Abs(PointRewardX[0].position.x - PointRewardX[6].position.x);
            Distance_07 = Mathf.Abs(PointRewardX[0].position.x - PointRewardX[7].position.x);
        }
    }
    public void StartSpin()
    {
        caculatorDistance();
        //yield return new WaitForSeconds(0.5f);
        // caculator distance  from start to position of each element in list PointRewardX          
        List<Vector3> path = new List<Vector3>();
        Debug.Log("Call before arrow.position");
        Arrow.position = new Vector3(Pos[0].position.x, Arrow.position.y, Arrow.position.z);
        for (int i = 0; i < Pos.Count; i++)
        {
            Vector3 posTarget = new Vector3(Pos[i].position.x, Arrow.position.y, Arrow.position.z);
            path.Add(posTarget);
        }
        Arrow.DOPath(path.ToArray(), 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).OnWaypointChange(point =>
        {
            Debug.Log("Call in loop DOPath");
        }).OnUpdate(() =>
        {
            NewBlockUnlockController.Instance.TextScore.SetText(CheckResult().ToString());
        });
    }




    public int CheckResult()
    {
        Debug.Log("Call CheckResult");
        DistanceArrow = Mathf.Abs(PointRewardX[0].position.x - Arrow.position.x);
        int valueScore = 1;
        if (PointRewardX.Count == 8)
        {
            if (DistanceArrow >= 0 && DistanceArrow < Distance_01)
            {
                valueScore = Score[0];
            }
            else if (DistanceArrow >= Distance_01 && DistanceArrow < Distance_02)
            {
                valueScore = Score[1];
            }
            else if (DistanceArrow >= Distance_02 && DistanceArrow < Distance_03)
            {
                valueScore = Score[2];
            }
            else if (DistanceArrow >= Distance_03 && DistanceArrow < Distance_04)
            {
                valueScore = Score[3];
            }
            else if (DistanceArrow >= Distance_04 && DistanceArrow < Distance_05)
            {
                valueScore = Score[4];
            }
            else if (DistanceArrow >= Distance_05 && DistanceArrow < Distance_06)
            {
                valueScore = Score[5];
            }
            else if (DistanceArrow >= Distance_06 && DistanceArrow < Distance_07)
            {
                valueScore = Score[6];
            }//return 0;
        }
        return valueScore;
    }

}