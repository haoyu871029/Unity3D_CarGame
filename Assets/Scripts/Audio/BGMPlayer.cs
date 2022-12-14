using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BGM控制器
public class BGMPlayer : MonoBehaviour                  // Prefabs/NeverDestroyObj/BGMPlayer
{
    public static BGMPlayer self;
    private void Awake()
    {
        self = this;
    }

    AudioSource aud;                                    // 音樂播放器

    private const string BGMPath = "music/BGM/";        // BGM路徑

    private void Start()
    {
        aud = GetComponent<AudioSource>();              // 取得音樂播放器
    }

    public void PlayBGM(string name)                    // 播放BGM(BGM名稱)
    {
        // 更改音樂播放器的音樂片段
        aud.clip = Resources.Load<AudioClip>(BGMPath + name);
        aud.Play();                                     // 真正的播放指令
    }
}
