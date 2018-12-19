using UnityEngine;
public class CityCharacter : Character
{
    Transform muzzle; //枪口
    GameObject bullet; //子弹
    void Start()
    {
        GetSelfData(); //获取数据
        GetMainCity();
        muzzle = transform.Find("Muzzle");
        bullet = Resources.Load("Prafabs/Bullet") as GameObject;
        hpSlider = transform.Find("HpSlider");
        maxHp = hp; //获取最大血量
    }
    public override void GetAttackTarget() //获取攻击目标
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
                if (obj.tag == enemyTag)
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
                b.GetData(attackTarget, ad, adp, cc, cm, ap, app); //防御塔没有暴击率
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
        if (!characterManage.gameOver) //如果游戏为结束则攻击
        {
            GetAttackTarget();
            Attack();
        }
        if (isDie)
            Death();
    }
}
