using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed, viewSpeed; //镜头移动速度和缩放速度
    Camera cam;
    float viewValue;
    void Start()
    {
        cam = GetComponent<Camera>();
        viewValue = cam.fieldOfView;
    }
    void Update()
    {
        DragMove();
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        limitPos();
        SetDistance();
    }

    void DragMove() //拖拽移动
    {
        if (Input.GetMouseButton(2))
        {
            float dirX = Input.GetAxis("Mouse X");//鼠标往右返回正数,往左负数,不动返回0
            float dirZ = Input.GetAxis("Mouse Y");//往上正数,往下负数,不动返回0
            Move(-dirX, -dirZ);
        }
    }
    void Move(float x, float z) //键盘移动
    {
        transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * moveSpeed, Space.World);
    }
    void limitPos() //移动限制
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -100, 100);
        pos.z = Mathf.Clamp(pos.z, -140, 60);
        transform.position = pos;
    }
    void SetDistance() //拉伸镜头
    {
        float slider = Input.GetAxis("Mouse ScrollWheel");
        viewValue -= slider * Time.deltaTime * viewSpeed;
        viewValue = Mathf.Clamp(viewValue, 20, 80);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, viewValue, Time.deltaTime*5);
    }
}
