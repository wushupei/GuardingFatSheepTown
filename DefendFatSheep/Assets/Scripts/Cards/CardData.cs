using UnityEngine;
using LitJson;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 卡牌数据类,不用挂,后期动态添加到卡牌上
/// </summary>
public class CardData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    DataManage dataManage;
    JsonData selfData; //自身数据
    Transform dataPanel; //信息面板
    PlayerContorller pc; //主角控制脚本
    public void OnEnable()
    {
        //初始化时根据自身名字从数据管理类中领取自己的数据
        dataManage = FindObjectOfType<DataManage>();
        selfData = dataManage.GetCardData(name);
        //先找到信息面板的父物体,再获取隐藏的信息栏
        dataPanel = GameObject.Find("DataPanel").transform.Find("CardData");
        pc = FindObjectOfType<PlayerContorller>();
    }

    public void OnPointerEnter(PointerEventData eventData) //鼠标进入调用一次
    {
        //显示信息栏并显示相关信息
        dataPanel.gameObject.SetActive(true);
        ShowSelfData();
        pc.isCreate = false; //鼠标在UI上时不能生成士兵
    }

    public void OnPointerExit(PointerEventData eventData) //鼠标离开调用一次
    {
        //隐藏信息栏
        dataPanel.gameObject.SetActive(false);
        pc.isCreate = true; //鼠标离开UI可以生成士兵
    }

    public void OnPointerDown(PointerEventData eventData) //鼠标点击时执行一次
    {
        //根据卡牌信息主角获取要生成的士兵信息
        pc.cardName = selfData["CardName"].ToString();
    }
    void ShowSelfData() //在面板显示卡牌信息
    {
        //遍历信息面板
        for (int i = 0; i < dataPanel.childCount; i++) 
        {
            //根据当前信息项的名字作为键去Json中获取值
            Transform a = dataPanel.GetChild(i); 
            string data = selfData[a.name].ToString();
            //拿到Text组件显示的内容
            Text t = a.GetComponent<Text>();
            string s = t.text.ToString();
            //删除冒号以后的内容,用具体的值来替代
            int index = s.IndexOf(":"); 
            t.text = s.Remove(index + 1) + selfData[a.name];
        }
    }
}
