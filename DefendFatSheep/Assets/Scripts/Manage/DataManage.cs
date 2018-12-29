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
    public JsonData GetDataByName(string name) //根据名字获取数据
    {
        for (int i = 0; i < allDate.Count; i++)
        {
            if (name.Contains(allDate[i]["PrefabName"].ToString())) //名字对的上,返回该数据
                return allDate[i];
        }
        return null;
    }
}
