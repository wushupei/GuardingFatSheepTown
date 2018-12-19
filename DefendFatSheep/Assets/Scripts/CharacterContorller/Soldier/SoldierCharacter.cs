using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 士兵角色类,挂士兵身上
/// </summary>
public class SoldierCharacter : Character
{
    NavMeshAgent agent; //寻路组件
    NavMeshObstacle obstacle; //动态障碍组件
    Animator anima; //动画组件
    AnimatorStateInfo animaState;
    float radius; //攻击目标的半径
    public AudioClip attackSound, deathSound; //攻击和死亡音效
    public void OnEnable()
    {
        GetMainCity(); //判断标签
        audioSource = GetComponent<AudioSource>();
        GetSelfData(); //获取数据            
        SetAgentData();
        anima = GetComponentInChildren<Animator>();
        hpSlider = transform.Find("HpSlider");
        maxHp = hp; //获取最大血量
        LoadAllSound();
    }
    void LoadAllSound() //加载所有声音
    {
        //创建的物体名字后都有一个(clone),使用字符串将其移除
        name = name.Remove(name.IndexOf("("));
        AudioClip[] clips = Resources.LoadAll<AudioClip>("AudioClip/" + name);
        foreach (var item in clips) //根据音效资源的名称对变量赋值
        {
            switch (item.name)
            {
                case "Attack":
                    attackSound = item;
                    break;
                default:
                    deathSound = item;
                    break;
            }
        }
    }
    void SetAgentData()//获取寻路组件设置寻路速度和旋转速度  
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = rotateSpeed;
        obstacle = GetComponent<NavMeshObstacle>(); //获取动态障碍
    }
    public override void GetAttackTarget() //重写获取攻击目标
    {
        if (attackTarget == EnemyMainCity && EnemyMainCity) //如果攻击目标为主城
        {
            //获取视野范围内所有物体,如果这些物体中的敌方标签且比主城近,视为攻击目标
            Collider[] objs = Physics.OverlapSphere(transform.position, viewRange);
            float dis = Vector3.Distance(transform.position, EnemyMainCity.position); //和主城的距离
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].tag == enemyTag && Vector3.Distance(transform.position, objs[i].transform.position) < dis)
                {
                    attackTarget = objs[i].transform;
                    break;
                }
            }
        }
        //如果攻击目标死亡,重新认定主城为攻击目标
        else if (attackTarget == null || !attackTarget.GetComponent<CapsuleCollider>())
            attackTarget = EnemyMainCity;
    }
    bool AttackDirection() //攻击目标方位判定
    {
        //匀速面向目标的方向
        Vector3 v = new Vector3(attackTarget.position.x, transform.position.y, attackTarget.position.z);
        Quaternion dir = Quaternion.LookRotation(v - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, rotateSpeed * Time.deltaTime);
        //往前方发射射线,如果打到攻击目标,则可以攻击
        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * 5, transform.forward, attackRange + radius);
        foreach (var item in hits)
        {
            if (item.collider == attackTarget.GetComponent<Collider>())
                return true;
        }
        return false;
    }
    public override void Attack() //攻击
    {
        if (AttackDirection()) //如果对准目标
        {
            if (timer <= 0) //确认攻击冷却,开始攻击
            {
                timer = 1 / attackSpeed; //进入冷却时间
                anima.SetTrigger("Attack");
                audioSource.PlayOneShot(attackSound);
            }
        }
    }
    public void FindWay() //寻路
    {
        //如果与攻击目标距离大于攻击范围,则走向它
        Vector3 v = attackTarget.position; //攻击目标位置
        radius = attackTarget.GetComponent<CapsuleCollider>().bounds.size.x; //攻击目标半径
        if (Vector3.Distance(transform.position, new Vector3(v.x, transform.position.y, v.z)) > attackRange + radius)
        {
            //如果寻路器被禁用,则启用
            if (agent.enabled == false)
            {
                obstacle.enabled = false;
                agent.enabled = true;
            }
            else
                agent.SetDestination(attackTarget.position);
            //切换为跑步动画
            if (!animaState.IsName("Run"))
                anima.SetTrigger("Run");
        }
        else //到达攻击范围则停下面向它攻击
        {
            //禁用寻路,打开动态障碍
            if (agent.enabled == true)
            {
                agent.enabled = false;
                obstacle.enabled = true;
            }
            Attack();
        }
    }
    public override void Death() //死亡方法
    {
        //如果没在死亡动画,切换为死亡动画,并且销毁碰撞器,停止寻路
        if (!animaState.IsName("Death"))
        {
            anima.SetTrigger("Death");
            if (hpSlider.gameObject.activeInHierarchy)
                audioSource.PlayOneShot(deathSound);
            hpSlider.gameObject.SetActive(false);
            Destroy(GetComponent<CapsuleCollider>());
            agent.enabled = false;
        }
        else if (animaState.normalizedTime > 1)
            Destroy(gameObject);
    }
    void Update()
    {
        animaState = anima.GetCurrentAnimatorStateInfo(0); //获取当前动画状态
        timer -= Time.deltaTime; //攻击冷却在任何情况都在计算
        if (isDie) //如果死亡,调用死亡方法
            Death();

        if (!characterManage.gameOver && !isDie) //如果游戏未结束并且活着
        {
            GetAttackTarget(); //获取攻击目标
            FindWay();
        }
        else
        {
            if (!animaState.IsName("Idle") && hp > 0)
                anima.SetTrigger("Idle"); //播放待机动画
            agent.enabled = false; //停止寻路
        }
    }
}
