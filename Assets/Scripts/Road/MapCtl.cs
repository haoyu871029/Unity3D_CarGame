using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制小地圖的移動
public class MapCtl : MonoBehaviour     // GameScene/Map
{
    public static MapCtl self;          // 建構式
    private void Awake()
    {
        self = this;
    }

    private float EndPos = 4.5f;        // 小地圖的終點位置
    private float MaxDistance = 0;      // 小地圖所移動的總距離

    private void Start()
    {
        // 取得小地圖所移動的總距離  
        MaxDistance = EndPos - transform.localPosition.y;
    }

    public void MapMove()// 控制小地圖移動的函式
    {
        // 每一次小地圖都會移動一段距離 (透過背景的圈數來決定)
        transform.localPosition += new Vector3 ( 0 , MaxDistance / RoundCtl.self.MaxRound , 0 );
    }
}
