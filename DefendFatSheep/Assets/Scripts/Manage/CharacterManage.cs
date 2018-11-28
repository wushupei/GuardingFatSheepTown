using System.Collections.Generic;
using UnityEngine;
public class CharacterManage : MonoBehaviour
{
    //存放双方主城
    public Transform friendMainCity;
    public Transform enemyMainCity;
    //友军预制体和敌人预制体
    public List<GameObject> friendPrababs;
    public List<GameObject> enemyPrababs;

    void Awake()
    {
        SoldierCharacter[] friendPrababsArray= Resources.LoadAll<SoldierCharacter>("Prafabs");    
        foreach (var item in friendPrababsArray)
        {
            friendPrababs.Add(item.gameObject);
        }
    }
    public GameObject CreateCharacter(Vector3 pos,string tag) //生成角色
    {
        friendPrababs[0].tag = tag;
        GameObject character= Instantiate(friendPrababs[0],pos,Quaternion.identity);
        return character;
    }
    public void JudgementOfVictoryOrDefeat() //胜负判断(谁的主城没了,谁就GG)
    {

    }
}
