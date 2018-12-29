using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色管理类,挂CharacterManage上
/// </summary>
public class CharacterManage : MonoBehaviour
{
    //存放双方主城
    public Transform[] mainCitys;
    int i; //索引
    //使用字典储存所有士兵
    Dictionary<string, GameObject> Soldiers = new Dictionary<string, GameObject>();
    //判断游戏是否结束
    public bool gameOver;
    void Awake()
    {
        SaveSoldierPrefabs();
    }
    void SaveSoldierPrefabs() //用字典储存所有士兵预制体,使用名字做键
    {
        GameObject[] soldierPrefabs = Resources.LoadAll<GameObject>("Prafabs/Soldiers");
        foreach (var item in soldierPrefabs)
        {
            Soldiers.Add(item.name, item);
        }
    }
    public GameObject GetSoldierByName(string cardName) //根据卡牌名找到士兵
    {
        if (!gameOver) //游戏没有结束时才能创建
            return Soldiers[cardName];
        return null;
    }
    void Update()
    {
        if (!gameOver) //如果游戏未结束,判断胜负
            JudgementOfVictoryOrDefeat();
    }
    public void JudgementOfVictoryOrDefeat() //胜负判断
    {
        //其中一方主城挂了, 游戏结束, 另一方获胜
        if (!mainCitys[i]) //发现
        {
            gameOver = true;
            print(mainCitys[Mathf.Abs(i - 1)].tag + "赢了");
        }
        i = Mathf.Abs(i - 1);
    }
}
