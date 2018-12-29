using System.Collections;

using System.Collections.
Generic;
using UnityEngine;


public class AIContorller : MonoBehaviour
{
    PlayerCharacter pc;
    string cardName;
    public float waterConsume;
    void Start()
    {
        pc = GetComponent<PlayerCharacter>();
    }
    void Update()
    {

        //按鼠标左键创建角色
        if (Input.GetMouseButtonDown(1))
        {
            int index = Random.Range(0, 4);
            switch (index)
            {
                case 0: cardName = "HanBing"; break;
                case 1: cardName = "JianSheng"; break;
                case 2: cardName = "NvJing"; break;
                case 3: cardName = "ShuGuang"; break;
            }
            pc.ClickMapCreateSoldier(Input.mousePosition, cardName, waterConsume);
        }
    }
}
