using UnityEditor;
using UnityEngine;
public class Tools : MonoBehaviour
{
    [MenuItem("GameObject/3D Object/Triangle")] 
    static void CreateTriangle() //创建一个等边直角三角形
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Mesh mesh = cube.GetComponent<MeshFilter>().mesh; //报错,不影响运行
        Vector3[] vertices = mesh.vertices;
        //修改某些端点的位置
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            if (vertices[i] == new Vector3(0.5f, 0.5f, 0.5f))
                vertices[i] = new Vector3(-0.5f, 0.5f, 0.5f);
            else if (vertices[i] == new Vector3(0.5f, -0.5f, 0.5f))
                vertices[i] = new Vector3(-0.5f, -0.5f, 0.5f);
        }
        mesh.vertices = vertices;
        //改名+更换碰撞器
        cube.name = "Triangle";
        Undo.DestroyObjectImmediate(cube.GetComponent<BoxCollider>());
        cube.AddComponent<MeshCollider>().convex = true;
    }
}

