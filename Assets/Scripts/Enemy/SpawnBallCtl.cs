using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallCtl : MonoBehaviour
{
    public static SpawnBallCtl self;
    private void Awake()
    {
        self = this;
    }

    private float AddBallCD = 5.0f;
    private float MaxAddBallCD = 0f;
    private float MaxDistance = 6.5f;
    private const string BallPrefabPath = "Prefabs/Ball";
    private void Update()
    {
        if(CanAddBall())
        {
            if (Time.time >= MaxAddBallCD)
            {
                Instantiate(Resources.Load(BallPrefabPath), transform);
                MaxAddBallCD = Time.time + AddBallCD;
            }
        }
    }
    bool CanAddBall()
    {
        int num = transform.transform.childCount;
        bool Safe = true;
        for(int i=0;i<num;i++)
        {
            Vector3 Tempos = transform.GetChild(i).transform.position;
            Vector3 MyPos = new Vector3(0, transform.position.y, transform.position.z);
            Vector3 TempPos2 = new Vector3(0, Tempos.y, Tempos.z);
            if(Vector3.Distance(MyPos,TempPos2)<MaxDistance)
            {
                Safe = false;
                break;
            }
        }
        return Safe;
    }
}
