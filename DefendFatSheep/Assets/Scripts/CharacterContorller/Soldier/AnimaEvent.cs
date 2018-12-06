using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class AnimaEvent : MonoBehaviour
{
    Character parentChara; //获取父物体的角色脚本
    public Transform target; //目标   
    public float ad, adp, cc, cm, ap, app; //物理攻击,物理穿透,暴击率,暴击倍数,法强,法术穿透
    void OnEnable()
    {
        parentChara = transform.parent.GetComponent<Character>();
        
    }
    void Update()
    {
        GetData();
    }
    public void GetData() //获取自身攻击数据
    {
        ad = parentChara.ad;
        adp = parentChara.adp;
        cc = parentChara.cc;
        cm = parentChara.cm;
        ap = parentChara.ap;
        app = parentChara.app;
    }
    public void AttackAnimaEvent() //攻击动画事件
    {
        //从父物体处获取攻击目标,并调用其受伤方法
        target = parentChara.attackTarget;
        //如果没有出暴击,暴击倍数为1
        if (Random.Range(0, 100) > cc * 100)
            cm = 1;
        else
            print("暴击");
        target.GetComponent<Character>().Damage(ad, adp, cm, ap, app);
    }
}
