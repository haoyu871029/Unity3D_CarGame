using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制玩家移動
public class CarCtl : MonoBehaviour
{
    public static CarCtl self;          //建構式
    private void Awake()
    {
        self = this;
    }

    public Animator BG;                 //背景的動畫控制器
    public float BGSpeed = 0;           //背景播放動畫的速度
    public float CarPosX = 0f;          //汽車的x軸位置
    public GameObject Explosion;        //爆炸特效的物件

    //汽車的左右移動範圍
    private Vector2 XMoveRange = new Vector2(-3.7f, 1.37f);
    private bool DriveBool = false;     //汽車是否需要加速
    private bool IsHit = false;         //汽車是否正在打滑
    private bool IsHitBall = false;
    private float CurrentTime;
    private float AddSpeed = 0.01f;     //汽車加速度
    private float XMoveSpeed = 0.2f;    //汽車左右移動的速度
    private float DefaultPosX;          //道路中間值
    private AudioSource aud;            //汽車音效
    private Vector3 StartScale;

    private void Start()
    {
        DefaultPosX = transform.position.x;
        CarPosX = transform.localPosition.x;           //一開始先取得汽車的X軸位置
        aud = EffectPlayer.self.GetEffect("drive");    //生產並取得音效
        aud.loop = true;
        aud.pitch = 0;

        StartScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
        Move();                             //控制向前移動
        if (!IsHit)
            Turn();                         //控制左右移動
        else //IsHit 汽車正在打滑
            Rotate();

        aud.pitch = BG.speed = BGSpeed;     //控制背景撥放速度

        //控制汽車左右移動
        transform.localPosition = new Vector3 ( CarPosX , transform.localPosition.y , transform.localPosition.z );

        if( PlayerDataManager.self.data.Lose && BGSpeed == 0)      // 沒有油 且 緩衝也結束
        {
            EffectPlayer.self.PlayEffect("fail");
            Stop();
        }
        if(Time.time >= (CurrentTime+5f) )
            transform.localScale = new Vector3(StartScale.x, StartScale.y, StartScale.z);
    }

    void Move()     //by FixedUpdate()
    {
        /*if(Input.GetKey(KeyCode.UpArrow))
            DriveBool = true;   //汽車加速
        else
            DriveBool = false;  //汽車不要加速*/
        //以上的精簡版
        DriveBool = Input.GetKey(KeyCode.UpArrow) && !PlayerDataManager.self.data.Lose;     //是否按住鍵盤上鍵 且 遊戲還沒輸
        TeachCtl.self.TouchArrow(0, DriveBool);                                             //是否有按上鍵
        
        AddSpeed = DriveBool ? Mathf.Abs(AddSpeed) : -Mathf.Abs(AddSpeed);                  //根據是否有按住上鍵來決定加減速
        BGSpeed += AddSpeed;

        /*if( BGSpeed >= 1 )
            BGSpeed = 1;
        if( BGSpeed <= 0 )
            BGSpeed = 0;*/
        //以上的精簡版
        BGSpeed = Mathf.Clamp(BGSpeed, 0, 1);       //限制背景撥放的速度 (若大於1則等於1，若小於0則等於0)
    }

    void Turn()     //by FixedUpdate()
    {
        bool LeftBool = false;                      //判斷是否有按下左鍵
        bool RightBool = false;                     //判斷是否有按下右鍵

        if (Input.GetKey(KeyCode.RightArrow))       //若按住鍵盤右鍵
        {
            CarPosX += XMoveSpeed;                  //汽車向右移動
            RightBool = true;                       //按下右鍵
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            CarPosX -= XMoveSpeed;                  //汽車向左移動
            LeftBool = true;                        //按下左鍵
        }

        TeachCtl.self.TouchArrow(1, LeftBool);      //是否有按左鍵
        TeachCtl.self.TouchArrow(2, RightBool);     //是否有按右鍵

        //限制汽車的左右移動範圍
        CarPosX = Mathf.Clamp(CarPosX, XMoveRange.x, XMoveRange.y);
    }

    void Rotate()   //by FixedUpdate()
    {
        //超過汽車的左右移動範圍
        if(CarPosX >= XMoveRange.y || CarPosX <= XMoveRange.x)
        {
            BoomStart();
            RotateReset();
        }
        if(BGSpeed == 0)
        {
            RotateReset();
        }
    }

    void RotateReset()
    {
        GetComponent<CarRotateCtl>().enabled = false;       //關閉汽車旋轉腳本
        transform.eulerAngles = Vector3.zero;               //重製旋轉軸
        IsHit = false;
    }

    void BoomStart()                    //發生爆炸的開端
    {
        Debug.Log(" 已發生爆炸 ");

        EffectPlayer.self.PlayEffect("explosion");
        aud.pitch = BGSpeed = BG.speed = 0;                 //背景撥放動畫的速度歸零(車子的速度歸零)、實際背景的播放速度歸零
        this.enabled = false ;                              //關閉這個腳本 (CarCTL)
        ShowExplosion(true);                                //顯示爆炸
    }

    public void BoomEnd()               //發生爆炸的尾端
    {
        Debug.Log(" 爆炸結束 ") ;

        this.enabled = true ;                               //打開這個腳本
        ShowExplosion(false);                               //隱藏爆炸
    }

    void ShowExplosion ( bool show)     //顯示爆炸特效
    {
        Explosion.SetActive(show);                          //顯示爆炸特效的物件
        GetComponent<SpriteRenderer>().enabled = !show;     //讓圖片消失  
        GetComponent<BoxCollider2D>().enabled = !show;      //讓碰撞框消失
    }

    public void Stop()                  //遊戲停止
    {  
        aud.pitch = BG.speed = BGSpeed = 0;                 //實際背景播放的速度 和 背景播放動畫的速度 歸零
        this.enabled = false;                               //關閉這個腳本
        PlayerDataManager.self.data.stop = true;            //遊戲必須停止
        MenuCtl.self.ShowResult();                          //顯示選單
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")                        //如果撞到敵人
        {
            Debug.Log(" 已發生碰撞 ");
            for (int i=0;i<3;i++)
            {
                TeachCtl.self.TouchArrow(i,false);          //是否有按上鍵
            }
            GetComponent<CarRotateCtl>().Hit(transform.position.x - DefaultPosX >= 0 ? 1 : -1);
            Debug.Log(" Hi ");
            IsHit = true;
            MenuCtl.self.HitNum++;                          //發生一次撞車
        }
        else if (collider.tag == "Fuel")                    //如果撞到汽油補充器
        {
            Debug.Log(" 已吃到補油器 ");
            FuelCtl.self.GetFuel();
            Destroy(collider.gameObject);
        }
        else if (collider.tag == "Ball")                    //如果撞到汽油補充器
        {
            CurrentTime = Time.time;
            Debug.Log(" 已撞到球 ");
            transform.localScale += new Vector3(1,1,0);
            IsHitBall = true;
            Destroy(collider.gameObject);
        }
    }
}
