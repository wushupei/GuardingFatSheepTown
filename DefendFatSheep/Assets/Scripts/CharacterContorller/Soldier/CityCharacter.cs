using System.Collections;

using System.Collections.
Generic;
using UnityEngine;


public class CityCharacter : Character
{
    Transform muzzle; //枪口
    public GameObject bullet;
    public void Start()
    {
        GetSelfData(); //获取数据
        SetTargetTag();
        muzzle = transform.Find("Muzzle");
        bullet = Resources.Load("Prafabs/Bullet") as GameObject;
        hpSlider = transform.Find("BackGround");
        maxHp = hp; //获取最大血量
    }
    void SetTargetTag() //根据自身标签判定获取敌人主城和自身颜色
    {
        characterManage = FindObjectOfType<CharacterManage>();
        foreach (var item in characterManage.mainCitys) //遍历两座主城
        {
            if (item.tag != tag) //标签和自己一样的,获取它
                mainCity = item;
        }
    }
    public override void GetAttackTarget()
    {
        if (attackTarget) //如果攻击目标不为空
        {
            //如果目标死亡或离开攻击范围,则攻击目标置为空
            if (!attackTarget.GetComponent<CapsuleCollider>() || Vector3.Distance(transform.position, attackTarget.position) > attackRange)
                attackTarget = null;
        }
        else
        {
            //获取视野范围内所有物体
            Collider[] objects = Physics.OverlapSphere(transform.position, attackRange);
            //如果这些物体中的敌方标签,视为攻击目标
            foreach (var obj in objects)
            {
                if (obj.tag == mainCity.tag)
                {
                    attackTarget = obj.transform;
                    break;
                }
            }
        }
    }
    public override void Attack()
    {
        if (attackTarget != null)
        {
            if (timer <= 0)
            {
                Bullet b = Instantiate(bullet, muzzle.position, Quaternion.identity).GetComponent<Bullet>();
                b.GetData(attackTarget, ad, adp, cc, cm, ap, app);
                timer = 1 / attackSpeed; //进入冷却时间
            }
        }
        timer -= Time.deltaTime; //攻击冷却在任何情况都在计算
    }
    public override void Death()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        GetAttackTarget();
        Attack();
        if (isDie)
            Death();
    }
}
