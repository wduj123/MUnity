using UnityEngine;
using System.Collections;

public class BuildMesh : MonoBehaviour
{
    private MeshFilter m_update;
    void Start()
    {
        Init();
    }

    void Update()
    {
        this.m_update.mesh = UpdateMesh();
    }

    void Init()
    {
        this.m_update = (MeshFilter)GameObject.Find("UpdateMesh").GetComponent(typeof(MeshFilter));
        // 得到MeshFilter对象，目前是空的。    
        MeshFilter triangle = (MeshFilter)GameObject.Find("TriangleMesh").GetComponent(typeof(MeshFilter));
        // 得到对应的网格对象    
        triangle.mesh = CreateTriangle();
        // 得到MeshFilter对象，目前是空的。    
        MeshFilter custom = (MeshFilter)GameObject.Find("CustomMesh").GetComponent(typeof(MeshFilter));
        // 得到对应的网格对象    
        custom.mesh = CreatRect(new Rect(0,0,2,1),0.2f);
    }

    private Mesh UpdateMesh()
    {
        // 三角形顶点的坐标数组    
        Vector3[] vertices = new Vector3[3];
        // 三角形顶点UV坐标  
        Vector2[] uv = new Vector2[vertices.Length];
        // 三角形顶点ID数组    
        int[] triangles = new int[3] {
            0, 1, 2
        };
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(-1, 1, 0);
        vertices[2] = new Vector3(1, 1, 0);
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(this.m_update.transform.position);
        if(viewportPos.y > 0.5)
        {
            mesh.colors = new Color[] {
                new Color(0, 0, 0, 0),
                new Color(0, 0, 0, 0),
                new Color(0, 0, 0, 0)
            };
        }
        return mesh;
    }

    /// <summary>
    /// 三角形面片
    /// </summary>
    private Mesh CreateTriangle()
    {
        // 三角形顶点的坐标数组    
        Vector3[] vertices = new Vector3[3];
        // 三角形顶点UV坐标  
        Vector2[] uv = new Vector2[vertices.Length];
        // 三角形顶点ID数组    
        int[] triangles = new int[3] {
            0, 1, 2
        };
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(-1, 1, 0);
        vertices[2] = new Vector3(1, 1, 1);
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }

    /// <summary>
    /// 自定义的任意面片
    /// </summary>
    private Mesh CreatCustom()
    {
        // 三角形顶点的坐标数组    
        Vector3[] vertices = new Vector3[4];
        // 三角形顶点UV坐标  
        Vector2[] uv = new Vector2[vertices.Length];
        // 三角形顶点ID数组    
        int[] triangles = new int[6] {
            0,1,2,
            0,1,3
        };
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(-1, 1, 0);
        vertices[2] = new Vector3(1, 1, 1);
        vertices[3] = new Vector3(0, 2, 0);
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        uv[3] = new Vector2(0.5f, 0.5f);

        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = i == 0 ? new Color(1, 1, 0, 0) : i > 1 ? Color.red : Color.blue;
        }
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.normals = vertices;
        mesh.colors = colors;
        return mesh;
    }


    /// <summary>
    /// 矩形框
    /// </summary>
    private Mesh CreatRect(Rect rect,float width)
    {
        // 三角形顶点的坐标数组    
        Vector3[] vertices = new Vector3[10];
        // 三角形顶点UV坐标  
        Vector2[] uv = new Vector2[vertices.Length];
        // 三角形顶点ID数组    
        int[] triangles = new int[8*3] {
            0,1,6,
            0,5,6,
            1,2,7,
            1,6,7,
            2,3,8,
            2,7,8,
            3,4,9,
            3,8,9
        };
        vertices[0] = new Vector3(0- width/2, 0 - width/2, 0);
        vertices[1] = new Vector3(rect.width + width/2, 0 - width/2, 0);
        vertices[2] = new Vector3(rect.width + width/2, rect.height + width/2, 0);
        vertices[3] = new Vector3(0 - width/2, rect.height + width/2, 0);
        vertices[4] = vertices[0];
        vertices[5] = new Vector3(0 + width/2, 0 + width/2, 0);
        vertices[6] = new Vector3(rect.width - width/2, 0 + width/2, 0);
        vertices[7] = new Vector3(rect.width - width/2, rect.height - width/2, 0);
        vertices[8] = new Vector3(0 + width/2, rect.height - width/2, 0);
        vertices[9] = vertices[5];
        for(int i=0;i < 2;i++)
        {
            uv[0 + 5 * i] = new Vector2(0, i);
            uv[1 + 5 * i] = new Vector2(rect.width / 2 / (rect.width + rect.height), i);
            uv[2 + 5 * i] = new Vector2(0.5f, i);
            uv[3 + 5 * i] = new Vector2(rect.width / 2 / (rect.width + rect.height) + 0.5f, i);
            uv[4 + 5 * i] = new Vector2(1,i);
        }
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.normals = vertices;
        return mesh;
    }
}
