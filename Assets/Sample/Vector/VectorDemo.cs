// ========================================================
// 描 述：VectorDemo.cs 
// 作 者：郑贤春 
// 时 间：2017/03/03 09:39:24 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using MUnity.Utility;
using System.Collections.Generic;
using System;
using System.Reflection;

public class VectorDemo : MonoBehaviour
{

    private enum ViewMode
    {
        Vector3,
        Quaternion
    }

    private ViewMode m_viewMode = ViewMode.Quaternion;
    private List<GUIAcition> m_vector3Actions;
    private List<GUIAcition> m_quaternionActions;

    void Start()
    {
        Type type = this.GetType();
        this.m_quaternionActions = new List<GUIAcition>();
        this.m_vector3Actions = new List<GUIAcition>();

        MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo method = methods[i];
            if (method.Name.Contains("Vector3Test"))
            {
                this.m_vector3Actions.Add((top, height) =>
                {
                    GUI.Label(new Rect(0, top, 1000, height), method.Invoke(this, null).ToString());
                });
            }
            else if (method.Name.Contains("QuaternionTest"))
            {
                this.m_quaternionActions.Add((top, height) =>
                {
                    GUI.Label(new Rect(0, top, 1000, height), method.Invoke(this, null).ToString());
                });
            }
        }
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Vector3"))
        {
            this.m_viewMode = ViewMode.Vector3;
        }
        if (GUI.Button(new Rect(Screen.width - 100, 50, 100, 50), "Quaternion"))
        {
            this.m_viewMode = ViewMode.Quaternion;
        }

