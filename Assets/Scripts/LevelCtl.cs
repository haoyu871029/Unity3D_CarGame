using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelCtl : MonoBehaviour
{
    [HideInInspector]
    public int[] EnemyType = new int[] { 1, 2, 3 };                 //敵人種類
    [HideInInspector]
    public int[] Fuel = new int[] { 100, 60, 30 };                  //汽油最大油量
    [HideInInspector]
    public int[] Round = new int[] { 5, 70, 100 };                 //抵達終點的最大圈數
    [HideInInspector]
    public float[] AddEnemyCD = new float[] { 2f, 1f, 0.5f };       //生產敵人的冷卻時間
    [HideInInspector]
    public bool[] UseFuelObj = new bool[] { false, false, true };   //是否要啟用補充汽油道具

    public static LevelCtl self;
    private void Awake()
    {
        self = this;
    }

    private void Start()
    {
        ShowLevel();
        int level = Mathf.Clamp(PlayerDataManager.self.data.Level, 0, 2);
        Debug.Log("現在是第" + (level+1).ToString() + "關,  有" + EnemyType[level] + "種敵人");
    }

    public void ShowLevel()
    {
        int level = PlayerDataManager.self.data.Level + 1;          //取得現在的關卡數
        GetComponent<Text>().text = "Level" + level.ToString();     //顯示關卡數的文字
    }


}
