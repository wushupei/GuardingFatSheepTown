using UnityEngine;
/// <summary>
/// 玩家角色类,挂Player上
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    CharacterManage characterManager; //获取角色管理类脚本
    void Start()
    {
        characterManager = FindObjectOfType<CharacterManage>();
    }
    public void ClickMapCreateSoldier(Vector3 pos, string cardName) //点击地图生成士兵
    {
        //发射射线在地图上生成士兵
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit, 300) && hit.collider.CompareTag("Ground")) //检测为地面                                                                                    
        {
            //生成在鼠标点击位置,将自身标签给
            GameObject soldier = characterManager.GetSoldierByName(cardName);
            if (soldier)
            {
                soldier.tag = tag;
                soldier = Instantiate(soldier, hit.point, Quaternion.identity);
            }
        }
    }
}
