using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

// 玩家資料管理器
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager self;   //建構式
    private void Awake()                
    {
        self = this;
        data = Load();
        if(data == null)
            data = new PlayerData();            //新增一個PlayerData的資料
        //data.Level = 2;
    }

    public PlayerData data;                 // 宣告一個資料型態為PlayerData的資料 且命名為data

    public void Save()
    {
        Debug.Log("SAVE!");
        string savePath = Path.Combine(Application.persistentDataPath, "PlayerData.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        StreamWriter writer = new StreamWriter(savePath);
        serializer.Serialize(writer.BaseStream, data);
        writer.Close();
    }

    public static PlayerData Load()
    {
        Debug.Log("LOAD!");
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        string savePath = Path.Combine(Application.persistentDataPath, "PlayerData.xml");
        if (File.Exists(savePath))
        {
            StreamReader reader = new StreamReader(savePath);
            PlayerData deserialized = (PlayerData)serializer.Deserialize(reader.BaseStream);
            reader.Close();
            return deserialized;
        }
        Debug.Log("沒有存檔紀錄!");
        return null;

    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FuelCtl.self.GetFuel();
        }
    }*/

    public void Default()
    {
        data.stop = false;
        data.Lose = false;
        for(int i=0;i<EffectPlayer.self.audios.Count;i++)
        {
            EffectPlayer.self.audios[i].Pause(); //雖然是暫停 其實是刪除並重整陣列
        }
    }
}
