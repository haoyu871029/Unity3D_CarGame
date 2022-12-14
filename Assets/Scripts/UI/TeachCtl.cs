using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 按鍵觸控回饋
public class TeachCtl : MonoBehaviour   // GameUI/Teach
{
    public static TeachCtl self;
    public void Awake()
    {
        self = this;
    }
    public Image[] Arrows;              // 上、左、右鍵

    private Color32 White = new Color32(255, 255, 255, 255);
    private Color32 Gray = new Color32(150, 150, 150, 255);

    public void TouchArrow(int ID,bool touch)    // 該按鍵是否有被按下
    {
        Arrows[ID].color = touch ? Gray : White;    // 若該按鍵被按下則顯示灰色 否則 顯示白色
    }
}
