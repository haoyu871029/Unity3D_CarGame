using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//控制敵人移動
public class EnemyCtl : MonoBehaviour      //Prefabs/Enemy
{
    public int Type;                       //敵人種類

    private float Speed = 0.8f;            //敵人最大車速
    private float DestroyDistance = 13f;   //超過此距離(DestoryDistance)就要刪除這個物件
    private float XMoveDistance = 8f ;     //小於此距離就要移動X軸 
    private float Duration = 1f ;
    private bool IsMove = false ;          //敵人是否已經移動過
    private Vector2 XMoveRange = new Vector2(-3.7f, 1.3f);

    void FixedUpdate()
    {
        float TrueSpeed = Speed - CarCtl.self.BGSpeed;               //計算出敵人真正該移動的速度 (0.8 ~ -0.2)
        transform.position += new Vector3( 0 , TrueSpeed , 0 );      //實際讓敵人移動的距離
        
        //如果這個物件的位置 跟 敵人生產器的位置 兩者距離 超過特定距離
        if( Vector3.Distance( transform.position, SpawnEnemyCtl.self.transform.position ) > DestroyDistance )
            Destroy ( gameObject );                                  //刪除物件
        XMove();                                                     //控制敵人移動
    }

    void XMove ()               //敵人的行為模式
    {
        if(Type == 0)           //衝撞玩家
        {
            HitPlayer();
        }
        else if(Type == 1)      //X軸不動
        {}
        else if(Type == 2)      //X軸固定來回移動
        {
            Type2();
        }
    }
    void HitPlayer()
    {
        // 如果敵人還沒移動過 且 這個物件的距離 跟 玩家汽車的位置 兩者距離小於特定距離
        //if( !IsMove && Vector3.Distance( transform.position, CarCtl.self.transform.position ) < XMoveDistance)
        if (!IsMove && Mathf.Abs(transform.position.y - CarCtl.self.transform.position.y) < XMoveDistance)
        {
            if (Mathf.Abs(transform.position.x - CarCtl.self.transform.position.x) > 1.5f)
                EffectPlayer.self.PlayEffect("enemymove");
            transform.DOMoveX(CarCtl.self.transform.position.x, Duration);
            IsMove = true;
        }
    }

    void Type2()
    {
        if(!IsMove)
        {
            float Left = transform.position.x - 1.5f;                   //敵人位置的左邊一段距離
            Left = Mathf.Clamp(Left, XMoveRange.x, XMoveRange.y);       //限制敵人左邊的範圍
            float Right = transform.position.x + 1.5f;                  //敵人位置的右邊一段距離
            Right = Mathf.Clamp(Right, XMoveRange.x, XMoveRange.y);     //限制敵人右邊的範圍

            int Ran = Random.Range(0, 2);
            if(Ran == 0)
            {
                transform.DOMoveX(Right, 0);
                PingPong(Left, Right);
            }
            else
            {
                transform.DOMoveX(Left, 0);
                PingPong(Right, Left);
            }

            IsMove = true;
        }
    }

    void PingPong(float left,float right)     //主要來回移動的指令  
    {
        //OnComplete做完之後接著做
        transform.DOMoveX(left, Duration).SetEase(Ease.Linear).OnComplete(()=>PingPong(right,left));
    }
}
