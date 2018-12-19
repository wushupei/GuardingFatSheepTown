using LitJson;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 卡牌管理类,管理所有卡牌和卡牌槽,挂CardsManage上
/// </summary>
public class CardsManage : MonoBehaviour
{
    GameObject[] cards; //保存所有卡牌   
    GridLayoutGroup cardSlot; //卡牌槽
    public int slotLength; //卡牌槽最大长度
    void Awake()
    {
        //加载所有卡牌
        cards = Resources.LoadAll<GameObject>("Prafabs/Cards");
        //找到卡牌槽的排列组件,设置水平最大长度
        cardSlot = FindObjectOfType<GridLayoutGroup>();
        cardSlot.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        cardSlot.constraintCount = slotLength;       
    }
    void Start()
    {
        CreateCards();
    }
    JsonData data;
    void CreateCards() //生成初始卡牌
    {       
        while (cardSlot.transform.childCount < slotLength) //如果卡牌槽未满
        {
            //随机一张卡牌
            int index = Random.Range(0, cards.Length); 
            //在卡牌槽的子物体下生产卡牌,添加卡牌信息脚本
            Instantiate(cards[index], cardSlot.transform).AddComponent<CardData>(); 
        }
    }
}
