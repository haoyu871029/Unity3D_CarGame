using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtl : MonoBehaviour
{
    private Vector2 BallMoveRange = new Vector2(-3.7f, 1.3f);
    private float posx = 0.05f;
    private float DestroyDistance = 13f;   //超過此距離(DestoryDistance)就要刪除這個物件
    private void FixedUpdate()
    {
        float TrueSpeed = -CarCtl.self.BGSpeed / 4f;               // 計算出敵人真正該移動的速度 (0.8 ~ -0.2)

        if (transform.position.x > BallMoveRange.y)
            posx = -0.05f;
        if (transform.position.x < BallMoveRange.x)
            posx = 0.05f;
        transform.position += new Vector3(posx, TrueSpeed, 0);

        if (Vector3.Distance(transform.position, SpawnBallCtl.self.transform.position) > DestroyDistance)
            Destroy(gameObject);
    }
    
}
