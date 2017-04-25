// ========================================================
// 描 述：NewBehaviourScript.cs 
// 作 者：郑贤春 
// 时 间：2017/03/01 21:14:20 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    public Material mat;
    private Vector3 startVertex;
    private Vector3 mousePos;
    void Update()
    {
        mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Space))
            startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
        //OnPostRender();
    }
    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex);
        GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));
        GL.End();
        GL.PopMatrix();
    }
    void Example()
    {
        startVertex = new Vector3(0, 0, 0);
    }
}
