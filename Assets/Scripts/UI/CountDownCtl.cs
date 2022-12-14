using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownCtl : MonoBehaviour
{
    private int Num = 3;
    public Transform TitleEnemys;

    private void Start()
    {
        GetComponent<Text>().text = Num.ToString();
        EffectPlayer.self.PlayEffect("countdown");
    }
    void CountDown() 
    {
        Num--;
        if(Num > 0)
        {
            GetComponent<Text>().text = Num.ToString();
        }
        else
        {
            GetComponent<Text>().text = " GO !";
            Destroy(TitleEnemys.gameObject, 1f);
            for(int i = 0; i < TitleEnemys.childCount; i++)
            {
                TitleEnemys.GetChild(i).GetComponent<TitleEnemyCtl>().enabled = true;
            }
            Invoke("StartGame", 1f);
        }
         
    }
    void StartGame()
    {
        TitleCtl.self.StartGame(true);                   // 啟動遊戲
        gameObject.SetActive(false);                     // 隱藏倒數文字
    }
}
