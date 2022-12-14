using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 顯示玩家汽車的時速
public class KMCtl : MonoBehaviour      // GameUI/KMText
{
    private int MaxSpeed = 200;         // 汽車最大公里數

    void Update()
    {
        int km = (int)( CarCtl.self.BGSpeed * MaxSpeed );       // 計算出最大公里數的比例
        GetComponent<Text>().text = km.ToString() + " KM/H";    // 能偵測腳本掛載在哪一個物件上，將公里數的文字設定成最大公里數的比例，並加上KM/H
    }
}
