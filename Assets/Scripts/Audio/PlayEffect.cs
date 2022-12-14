using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 音樂播放器
public class PlayEffect : MonoBehaviour         // Prefabs/PlayEffect 
{
    public int MyID;
    private void Awake()
    {
        this.enabled = false;                   // 暫時關閉腳本
    }

    private void Update()
    {
        // 若音樂片段播放完畢 且 沒有暫停
        if(!GetComponent<AudioSource>().isPlaying && !PauseCtl.self.PauseBool)
        {
            EffectPlayer.self.audios.RemoveAt(MyID);
            EffectPlayer.self.ResetID();
            // 就刪除物件
            Destroy(gameObject);
        }
    }
}
