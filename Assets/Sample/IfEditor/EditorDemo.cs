using UnityEngine;
using System.Collections;

public class EditorDemo : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    #if UNITY_EDITOR
    void Reset()
    {
        GameObject.DestroyImmediate(this,true);
        UnityEditor.EditorUtility.DisplayDialog("错误提示：","不能在编辑模式下添加","确定");
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.AssetDatabase.SaveAssets();
    }
    #endif
}                 
