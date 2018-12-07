using UnityEngine;
/// <summary>
/// 玩家控制类,挂Player上
/// </summary>
public class PlayerContorller : MonoBehaviour
{
    PlayerCharacter pc;
    void Start ()  
    {
        pc = GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        //按鼠标左键创建角色
        if (Input.GetMouseButtonDown(0)) 
        {
            pc.ClickMapCreateCharacter();
        }
    }
}
