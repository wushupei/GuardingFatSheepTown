using UnityEngine;
using UnityEngine.AI;
public class SoldierCharacter : Character
{
    NavMeshAgent agent; //寻路组件
    Animator anima;
    public override void OnEnable()
    {
        base.OnEnable();
        //获取寻路组件设置寻路速度和旋转速度  
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = rotateSpeed;
        anima = GetComponentInChildren<Animator>();
    }

    public override void GetAttackTarget() //重写获取攻击目标
    {
        if (attackTarget == mainCity) //如果攻击目标位主城
        {
            //获取视野范围内所有物体
            Collider[] objects = Physics.OverlapSphere(transform.position, viewRange);
            //如果这些物体中谁有敌方标签,则将它视为攻击目标
            foreach (var obj in objects)
            {
                if (obj.CompareTag(mainCity.tag))
                {
                    attackTarget = obj.transform;
                    break;
                }
            }
        }
        else if (attackTarget == null) //如果攻击目标被消灭,重新获取主城为攻击目标
            attackTarget = mainCity;
    }
    public void LookAtAttackTarget() //在攻击时面向目标
    {
        //匀速面向目标的方向
        Quaternion dir = Quaternion.LookRotation(attackTarget.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, rotateSpeed * Time.deltaTime);
    }
    void Update()
    {
        timer -= Time.deltaTime; //攻击冷却在任何情况都在计算
        GetAttackTarget(); //获取攻击目标

        //如果与攻击目标距离大于攻击范围,则走向它
        Vector3 v = attackTarget.position;
        if (Vector3.Distance(transform.position, new Vector3(v.x, transform.position.y, v.z)) > attackRange)
        {
            agent.areaMask = 1;
            agent.SetDestination(attackTarget.position);
        }
        //到达攻击范围则停下面向它攻击
        else
        {
            agent.areaMask = 0;
            LookAtAttackTarget();
            Attack();
        }
    }
}
