using UnityEngine;
public class AnimaEvent : MonoBehaviour
{
    Character parentChara; //获取父物体的角色脚本
    public Transform target; //攻击目标 
    Transform muzzle; //枪口(远程士兵用)
    GameObject bullet; //子弹(远程士兵用)
    public float ad, adp, cc, cm, ap, app; //物理攻击,物理穿透,暴击率,暴击倍数,法强,法术穿透
    void OnEnable()
    {
        //获取枪口,加载子弹预制体
        muzzle = FindChildByName(transform, "Muzzle");
        bullet = Resources.Load("Prafabs/Bullet") as GameObject;
        //获取父物体的攻击数据
        parentChara = transform.parent.GetComponent<Character>();
        ad = parentChara.ad;
        adp = parentChara.adp;
        cc = parentChara.cc;
        cm = parentChara.cm;
        ap = parentChara.ap;
        app = parentChara.app;
    }
    Transform FindChildByName(Transform currentTF, string childName) //根据名字查找位置层级子物体
    {
        //使用递归,先查找第一层子物体,如果没有则遍历第一层子物体往下查
        Transform childTF = currentTF.Find(childName);
        if (childTF != null) return childTF;
        for (int i = 0; i < currentTF.childCount; i++)
        {
            childTF = FindChildByName(currentTF.GetChild(i), childName);
            if (childTF != null) return childTF;
        }
        return null;
    }
    public void AttackAnimaEvent(string AttackType) //攻击动画事件
    {
        //从父物体处获取攻击目标,并调用其受伤方法
        target = parentChara.attackTarget;
        if (target)
        {
            //如果出暴击,得到暴击倍数,否则暴击倍数为1
            if (Random.Range(0, 100) <= cc * 100)
            {
                print("暴击");
                cm = parentChara.cm;
            }
            else
                cm = 1;

            //近战直接调用目标受伤方法,远程则发射子弹,用子弹去攻击
            if (AttackType == "近")
                target.GetComponent<Character>().Damage(ad, adp, cm, ap, app);
            else if (AttackType == "远") //
            {
                //发射子弹,获取攻击数据
                GameObject b = Instantiate(bullet, muzzle.position, Quaternion.identity);
                b.GetComponent<Bullet>().GetData(target, ad, adp, cc, cm, ap, app);
            }
        }
    }
    //void Update()
    //{
    //    //实时获取自身攻击数据
    //    ad = parentChara.ad;
    //    adp = parentChara.adp;
    //    cc = parentChara.cc;
    //    cm = parentChara.cm;
    //    ap = parentChara.ap;
    //    app = parentChara.app;
    //}
}
