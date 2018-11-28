using UnityEngine;
using UnityEngine.AI;
public class SoldierCharacter : Character
{
    private NavMeshAgent agent; //寻路组件
    public void OnEnable()
    {
        SetTargetTag();
        GetSelfData();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed; //将移动速度设为寻路速度  
        agent.angularSpeed = rotateSpeed;
    }
    public override void GetAttackTarget() //重写获取目标敌人的方法
    {
        //根据标签获取所有敌人,找到离自己最近的一个作为攻击目标
        GameObject[] allTarget = GameObject.FindGameObjectsWithTag(targetTag);
        attackTarget = allTarget[0].transform;
        foreach (var item in allTarget)
        {
            if (Vector3.Distance(transform.position, item.transform.position) < Vector3.Distance(transform.position, attackTarget.position))
                attackTarget = item.transform;
        }
    }
    public void MoveToGetAttackTarget() //向目标敌人寻路
    {
        agent.SetDestination(attackTarget.position);
    }
    public void LookAtAttackTarget() //在攻击时面向目标
    {
        //匀速面向目标的方向
        Quaternion dir = Quaternion.LookRotation(attackTarget.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, rotateSpeed * Time.deltaTime);
    }
    void Update()
    {
        //没有目标敌人获取一个
        if (!attackTarget)
            GetAttackTarget();
        //如果目标敌人没有进入攻击范围,继续前行
        else if (Vector3.Distance(transform.position, new Vector3(attackTarget.position.x, transform.position.y, attackTarget.position.z)) > attackRange)
        {
            agent.areaMask = 1;
            MoveToGetAttackTarget();
        }
        //到达攻击范围则停下并攻击
        else
        {
            agent.areaMask = 0;
            Attack();
            LookAtAttackTarget();
        }
    }
}
