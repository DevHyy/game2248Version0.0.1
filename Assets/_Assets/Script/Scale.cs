using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour
{
    public GameObject IconFree;
    public float moveTime;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        moveTime = 1f;
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        moveTime -= Time.deltaTime;
        if (moveTime >= 0) return;
        moveTime = timer;
        // call function Scale 
        Debug.Log("Call  fun scaleIconFree");
        scaleIconFree();
    }
    public void scaleIconFree()
    {
        // Start the scaling animation in a loop
        // IconFree.transform.DOScale(1.5f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).Play();
        IconFree.transform.DOScale(1.3f, 1f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).Play();
    }

}
