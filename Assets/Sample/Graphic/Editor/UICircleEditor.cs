// ========================================================
// 描 述：GraphicTestEditor.cs 
// 作 者：郑贤春 
// 时 间：2017/02/28 16:17:13 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UICircle))]
public class UICircleEditor : UnityEditor.UI.ImageEditor
{
    private SerializedProperty m_propertySize;

    void OnEnable()
    {
        this.m_propertySize = serializedObject.FindProperty("m_size");
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_propertySize);
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }
}
