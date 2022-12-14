using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵人生產器
public class SpawnEnemyCtl : MonoBehaviour  // SpawnEnemy
{
    public static SpawnEnemyCtl self;                           // 建構式
    private void Awake()
    {
        self = this;
    }

    private float AddEnemyCD;                            // 冷卻時間
    private float MaxAddEnemyCD = 0f;                           // 實際生產敵人的時間
    private float MaxDistance = 6.5f;                           // 敵人的最大距離
    private int TypeNum;

    private string EnemyPrefabPath = "Prefabs/Enemy";           // 敵人的Prefab路徑

    private Vector2 XRange= new Vector2(-3.7f, 1.37f);          // 敵人生產時的X軸範圍

    private void Start()
    {
        int level = Mathf.Clamp(PlayerDataManager.self.data.Level,0,2);
        TypeNum = LevelCtl.self.EnemyType[level];
        AddEnemyCD = LevelCtl.self.AddEnemyCD[level];
    }

    private void Update()
    {
        if( CanAdd() && !PlayerDataManager.self.data.stop )     // 如果現在生產敵人的位置是安全距離 且 遊戲還沒停止
        {
            Spawn();                                            // 專門生產敵人的函式
        }
    }

    void Spawn()    // 專門生成敵人的函式
    {
        if (Time.time > MaxAddEnemyCD)                          // 若現在遊戲運行的時間 超過 實際生產敵人的時間
        {
            // Random.Range的範圍: mix <= x < max
            float PosX = Random.Range( XRange.x , XRange.y );   // 從最X軸範圍隨機取得一個浮點數
            // 生產敵人
            int ID = Random.Range( 0 , TypeNum );
            GameObject enemy = Instantiate( Resources.Load( EnemyPrefabPath + ID.ToString() ), transform ) as GameObject;
            // 更改敵人的位置
            enemy.transform.localPosition += new Vector3( PosX , 0 , 0 );
            MaxAddEnemyCD = Time.time + AddEnemyCD;             // 增加冷卻
        }
        
    }

    bool CanAdd()   // 如果現在生產敵人汽車的位置是安全的
    {
        int num = transform.childCount;                         // transform.childCount: 取得子物件的數量
        bool Safe = true;                                       // 事先假設 距離是ok的
        // 判斷每一台敵人車和現在的位置為安全距離
        for ( int i = 0 ; i < num ; i++ )
        {
            // 取得第i個子物件的位置
            Vector3 TemPos = transform.GetChild( i ).transform.position;
            // 取得自己的位置 (X軸歸0)
            Vector3 TempPos2 = new Vector3(0, TemPos.y, TemPos.z);
            // 如果有任何一台車是不安全的
            if( Vector3.Distance ( transform.position , TemPos ) < MaxDistance )
            {
                Safe = false;                                   // 設定為不安全
                break;                                          // 跳出此迴圈 (後續都不用再檢查)
            }
        }

        return Safe;                                            // 回傳安全與否
    }
}
