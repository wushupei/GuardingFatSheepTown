﻿using UnityEngine;
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
    public float ad; //攻击力
    public float ap; //法强
    public float adr; //物理抗性
    public float apr; //魔法抗性
    public float attackSpeed; //攻速
    public float cc; //暴击率
    public float cm; //暴击倍数
    public float adp; //物理穿透
    public float app; //法术穿透    
    public float moveSpeed; //移动速度
    public float rotateSpeed; //转身速度
    public float attackRange; //攻击范围
    public float viewRange; //视野范围
    public float waterConsume; //圣水消耗
    public float spiritConsume; //精魄消耗

    //运行中获取
    public Transform EnemyMainCity; //敌方主城
    public string enemyTag; //敌方标签
    public AudioSource audioSource; //声音播放组件
    public Transform attackTarget; //攻击目标
    public float timer = 0; //攻击间隔(受攻击速度影响)
    public float maxHp; //最大血量
    public Transform hpSlider; //血条
    public bool isDie = false;

    //引用的类  
    public CharacterManage characterManage; //角色管理类
    DataManage dataManage; //数据管理类

    protected void GetSelfData() //获取自身数据
    {
        dataManage = FindObjectOfType<DataManage>();
        JsonData data = dataManage.allDate;
        data = dataManage.GetDataByName(name);//根据名字获取数据

        CharacterName = data["CharacterName"].ToString();
        hp = float.Parse(data["Hp"].ToString()); //生命值
        ad = float.Parse(data["Ad"].ToString());  //攻击力
        ap = float.Parse(data["Ap"].ToString()); //法强
        adr = float.Parse(data["Adr"].ToString());//物理抗性
        apr = float.Parse(data["Apr"].ToString()); //魔法抗性
        attackSpeed = float.Parse(data["AttackSpeed"].ToString()); //攻速
        cc = float.Parse(data["Cc"].ToString()); //暴击率
        cm = float.Parse(data["Cm"].ToString()); //暴击倍数
        adp = float.Parse(data["Adp"].ToString()); //物理穿透
        app = float.Parse(data["App"].ToString()); //法术穿透   
        moveSpeed = float.Parse(data["MoveSpeed"].ToString());//移动速度
        rotateSpeed = float.Parse(data["RotateSpeed"].ToString());//转身速度
        attackRange = float.Parse(data["AttackRange"].ToString()); //攻击范围
        viewRange = float.Parse(data["ViewRange"].ToString()); //视野范围
        waterConsume = float.Parse(data["WaterConsume"].ToString()); //圣水消耗
        spiritConsume = float.Parse(data["SpiritConsume"].ToString()); //精魄消耗
    }
    //根据自身标签判定获取敌人主城
    public void GetMainCity()
    {
        characterManage = FindObjectOfType<CharacterManage>();
        foreach (var item in characterManage.mainCitys) //遍历两座主城
        {
            if (item.tag != tag) //和自己不一样的是敌方主城
            {
                EnemyMainCity = item;
                enemyTag = EnemyMainCity.tag;
            }
            else
            {
                //Color color = item.GetComponentInChildren<MeshRenderer>().material.color;
                //GetComponentInChildren<Renderer>().material.color = color;
            }
        }
    }
    //受伤方法
    public void Damage(float _ad, float _adp, float _cm, float _ap, float _app)
    {
        //物理伤害*暴击倍数
        float adDamage = (1 - (adr - _adp) / (adr - _adp + 100)) * _ad * _cm;
        //法术伤害
        float apDamage = (1 - (apr - _app) / (apr - _app + 100)) * _ap;
        //真实伤害
        float realDamage = adDamage + apDamage;
        if (hp > realDamage) //如果血量有得扣,则扣血
            hp -= realDamage;
        else if (hp > 0)//否则说明血量不够,角色死亡
        {
            hp = 0;
            if (hp == 0)
                isDie = true;
        }
        ShowHpSlider();
    }
    //显示血条
    public void ShowHpSlider()
    {
        //如果受伤，显示血条
        if (hp < maxHp && hp > 0)
            hpSlider.gameObject.SetActive(true);
        //如果血条显示，则同步显示血量
        if (hpSlider.gameObject.activeInHierarchy)
        {
            Transform hpObj = hpSlider.Find("Hp");
            Vector3 v = hpObj.localScale;
            v.x = hp / maxHp;
            hpObj.localScale = v;
        }
    }
    public abstract void GetAttackTarget(); //获取当前攻击目标   
    public abstract void Attack(); //攻击方法   
    public abstract void Death();
}
