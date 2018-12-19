using UnityEngine;
/// <summary>
/// 玩家控制类,挂Player上
/// </summary>
public class PlayerContorller : MonoBehaviour
{
    PlayerCharacter pc;
    public string cardName; //接收需要创建的卡牌预制体名(和角色预制体名相同)
    public bool isCreate; //是否可以生成(光标离开UI为true)
    void Start()
    {
        pc = GetComponent<PlayerCharacter>();
    }
    void Update()
    {
        //按鼠标左键创建角色
        if (Input.GetMouseButtonDown(0)&&isCreate)
        {
            //根据卡牌名称生成相应士兵
            pc.ClickMapCreateSoldier(Input.mousePosition, cardName);
            cardName = ""; //只能生成一次
        }
    }
}
