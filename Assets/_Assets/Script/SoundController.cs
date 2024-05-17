using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundController : Singleton<SoundController>
{
    public AudioSource SoundCollect;
    public AudioSource SoundCollectF;
    public AudioSource SoundCompleteMove;
    public AudioSource SoundShowText;
    public AudioSource SoundShowUnlock;
    public AudioSource SoundShowPanel;
    public AudioSource SoundPanelLiminated;
    public AudioSource SoundGameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame

        public void PlaySoundCollect()
        {
            if (DataManager.Instance.UserData.IsSoundTurnOn)
            {
                SoundCollect.Play();
            }
            else
            {
                SoundCollect.Stop();
            }
        }
        public void PlaySoundCollectF()
        {
            if (DataManager.Instance.UserData.IsSoundTurnOn)
            {
                SoundCollectF.Play();
            }
            else
            {
                SoundCollectF.Stop();
            }
        }
        public void PlaySoundGameOver()
        {
            if (DataManager.Instance.UserData.IsSoundTurnOn)
            {
                SoundGameOver.Play();
            }
            else
            {
                SoundGameOver.Stop();
            }
        }
        public void PlaySoundCompleteMove()
        {
            if (DataManager.Instance.UserData.IsSoundTurnOn)
            {
                SoundCompleteMove.Play();
            }
            else
            {
                SoundCompleteMove.Stop();
            }
        }
        public void PlaySoundShowText()
        {
            if (DataManager.Instance.UserData.IsSoundTurnOn)
            {
                SoundShowText.Play();
            }
            else
            {
                SoundShowText.Stop();
            }
        }
        public void PlaySoundShowUnlock()
        {
            if (DataManager.Instance.UserData.IsSoundTurnOn)
            {
                SoundShowUnlock.Play();
            }
            else
            {
                SoundShowUnlock.Stop();
            }
        }
        public void PlaySoundShowPanel()
        {
            if (DataManager.Instance.UserData.IsSoundTurnOn)
            {
                SoundShowPanel.Play();
            }
            else
            {
                SoundShowPanel.Stop();
            }
        }
    public void PlaySoundPanelLiminated()
    {
        if (DataManager.Instance.UserData.IsSoundTurnOn)
        {
            SoundPanelLiminated.Play();
        }
        else
        {
            SoundPanelLiminated.Stop();
        }
    }        
}
