using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;           // 需要序列化的話就要using 

[Serializable]          // 將資料序列化
public class PlayerData 
{
    public bool stop;   //遊戲是否已停止
    public bool Lose;   //遊戲是否輸了

    public int Level;   //關卡數

    public int ClickNum;
}
