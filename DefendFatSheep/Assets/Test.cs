using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
public class Test : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target; //把被寻路的物体拖进去
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //agent.SetDestination(target.position); //向被寻路物体的坐标进发

    }
}
