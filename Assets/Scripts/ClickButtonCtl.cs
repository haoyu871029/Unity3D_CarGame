using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickButtonCtl : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.text = "Click : " + PlayerDataManager.self.data.ClickNum.ToString();
    }
    public void Click()
    {
        PlayerDataManager.self.data.ClickNum++;
        text.text = "Click : " + PlayerDataManager.self.data.ClickNum.ToString();
        PlayerDataManager.self.Save();
    }
}