        GUI.color = Color.red;
        if (m_viewMode == ViewMode.Vector3)
        {
            MGUIUtility.ShowList(m_vector3Actions);
        }
        else if (m_viewMode == ViewMode.Quaternion)
        {
            MGUIUtility.ShowList(m_quaternionActions);
        }
    }

    void DrawLine(Vector3 start, Vector3 end, float width)
    {
        Texture2D tex = new Texture2D(1, 3, TextureFormat.ARGB32, true);
        tex.SetPixel(0, 0, Color.blue);
        tex.SetPixel(0, 1, Color.black);
        tex.SetPixel(0, 2, Color.blue);
        tex.Apply();
        float distance = (end - start).magnitude;
        float angle = Vector2.Angle(end - start, Vector2.right);
        Vector2 pivot = (start + end) * 0.5f;
        GUI.matrix = Matrix4x4.TRS(pivot, Quaternion.identity, Vector3.one);
        GUIUtility.RotateAroundPivot(angle, pivot);
        GUI.DrawTexture(new Rect(-distance * 0.5f, -width * 0.5f, distance, width), tex);
        GUI.matrix = Matrix4x4.identity;
    }

    string Vector3Test0()
    {
        Vector3 vector = new Vector3(3, 4, 0);
        return string.Format("{0}.magnitude = {1}", vector, vector.magnitude);
    }

    string Vector3Test1()
    {
        Vector3 from = Vector3.right;
        Vector3 to = new Vector3(3, 3, 0);
        return string.Format("Vector3.angle({0},{1}) = {2}", from, to, Vector3.Angle(from, to));
    }
    string Vector3Test2()
    {
        Vector3 from = Vector3.right;
        Vector3 to = new Vector3(-3, 3, 0);
        return string.Format("Vector3.angle({0},{1}) = {2}", from, to, Vector3.Angle(from, to));
    }
    string Vector3Test3()
    {
        Vector3 from = Vector3.right;
        Vector3 to = new Vector3(-3, -3, 0);
        return string.Format("Vector3.angle({0},{1}) = {2}", from, to, Vector3.Angle(from, to));
    }
    string Vector3Test4()
    {
        Vector3 from = Vector3.right;
        Vector3 to = new Vector3(3, -3, 0);
        return string.Format("Vector3.angle({0},{1}) = {2}", from, to, Vector3.Angle(from, to));
    }

    string Vector3Test5()
    {
        Vector3 vector = new Vector3(3, 4, 0);
        float maxLength = 2.5f;
        return string.Format("Vector3.ClampMagnitude({0},{1}) = {2}", vector, maxLength, Vector3.ClampMagnitude(vector, maxLength));
    }
    string Vector3Test6()
    {
        Vector3 vector = new Vector3(3, 4, 0);
        float maxLength = 5f;
        return string.Format("Vector3.ClampMagnitude({0},{1}) = {2}", vector, maxLength, Vector3.ClampMagnitude(vector, maxLength));
    }
    string Vector3Test7()
    {
        Vector3 vector = new Vector3(3, 4, 0);
        float maxLength = 6f;
        return string.Format("Vector3.ClampMagnitude({0},{1}) = {2}", vector, maxLength, Vector3.ClampMagnitude(vector, maxLength));
    }

    string Vector3Test8()
    {
        Vector3 vector = new Vector3(3, 4, 0);
        float distance = 2.5f;
        return string.Format("{0}.normalized * {1} = {2}", vector, distance, vector.normalized * distance);
    }
    string Vector3Test9()
    {
        Vector3 vector = new Vector3(3, 4, 0);
        float distance = 6f;
        return string.Format("{0}.normalized * {1} = {2}", vector, distance, vector.normalized * distance);
    }

    /// <summary>
    /// 运用左手定则
    /// </summary>
    /// <returns></returns>
    string Vector3Test10()
    {
        Vector3 a = new Vector3(0, 1, 0);
        Vector3 b = new Vector3(1, 0, 0);
        return string.Format("Vector3.Cross({0},{1}) = {2}", a, b, Vector3.Cross(a, b));
    }

    string Vector3Test11()
    {
        Vector3 a = new Vector3(1, 0, 0);
        Vector3 b = new Vector3(0, 1, 0);
        return string.Format("Vector3.Cross({0},{1}) = {2}", a, b, Vector3.Cross(a, b));
    }
    string Vector3Test12()
    {
        Vector3 a = new Vector3(1, 0, 0);
        Vector3 b = new Vector3(0, 2, 0);
        return string.Format("Vector3.Cross({0},{1}) = {2}", a, b, Vector3.Cross(a, b));
    }

    string Vector3Test13()
    {
        Vector3 a = new Vector3(1, 0, 0);
        Vector3 b = new Vector3(0, 2, 0);
        return string.Format("Vector3.Distance({0},{1}) = {2}", a, b, Vector3.Distance(a, b));
    }

    string QuaternionTest1()
    {
        return string.Format("Quaternion.identity = {0}", Quaternion.identity);
    }

    string QuaternionTest0()
    {
        Vector3 forward = new Vector3(0, 0, 1);
        Vector3 upwards = new Vector3(0, 1, 0);
        Quaternion q = Quaternion.LookRotation(forward, upwards);
        return string.Format("Quaternion.LookAtRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", forward, upwards, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
    string QuaternionTest2()
    {
        Vector3 forward = new Vector3(0, 0, 1);
        Vector3 upwards = new Vector3(0, -1, 0);
        Quaternion q = Quaternion.LookRotation(forward, upwards);
        return string.Format("Quaternion.LookAtRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", forward, upwards, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
    string QuaternionTest3()
    {
        Vector3 forward = new Vector3(1, 1, 0);
        Vector3 upwards = new Vector3(0, 1, 0);
        Quaternion q = Quaternion.LookRotation(forward, upwards);
        return string.Format("Quaternion.LookAtRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", forward, upwards, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }

    string QuaternionTest4()
    {
        Vector3 forward = new Vector3(1, 0, 1);
        Vector3 upwards = new Vector3(0, 1, 0);
        Quaternion q = Quaternion.LookRotation(forward, upwards);
        return string.Format("Quaternion.LookAtRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", forward, upwards, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }

    string QuaternionTest5()
    {
        Vector3 forward = new Vector3(0, 0, 1);
        Vector3 upwards = new Vector3(1, 1, 0);
        Quaternion q = Quaternion.LookRotation(forward, upwards);
        return string.Format("Quaternion.LookAtRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", forward, upwards, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
    string QuaternionTest6()
    {
        Vector3 forward = new Vector3(0, 0, 1);
        Vector3 upwards = new Vector3(-1, 1, 0);
        Quaternion q = Quaternion.LookRotation(forward, upwards);
        return string.Format("Quaternion.LookAtRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", forward, upwards, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
    string QuaternionTest7()
    {
        Vector3 forward = new Vector3(0, 0, 1);
        Vector3 upwards = new Vector3(1, -1, 0);
        Quaternion q = Quaternion.LookRotation(forward, upwards);
        return string.Format("Quaternion.LookAtRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", forward, upwards, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
    string QuaternionTest8()
    {
        Vector3 forward = new Vector3(0, 0, 1);
        Vector3 upwards = new Vector3(-1, -1, 0);
        Quaternion q = Quaternion.LookRotation(forward, upwards);
        return string.Format("Quaternion.LookAtRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", forward, upwards, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
    string QuaternionTest9()
    {
        float angle = 45;
        Vector3 axis = new Vector3(0, 0, 1);
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        return string.Format("Quaternion.AngleAxis({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", angle, axis, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
    string QuaternionTest10()
    {
        float angle = 45;
        Vector3 axis = new Vector3(0, 0, -1);
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        return string.Format("Quaternion.AngleAxis({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", angle, axis, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }

    string QuaternionTest11()
    {
        float x = 0;
        float y = 0;
        float z = 45;
        Quaternion q = Quaternion.Euler(0, 0, 45);
        return string.Format("Quaternion.Euler({0},{1},{2}) = {3} Angle x : y : z  = {4} : {5} : {6}", x, y, z, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
    string QuaternionTest12()
    {
        Vector3 from = new Vector3(1, 0, 0);
        Vector3 to = new Vector3(1, 1, 0);
        Quaternion q = Quaternion.FromToRotation(from, to);
        return string.Format("Quaternion.FromToRotation({0},{1}) = {2} Angle x : y : z  = {3} : {4} : {5}", from, to, q, q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);
    }
}
