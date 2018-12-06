using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
public class SoldierCharacter : Character
{
    NavMeshAgent agent; //寻路组件
    Animator anima; //动画组件
    AnimatorStateInfo animaState;
    float radius; //攻击目标的半径
    public void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        GetSelfData(); //获取数据            
        SetAgentData();
        SetTargetTag(); //判断标签
        anima = GetComponentInChildren<Animator>();
        hpSlider = transform.Find("BackGround");
        maxHp = hp; //获取最大血量
    }
    void SetAgentData()//获取寻路组件设置寻路速度和旋转速度  
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = rotateSpeed;
    }
    void SetTargetTag() //根据自身标签判定获取敌人主城和自身颜色
    {
        characterManage = FindObjectOfType<CharacterManage>();
        foreach (var item in characterManage.mainCitys) //遍历两座主城
        {
            if (item.tag == tag) //标签和自己一样的,设成同它一样的颜色
            {
                Color color = item.GetComponentInChildren<MeshRenderer>().material.color;
                GetComponentInChildren<Renderer>().material.color = color;
            }
            else //不一样则获取它
            {
                mainCity = item;
                attackTarget = mainCity;
            }
        }
    }
    public override void GetAttackTarget() //重写获取攻击目标
    {
        if (attackTarget == mainCity) //如果攻击目标位主城
        {
            //获取视野范围内所有物体
            Collider[] objs = Physics.OverlapSphere(transform.position, viewRange);
            //如果这些物体中的敌方标签,视为攻击目标
            float dis = Vector3.Distance(transform.position, mainCity.position);
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].tag == mainCity.tag && Vector3.Distance(transform.position, objs[i].transform.position) < dis)
                    attackTarget = objs[i].transform;
            }
        }
        else if (attackTarget == null || !attackTarget.GetComponent<CapsuleCollider>()) //如果攻击目标被死亡,重新认定主城为攻击目标
            attackTarget = mainCity;
    }
    bool AttackDirection() //攻击目标方位判定
    {
        //匀速面向目标的方向
        Quaternion dir = Quaternion.LookRotation(attackTarget.position - transform.position);
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
    AnimatorStateInfo stateinfo;
    public float animaLength; //动画时长
    public float animaSpeed;
    public override void Attack() //攻击
    {
        if (AttackDirection()) //如果对准目标
        {
            if (timer <= 0)
            {
                timer = 1 / attackSpeed; //进入冷却时间
                anima.SetTrigger("Attack");
                PlayAudio(audioPath);
                stateinfo = anima.GetCurrentAnimatorStateInfo(0); //获取当前动画状态
            }
            if (stateinfo.IsName("Attack")) //如果当前为攻击
            {
                animaLength = stateinfo.length; //获取攻击动画时长    
                //如果攻击动画时长超过攻击间隔,则让动画时长等于攻击间隔
                if (animaLength > 1 / attackSpeed)
                {
                    animaSpeed = 1 / (1 / attackSpeed / animaLength);
                    if (animaSpeed != 0)
                        anima.speed = animaSpeed;
                }
            }
            else
                anima.speed = 1;
        }
    }
    public override void Death()
    {
        if (!animaState.IsName("Death"))
        {
            anima.SetTrigger("Death");
            hpSlider.gameObject.SetActive(false);
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(agent);
        }
        if (animaState.IsName("Death"))
        {
            if (animaState.normalizedTime > 1)
                Destroy(gameObject);
        }
    }
    public void PlayAudio(string path) //播放声音
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        audioSource.PlayOneShot(clip);
    }
    void Update()
    {
        animaState = anima.GetCurrentAnimatorStateInfo(0);
        if (isDie)
            Death();
        timer -= Time.deltaTime; //攻击冷却在任何情况都在计算
        GetAttackTarget(); //获取攻击目标

        //如果与攻击目标距离大于攻击范围,则走向它
        Vector3 v = attackTarget.position;
        if (agent)
        {
            radius = attackTarget.GetComponent<CapsuleCollider>().bounds.size.x;
            if (Vector3.Distance(transform.position, new Vector3(v.x, transform.position.y, v.z)) > attackRange + radius)
            {
                agent.areaMask = 1;
                agent.SetDestination(attackTarget.position);
                anima.SetTrigger("Run");
            }
            //到达攻击范围则停下面向它攻击
            else
            {
                agent.areaMask = 0;
                Attack();
            }
        }
    }
}
