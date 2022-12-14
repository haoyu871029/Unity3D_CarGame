using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制標題的敵人移動
public class TitleEnemyCtl : MonoBehaviour      // TitleEnemy/Enemy0
{
    private float Speed = 0.8f;                                 // 敵人最大車速
    private void FixedUpdate()
    {
        transform.position += new Vector3(0, Speed, 0);     // 實際讓敵人移動的距離
    }
}
