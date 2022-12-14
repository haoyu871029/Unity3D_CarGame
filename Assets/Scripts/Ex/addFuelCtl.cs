using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addFuelCtl : MonoBehaviour
{
    private float DestroyDistance = 13f;   // 超過此距離(DestoryDistance)就要刪除這個物件

    void FixedUpdate()
    {
        float TrueSpeed = -CarCtl.self.BGSpeed/2.35f;               // 計算出敵人真正該移動的速度 (0.8 ~ -0.2)
        transform.position += new Vector3(0, TrueSpeed, 0);      // 實際讓敵人移動的距離
        // 如果這個物件的位置 跟 敵人生產器的位置 兩者距離 超過特定距離
        if (Vector3.Distance(transform.position, SpawnEnemyCtl.self.transform.position) > DestroyDistance)
            Destroy(gameObject);    // 刪除物件
    }
}
