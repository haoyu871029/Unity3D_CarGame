using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判斷玩家是否經過一圈
public class RoundCtl : MonoBehaviour   // GameScene/BG
{
    public static RoundCtl self;        // 建構是
    private void Awake()
    {
        self = this;
    }

    public GameObject EndLine;          // 終點線
    public int MaxRound;                // 最大圈數，public是因為別人要用，但數值會以unity內為主
    private int Round = 0;              // 目前行駛圈數
    private int SpawnFuelNum;
    private Vector2 SpawnFuelRange = new Vector2(3, 10);    //生產汽油的範圍

    private void Start()
    {
        SpawnFuelNum = Round + Random.Range((int)SpawnFuelRange.x, (int)SpawnFuelRange.y);
        int level = Mathf.Clamp(PlayerDataManager.self.data.Level, 0, 2);
        MaxRound = LevelCtl.self.Round[level];
    }

    void RoadStart() // 每一圈的開頭 (從Animation呼叫來的) 
    {
        // 在終點的前一圈顯示終點線
        /*if (Round >= MaxRound - 1)
            EndLine.SetActive( true );
        else
            EndLine.SetActive( false );*/
        //以上4行精簡版
        EndLine.SetActive( Round >= MaxRound - 1 );
    }

    // Update is called once per frame
    void RoadEnd()   // 每一圈的尾端 (從Animation呼叫來的)
    {
        Round++;                       // 增加圈數
        if(Round > SpawnFuelNum)
        {
            SpawnFuelObj.self.Spawn();      //生產汽油
            //重新決定哪一圈要生產汽油
            SpawnFuelNum = Round + Random.Range((int)SpawnFuelRange.x, (int)SpawnFuelRange.y);
        }
        MapCtl.self.MapMove();         // 讓小地圖移動
        if (EndLine.activeSelf)        // 如果已經顯示終點線 或是 if( Ruond >= MaxRound ) 如果目前行駛圈數已達最大圈數
        {
            EffectPlayer.self.PlayEffect("win");
            CarCtl.self.Stop();                     // 過關
        }
    }
}
