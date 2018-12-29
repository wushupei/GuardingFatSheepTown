using System.Collections;

using System.Collections.Generic;

using UnityEngine;

//挂子弹预制体上
public class Bullet : MonoBehaviour
{
    public float flySpeed; //子弹飞行速度
    public Transform target; //目标
    Vector3 targetPos;
    public float ad, adp, cc, cm, ap, app; //物理攻击,物理穿透,暴击率,暴击倍数,法强,法术穿透

    public void GetData(Transform _target, float _ad, float _adp, float _cc, float _cm, float _ap, float _app) //获取数据
    {
        target = _target;
        ad = _ad;
        adp = _adp;
        cc = _cc;
        cm = _cm;
        ap = _ap;
        app = _app;
    }
    void FlyToTarget() //子弹飞向目标
    {
        //获取目标位置,子弹飞到目标位置销毁自身
        if (target)
            targetPos = target.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * flySpeed);
        if (Vector3.Distance(transform.position, targetPos) <= 0.01f)
            Destroy(gameObject);
    }
    void Update()
    {
        FlyToTarget();
    }
    void OnDisable()
    {
        if (target)
            target.GetComponent<Character>().Damage(ad, adp, cm, ap, app);
    }
}
