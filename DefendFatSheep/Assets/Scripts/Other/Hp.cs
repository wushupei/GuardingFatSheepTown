﻿using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class Hp : MonoBehaviour
{
    Transform mainCamera;
    void OnEnable()
    {
        mainCamera = GameObject.Find("Main Camera").transform;
    }
    void Update()
    {
        transform.rotation = mainCamera.rotation; //和主摄像同样的旋转
    }
}
