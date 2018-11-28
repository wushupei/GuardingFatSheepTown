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
    public void ClickMapCreateCharacter()
    {
        //发射射线在地图上生成角色
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 300) && hit.collider.CompareTag("Ground")) //检测为地面
            //生成角色后将角色的,生成在鼠标点击位置,将自身标签给它
            characterManager.CreateCharacter(hit.point, tag);
    }
}
