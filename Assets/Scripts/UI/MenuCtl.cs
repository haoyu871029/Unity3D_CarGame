using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//選單的控制器
public class MenuCtl : MonoBehaviour   //GameUI/Menu
{
    public static MenuCtl self;        //建構式
    private void Awake()
    {
        self = this;
    }

    public GameObject Menu_BG;         //物件 會把選單的背景拉入此物件
    public int TimeNum;                //經過的時間(數字)
    public int HitNum;                 //撞車的次數(數字)
    public Text TimeText;              //經過的時間(文字)
    public Text HitText;               //撞車的次數(文字)
    public Text BtnText;               //下一關/再來一次的文字

    private void Start()
    {
        Menu_BG.SetActive(false);      //把Menu_BG這個物件關掉
    }

    public void ShowResult()           //顯示結果
    {
        Menu_BG.SetActive(true);       //把Menu_BG這個物件打開
        TimeResult();                  //時間的結果次數
        HitResult();                   //撞車的結果次數
        ChangeBtnText();               //更改下一關/再來一次的文字
    }

    void TimeResult()
    {
        int Min = TimeNum / 60;        //換算成分
        int Secd = TimeNum % 60;       //換算成秒
        //個位數補零
        string MinStr = Min > 9 ? Min.ToString() : "0" + Min.ToString();        
        string SecStr = Secd > 9 ? Secd.ToString() : "0" + Secd.ToString();
        TimeText.text = "花費時間 " + MinStr + " : " + SecStr;
    }

    void HitResult()
    {
        HitText.text = "撞車次數 " + HitNum.ToString() + " 次";
    }

    public void AgainBtn()              // 再來一次(Button)
    {
        if (!PlayerDataManager.self.data.Lose)      //如果沒有輸
            PlayerDataManager.self.data.Level++;    //下一關
        LoadingCtl.self.Loading();                  //重新讀取場景(無論輸贏)
        PlayerDataManager.self.Default();           //恢復預設    
    }
    public void ExitBtn()               // 離開遊戲(Button)
    {
        Application.Quit();             // 離開遊戲的指令(限定執行檔才可以使用)
    }

    public void ChangeBtnText()
    {
        BtnText.text = PlayerDataManager.self.data.Lose ? "再來一次" : "下一關";
    }
}
