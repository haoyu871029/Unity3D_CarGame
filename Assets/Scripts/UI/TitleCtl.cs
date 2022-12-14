using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 標題的腳本
public class TitleCtl : MonoBehaviour   // GameUI/Title
{
    public CarCtl Car;                      // 玩家控制器
    public SpawnEnemyCtl SpawnEnemy;        // 敵人生產器
    public FuelCtl Fuel;                    // 控制消耗油量
    public GameObject TitleText;            // 標題文字
    public GameObject CountDownText;        // 倒數文字
    public PauseCtl Pause;                  // 控制暫停

    public static TitleCtl self;
    private void Awake()                    // 一定要放在Awake(一定要比Start快)
    {
        self = this;

        Car.BG.speed = 0;                   // 事先將背景的播放速度變成0
        Fuel.ShowFuel();                    // 事先顯示油量

        StartGame(false);                   // 一開始先暫停遊戲
    }
    private void Update()
    {
        // 如果按下大Enter鍵或小Enter鍵
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CountDownText.SetActive(true);  // 顯示倒數文字
            TitleText.SetActive(false);     // 隱藏標題文字
            this.enabled = false;
        }
    }
    public void StartGame(bool start)              // 開始 / 暫停遊戲
    {
        Car.enabled = start;                // 開啟 / 關閉玩家控制器
        SpawnEnemy.enabled = start;         // 開啟 / 關閉敵人生產器
        Fuel.enabled = start;               // 開啟 / 關閉控制消耗油量
        Pause.enabled = start;              // 開啟 / 關閉控制暫停腳本
    }
}
