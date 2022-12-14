using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarRotateCtl : MonoBehaviour
{
    private void Awake()                //建構式
    {
        this.enabled = false;
    }

    private float CutSpeed = 0.015f;    //打滑時所減少的速度
    private float RotateSpeed = 20f;
    private float XMoveSpeed = 0.05f;   //打滑時的左右移動速度
    private int Direction = 1;

    private void FixedUpdate()
    {
        CarCtl.self.BGSpeed = Mathf.Clamp(CarCtl.self.BGSpeed - CutSpeed,0,1);
        transform.Rotate(0, 0, RotateSpeed * Direction);
        CarCtl.self.CarPosX -= XMoveSpeed * Direction;
    }

    public void Hit(int dir)
    {
        this.enabled = true;
        Direction = dir;
    }
}
