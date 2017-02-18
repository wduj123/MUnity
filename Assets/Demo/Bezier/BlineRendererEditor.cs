// ========================================================
// 描 述：BezierLine.cs 
// 作 者：郑贤春 
// 时 间：2017/02/10 19:46:48 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BlineRenderer))]
public class BlineRendererEditor : Editor
{
    BlineRenderer m_blineRenderer;
    Vector3[] m_pathList;

    float m_arrowSize = 1;

    void OnEnable()
    {
        this.m_blineRenderer = target as BlineRenderer;
        this.m_blineRenderer.Init();
        this.m_pathList = this.m_blineRenderer.GetPath();
    }

    void OnDisable()
    {

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        this.m_blineRenderer.UpdateMesh();
    }

    void OnSceneGUI()
    {
        for (int i = 0; this.m_pathList != null && i < this.m_pathList.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 position = Handles.PositionHandle(this.m_blineRenderer.transform.localToWorldMatrix.MultiplyPoint(this.m_pathList[i]), Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                this.m_pathList[i] = this.m_blineRenderer.transform.worldToLocalMatrix.MultiplyPoint(position);
                this.m_blineRenderer.SetPath(this.m_pathList);
            }
        }
    }

    void OnSceneFunc(SceneView sceneView)
    {

    }
}
