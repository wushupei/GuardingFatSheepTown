using System.Collections;

using System.Collections.
Generic;
using UnityEngine;


public class AIContorller : MonoBehaviour
{
    PlayerCharacter pc;
    void Start()
    {
       // pc = GetComponent<PlayerCharacter>();
    }
    void Update()
    {
        //按鼠标左键创建角色
        if (Input.GetMouseButtonDown(1))
        {
            //pc.ClickMapCreateCharacter();
        }
    }
}
