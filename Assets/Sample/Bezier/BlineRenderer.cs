// ========================================================
// 描 述：BezierMesh.cs 
// 作 者：郑贤春 
// 时 间：2017/02/09 12:33:20 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class BlineRenderer : MonoBehaviour
{
    public enum MeshDisposition
    {
        Continuous,
        Fragmented
    };

    [SerializeField]
    MeshDisposition meshDisposition = MeshDisposition.Fragmented;
    [SerializeField]
    float m_lineWidth = 0.25f;
    [SerializeField]
    float m_dividerHeight = 0.25f;
    [SerializeField]
    Vector3[] m_pathList;
    MeshFilter m_meshFilter;
    MeshDisposition m_preMeshDisposition;
    float m_preDividerHeight;
    float m_preLineWidth;
    BlinePointController m_pointController;

    void Start ()
    {
        Init();
    }
	
	void LateUpdate() {
        UpdateMesh();
    }

    public void Init()
    {
        this.m_pointController = new BlinePointController();
        this.m_meshFilter = GetComponent<MeshFilter>();
        int length = transform.childCount;
        UpdateMesh();
    }

    public void SetLineWidth(float lineWidth)
    {
        this.m_lineWidth = lineWidth;
    }

    public void SetDividerHeight(float height)
    {
        this.m_dividerHeight = height;
    }

    public void SetPath(Vector3[] path)
    {
        this.m_pathList = path;
        UpdatePath();
    }

    public Vector3[] GetPath()
    {
        return this.m_pathList;
    }

    public void UpdateMesh()
    {
        bool updateFlag = false;
        if (m_preMeshDisposition != meshDisposition)
        {
            m_preMeshDisposition = meshDisposition;
            updateFlag = true;
        }
        m_dividerHeight = Mathf.Max(0.001f, m_dividerHeight);
        if (m_preDividerHeight != m_dividerHeight)
        {
            m_preDividerHeight = m_dividerHeight;
            updateFlag = true;
            m_pointController.PointListAverage(this.m_pathList, m_dividerHeight);
        }
        if(m_preLineWidth != m_lineWidth)
        {
            m_preLineWidth = m_lineWidth;
            updateFlag = true;
        }
        if(updateFlag)
        {
            Vector3[] pathUp = m_pointController.GetPathUp(m_lineWidth);
            Vector3[] pathDown = m_pointController.GetPathDown(m_lineWidth);
            m_meshFilter.mesh = CreateMesh(pathUp, pathDown);
        }
    }

    public void UpdatePath()
    {
        m_pointController.PointListAverage(this.m_pathList, m_dividerHeight);
        Vector3[] pathUp = m_pointController.GetPathUp(m_lineWidth);
        Vector3[] pathDown = m_pointController.GetPathDown(m_lineWidth);
        m_meshFilter.mesh = CreateMesh(pathUp, pathDown);
    }

    Mesh CreateMesh(Vector3[] upPath,Vector3[] downPath)
    {
        int length = upPath.Length;
        // 三角形顶点的坐标数组    
        Vector3[] vertices = new Vector3[4 * (length - 1)];
        // 三角形顶点UV坐标  
        Vector2[] uv = new Vector2[vertices.Length];
        // 三角形顶点ID数组    
        int[] triangles = new int[2 * (length - 1) * 3];
        // 点的的顺逆时针决定了网格显示方向
        float lastDistance = 0;
        for (int i=0;i<length - 1;i++)
        {
            float distance = lastDistance + m_dividerHeight;
            triangles[6 * i + 0] = 4 * i + 2;
            triangles[6 * i + 1] = 4 * i + 1;
            triangles[6 * i + 2] = 4 * i + 0;
            triangles[6 * i + 3] = 4 * i + 1;
            triangles[6 * i + 4] = 4 * i + 2;
            triangles[6 * i + 5] = 4 * i + 3;
            vertices[4 * i + 0] = downPath[i];
            vertices[4 * i + 1] = downPath[i + 1]; 
            vertices[4 * i + 2] = upPath[i];
            vertices[4 * i + 3] = upPath[i + 1];
            if (meshDisposition == MeshDisposition.Continuous)
            {
                uv[4 * i + 0] = new Vector2(lastDistance / m_lineWidth, 0f);
                uv[4 * i + 1] = new Vector2(distance / m_lineWidth, 0f);
                uv[4 * i + 2] = new Vector2(lastDistance / m_lineWidth, 1f);
                uv[4 * i + 3] = new Vector2(distance / m_lineWidth, 1f);
            }
            else if (meshDisposition == MeshDisposition.Fragmented)
            { 
                uv[4 * i + 0] = new Vector2(0f, 0f);
                uv[4 * i + 1] = new Vector2(1f, 0f);
                uv[4 * i + 2] = new Vector2(0f, 1f);
                uv[4 * i + 3] = new Vector2(1f, 1f);
            }
            lastDistance = distance;
        }
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }
}
