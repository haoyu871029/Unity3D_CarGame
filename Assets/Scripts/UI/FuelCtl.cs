using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//控制消耗油量
public class FuelCtl : MonoBehaviour    //GameUI/FuelText
{
    public static FuelCtl self;
    private void Awake()
    {
        self = this;
    }

    private int MaxFuel;                // 汽車最大油量
    private int Fuel = 0;               // 汽車消耗油量
    private string zero = "000";        // 油量的0最多有幾個

    private void Start()
    {
        int level = Mathf.Clamp(PlayerDataManager.self.data.Level, 0, 2); 
        MaxFuel = LevelCtl.self.Fuel[level];
        ShowFuel();                     // 顯示油量
        Invoke("Cut", 1);               // 1秒後執行Cut
    }

    void Cut()
    {
        if( !PlayerDataManager.self.data.stop )             //如果遊戲還沒停止
        {
            Fuel++;                                         //每次耗油量都加1
            MenuCtl.self.TimeNum++;                         //經過的時間就加一秒(選單用)
            ShowFuel();                                     //顯示油量
            if ((MaxFuel - Fuel) > 0)                       //若剩餘油量大於1: 就繼續耗油
                Invoke("Cut", 1);                           //1秒後執行Cut
            else                                            //否則
                PlayerDataManager.self.data.Lose = true;    //沒油了 不耗油 

            if(MaxFuel-Fuel<=5)
            {
                EffectPlayer.self.PlayEffect("warniing");
            }
        }
    }

    public void ShowFuel()
    {
        string fuelStr = (MaxFuel - Fuel).ToString();
        // 將油量文字設定成 Fuel + (補上該有的0) + 剩餘油量
        GetComponent<Text>().text = "Fuel " + zero.Substring(fuelStr.Length) + fuelStr;
    }

    public void GetFuel()       // 外掛: 取得油量
    {
        if (PlayerDataManager.self.data.Lose)
        {
            PlayerDataManager.self.data.Lose = false;
            Invoke("Cut", 1);       // 1秒後執行"Cut"，否則沒油後再補的油永遠不會被扣
        }
        Fuel -= 10;
        if (Fuel < 0)
            Fuel = 0;       // 90~100內補油都回到100 ( MaxFuel - Fuel = 100 - 0 )
        ShowFuel();
    }
}
