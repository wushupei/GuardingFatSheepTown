using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed;
    private void Update()
    {
        DragMove();
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        limitPos();
    }

    void DragMove()
    {
        if (Input.GetMouseButton(2))
        {
            float dirX = Input.GetAxis("Mouse X");//鼠标往右返回正数,往左负数,不动返回0
            float dirZ = Input.GetAxis("Mouse Y");//往上正数,往下负数,不动返回0
            Move(-dirX, -dirZ);
        }
    }
    private void Move(float x, float z)
    {
        transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * moveSpeed, Space.World);
    }
    void limitPos()
    {
        Vector3 pos = transform.position;
        if (pos.x > 100)
            pos.x = 100;
        else if (pos.x < -100)
            pos.x = -100;
        if (pos.z > 80)
            pos.z = 80;
        else if (pos.z < -120)
            pos.z = -120;
        transform.position = pos;
    }
}
