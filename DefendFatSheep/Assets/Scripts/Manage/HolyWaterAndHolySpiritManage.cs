using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 圣水和精魄管理类
/// </summary>
public class HolyWaterAndHolySpiritManage
{
    //圣水
    public float water = 10;
    float minWater = 0, maxWater = 10; //最小数量和最大数量
    float waterAddSpeed = 1; //自增速度
    Transform waterSlider; //进度条
    Text waterNumber; //显示数量
    //精魄
    public float holySpirit = 10;
    public HolyWaterAndHolySpiritManage(Transform slider, Text number) //构造函数
    {
        waterSlider = slider;
        waterNumber = number;
    }
    public void AddHolyWater() //圣水自增
    {
        water = Mathf.Clamp(water, minWater, maxWater); //确定最大值和最小值
        if (water < maxWater) //小于最大值则自增
            water += Time.deltaTime * waterAddSpeed;
        if (waterSlider) //进度条则同步显示
            waterSlider.localScale = new Vector3(water / 10, 1, 1);
        if (waterNumber) //数量同步显示
            waterNumber.text = ((int)water).ToString();
    }
    public void SubHolyWater(float number) //扣除圣水
    {
        water -= number;
    }
}
