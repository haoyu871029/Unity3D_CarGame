using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生產汽油(獎勵機制)
public class SpawnFuelObj : MonoBehaviour                      //SpawnFuelObj
{
    public static SpawnFuelObj self;                           // 建構式
    private void Awake()
    {
        self = this;
    }

    private bool UseBool = false;

    private string FuelPrefabPath = "Prefabs/FuelObj";           // 汽油的Prefab路徑

    private Vector2 XRange = new Vector2(-3.7f, 1.37f);          // 汽油生產時的X軸範圍

    private void Start()
    {
        int level = Mathf.Clamp(PlayerDataManager.self.data.Level, 0, 2);
        UseBool = LevelCtl.self.UseFuelObj[level];
    }

    public void Spawn()    // 專門生成汽油的函式
    {
        if(UseBool)
        {
            // Random.Range的範圍: mix <= x < max
            float PosX = Random.Range(XRange.x, XRange.y);   // 從最X軸範圍隨機取得一個浮點數
            GameObject Fuel = Instantiate(Resources.Load(FuelPrefabPath), transform) as GameObject;
            // 更改汽油的位置
            Fuel.transform.localPosition += new Vector3(PosX, 0, 0);
        }
        
    }
}
