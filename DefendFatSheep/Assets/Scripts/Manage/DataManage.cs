using UnityEngine;
using LitJson;
using System.IO;

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
}
