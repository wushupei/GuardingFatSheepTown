using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 玩家角色类,挂Player上
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    HolyWaterAndHolySpiritManage waterAndSpirit; //圣水和精魂
    public Transform waterSlider; //圣水进度条
    public Text waterNumber; //圣水数量Text
    CharacterManage characterManager; //获取角色管理类脚本
    void Start()
    {
        waterAndSpirit = new HolyWaterAndHolySpiritManage(waterSlider, waterNumber);
        characterManager = FindObjectOfType<CharacterManage>();
    }
    public void ClickMapCreateSoldier(Vector3 pos, string cardName, float waterConsume) //点击地图生成士兵
    {
        //发射射线在地图上生成士兵
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit, 300) && hit.collider.CompareTag("Ground")) //检测为地面                                                                                    
        {
            //生成在鼠标点击位置,将自身标签给生成的士兵
            GameObject soldier = characterManager.GetSoldierByName(cardName);
            //如果圣水数量大于消耗数量
            if (soldier && waterAndSpirit.water > waterConsume)
            {
                waterAndSpirit.SubHolyWater(waterConsume); //扣除圣水
                soldier.tag = tag;
                Instantiate(soldier, hit.point, Quaternion.identity).AddComponent<SoldierCharacter>();
            }
        }
    }
    void Update()
    {
        waterAndSpirit.AddHolyWater(); //圣水自增
    }
}
