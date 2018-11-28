using UnityEngine;
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
