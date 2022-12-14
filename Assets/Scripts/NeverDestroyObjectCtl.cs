using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生產[不要刪除的物件]的控制器
public class NeverDestroyObjectCtl : MonoBehaviour  //NeverDestroyObj
{
    public GameObject[] NeverDestroyObjs;           //不要被刪除的物件們

    public static bool IsSpawn;                     //是否已經生產過物件

    private void Awake()
    {
        if(!IsSpawn)                                //如果還沒生產過物件
        {
            for (int i = 0; i < NeverDestroyObjs.Length; i++)
            {
                //生產物件
                GameObject obj = Instantiate(NeverDestroyObjs[i]);
                DontDestroyOnLoad(obj);             //不要因為換場景就被刪除
            }
        }
        IsSpawn = true;                             //表示已生產
    }
}
