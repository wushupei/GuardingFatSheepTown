using UnityEngine;
using LitJson;
using UnityEngine.EventSystems;
/// <summary>
/// 卡牌数据类,不用挂,后期动态添加到卡牌上
/// </summary>
public class CardData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    DataManage dataManage;
    JsonData selfData; //自身数据
    Transform dataPanel; //信息面板
    public void OnEnable()
    {
        //初始化时根据自身名字从数据管理类中领取自己的数据
        dataManage = FindObjectOfType<DataManage>();
        selfData = dataManage.GetCardData(name);
        //先找到信息面板的父物体,再获取隐藏的信息栏
        dataPanel = GameObject.Find("DataPanel").transform.Find("CardData");
    }

    public void OnPointerEnter(PointerEventData eventData) //鼠标进入调用一次
    {
        //显示信息栏并显示相关信息
        dataPanel.gameObject.SetActive(true);
        12
        foreach (var item in selfData)
        {
            string s = item.ToString(); //[键,值](获取该组数据)
            int index = s.IndexOf(",");
            print(index);
        }
      
    }

    public void OnPointerExit(PointerEventData eventData) //鼠标离开调用一次
    {
        //隐藏信息栏
        dataPanel.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData) //鼠标点击时执行一次
    {

    }

    public void ShowData()
    {
        for (int i = 0; i < dataPanel.childCount; i++)
        {
            foreach (var item in selfData)
            {
                string s = item.ToString(); //[键,值](获取该组数据)
                int index = s.IndexOf(",");

            }
        }
    }
}

