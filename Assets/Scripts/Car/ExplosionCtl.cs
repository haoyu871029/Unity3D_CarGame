using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//觸發爆炸特效的物件
public class ExplosionCtl : MonoBehaviour  //Car/Explosion
{   
    void ExplosionEvent ()                 //爆炸事件 (Animation)
    {
        CarCtl.self.BoomEnd();             //呼叫玩家汽車爆炸的尾端
    }
}
