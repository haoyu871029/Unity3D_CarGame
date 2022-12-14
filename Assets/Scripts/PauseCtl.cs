using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制暫停
public class PauseCtl : MonoBehaviour           //Pause
{
    public bool PauseBool = false;             //暫停 / 開始遊戲

    public static PauseCtl self;
    private void Awake()
    {
        self = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))    //如果按下大Enter鍵
        {
            PauseBool = !PauseBool;             //暫停 / 開始遊戲
            Pause();                            //主要控制暫停的函式
        }
    }

    void Pause()
    {
        //Time.timeScale: 實際遊戲運行時間(1倍)
        Time.timeScale = PauseBool ? 0 : 1;     //如果要暫停 遊戲運行時間改為0倍 否則改為1倍
        EffectPlayer.self.Pause(PauseBool);     //暫停 / 開始音效播放
    }
}
