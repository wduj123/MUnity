using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Clip),true)]
public class ClipEditor : Editor
{
    private Clip m_target;
    private float rangeX;

    public void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        this.m_target = target as Clip;
        Material material = Resources.Load<Material>("Shaders/Materials/Clip");
        if (this.m_target.Material == null)
        {
            this.m_target.Material = material;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        this.m_target.rangeX = EditorGUILayout.Slider(this.m_target.rangeX, 0, 1);
        this.m_target.Material.SetFloat("_RangeX", this.m_target.rangeX);
    }
}
