using UnityEngine;
using LitJson;
using System.IO;
/// <summary>
/// 所有Json数据的管理类,挂DataManage上
/// </summary>
public class DataManage : MonoBehaviour
{
    public JsonData allDate;
    void Awake()
    {
        allDate = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/AllCharacterData.json"));
        if (allDate == null)
            print("空");
    }
    public JsonData GetCharacterData(int id) //获取角色数据
    {
        for (int i = 0; i < allDate.Count; i++)
        {
            if ((int)allDate[i]["ID"] == id) //ID对上就将数据给它
                return allDate[i];
        }
        return null;
    }
    public JsonData GetCardData(string name) //获取卡牌数据
    {
        for (int i = 0; i < allDate.Count; i++)
        {
            if (name.Contains(allDate[i]["CardName"].ToString())) //名字对的上,返回该数据
                return allDate[i];
        }
        return null;
    }
}
