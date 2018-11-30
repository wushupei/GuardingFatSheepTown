using UnityEngine;
using LitJson;
/// <summary>
/// 角色类基类,不用挂
/// </summary>
public abstract class Character : MonoBehaviour
{
    //从配置表读取
    public int id;
    public string CharacterName;
    public float hp; //生命值
    public float aggressivity; //攻击力
    public float spellPower; //法强
    public float physicalResistance; //物理抗性
    public float magicResistance; //魔法抗性
    public float attackSpeed; //攻速
    public float criticalChance; //暴击率
    public float criticalHitMultiple; //暴击倍数
    public float physicalPenetration; //物理穿透
    public float spellPenetration; //法术穿透    
    public float moveSpeed; //移动速度
    public float rotateSpeed; //转身速度
    public float attackRange; //攻击范围
    public float viewRange; //视野范围

    //运行中获取
    public Transform mainCity; //敌方主城
    public Transform attackTarget; //攻击目标
    public float timer = 0; //攻击间隔(受攻击速度影响)

    //引用的类  
    CharacterManage characterManage; //角色管理类
    DataManage dataManage; //数据管理类

    public virtual void OnEnable()
    {
        characterManage = FindObjectOfType<CharacterManage>();
        SetTargetTag(); //判断标签
        GetSelfData(); //获取数据
    }
    void SetTargetTag() //根据自身标签判定获取敌人主城和自身颜色
    {
        foreach (var item in characterManage.mainCitys) //遍历两座主城
        {
            if (item.tag == tag) //标签和自己一样的,设成同它一样的颜色
            {
                Color color = item.GetComponentInChildren<MeshRenderer>().material.color;
                GetComponentInChildren<MeshRenderer>().material.color = color;
            }
            else //不一样则获取它
            {
                mainCity = item;
                attackTarget = mainCity;
            }
        }
    }
    protected void GetSelfData() //获取自身数据
    {
        dataManage = FindObjectOfType<DataManage>();
        JsonData data = dataManage.allDate;
        data = dataManage.GetCharacterData(id);

        CharacterName = data["CharacterName"].ToString();
        hp = float.Parse(data["Hp"].ToString()); //生命值
        aggressivity = float.Parse(data["Aggressivity"].ToString());  //攻击力
        spellPower = float.Parse(data["SpellPower"].ToString()); //法强
        physicalResistance = float.Parse(data["PhysicalResistance"].ToString());//物理抗性
        magicResistance = float.Parse(data["MagicResistance"].ToString()); //魔法抗性
        attackSpeed = float.Parse(data["AttackSpeed"].ToString()); //攻速
        criticalChance = float.Parse(data["CriticalChance"].ToString()); //暴击率
        criticalHitMultiple = float.Parse(data["CriticalHitMultiple"].ToString()); //暴击倍数
        physicalPenetration = float.Parse(data["PhysicalPenetration"].ToString()); //物理穿透
        spellPenetration = float.Parse(data["SpellPenetration"].ToString()); //法术穿透   
        moveSpeed = float.Parse(data["MoveSpeed"].ToString());//移动速度
        rotateSpeed = float.Parse(data["RotateSpeed"].ToString());//转身速度
        attackRange = float.Parse(data["AttackRange"].ToString()); //攻击范围
        viewRange = float.Parse(data["ViewRange"].ToString()); //视野范围
    }
    public abstract void GetAttackTarget(); //获取当前攻击目标   
    public void Attack() //攻击
    {
        if (AttackDirection())
        {
            if (timer <= 0)
            {
                print("攻击");
                timer = 1 / attackSpeed;
            }
        }
    }
    bool AttackDirection() //攻击目标方位判定
    {
        //往前方发射射线,如果打到攻击目标,则可以攻击
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, attackRange);
        foreach (var item in hits)
        {
            if (item.collider == attackTarget.GetComponent<Collider>())
                return true;
        }
        return false;
    }
}
