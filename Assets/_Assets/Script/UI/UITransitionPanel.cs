using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITransitionPanel : MonoBehaviour
{
    public UnityEvent OnTransitionCompleted;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }
    private void OnEnable()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.SetEase(Ease.Linear);
        mySequence.Append(transform.DOScale(1, 0.3f));
        mySequence.OnComplete(()=>OnTransitionCompleted?.Invoke());
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }

    public void OnCloseClick()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.SetEase(Ease.Linear);
        mySequence.Append(transform.DOScale(0, 0.3f));
        mySequence.OnComplete(()=> {
            transform.parent.gameObject.SetActive(false);
        });
    }
}
