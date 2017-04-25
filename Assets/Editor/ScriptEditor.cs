using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ScriptEditor: EditorWindow{

    public static int focusedWindowId
    {
        get
        {
            System.Reflection.FieldInfo field = typeof(GUI).GetField("focusedWindow",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Static);
            if(field == null)
            {
                //Debug.Log("xxxx");
                return -1;
            }
            return (int)field.GetValue(null);
        }
    }

    [MenuItem("Window/ScriptEditor")]
    static void init()
    {
        ScriptEditorDebughelpers.openScriptEditor();
    }

    Rect wr = new Rect(100, 100, 200, 100);
    Rect wr2 = new Rect(300, 100, 200, 100);
    Rect wr3 = new Rect(150, 300, 200, 100);
    Rect wr4 = new Rect(250, 300, 200, 100);

    private Vector2 m_scrollPos;

    List<WindowObject> m_windows = new List<WindowObject>();
    void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            WindowObject obj = ScriptableObject.CreateInstance<WindowObject>();
            obj.index = i;
            this.m_windows.Add(obj);
        }
    }

    void doWindow(int id)
    {
        GUI.Button(new Rect(0, 30, 150, 50), "Wee!");
        GUI.DragWindow();
    }

    void curveFromTo(Rect wr, Rect wr2, Color color, Color shadow)
    {
        Drawing.bezierLine(
            new Vector2(wr.x + wr.width, wr.y + 3 + wr.height / 2),
            new Vector2(wr.x + wr.width + Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr.y + 3 + wr.height / 2),
            new Vector2(wr2.x, wr2.y + 3 + wr2.height / 2),
            new Vector2(wr2.x - Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr2.y + 3 + wr2.height / 2), shadow, 5, true,20);
        Drawing.bezierLine(
            new Vector2(wr.x + wr.width, wr.y + wr.height / 2),
            new Vector2(wr.x + wr.width + Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr.y + wr.height / 2),
            new Vector2(wr2.x, wr2.y + wr2.height / 2),
            new Vector2(wr2.x - Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr2.y + wr2.height / 2), color, 2, true,20);
    }
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        {
            
            GUILayout.Label("xxxx");

            


            m_scrollPos = GUI.BeginScrollView(new Rect(100, 0, position.width - 100, position.height), m_scrollPos, new Rect(0, 0, position.width + 100, position.height + 100),GUIStyle.none,GUIStyle.none);

            if (Event.current.type == EventType.Repaint)
            {
                GUIStyle backgroundStyle = "flow background";
                backgroundStyle.Draw(new Rect(0, 0, position.width + 100, position.height + 100), false, false, false, false);
            }

            Color s = new Color(0.4f, 0.4f, 0.5f);
            curveFromTo(wr, wr2, new Color(0.3f, 0.7f, 0.4f), s);
            curveFromTo(wr2, wr3, new Color(0.7f, 0.2f, 0.3f), s);
            Handles.DrawLine(Vector3.zero, new Vector3(200, 200, 0));
            
            Drawing.DrawLine(Vector3.zero, new Vector3(200, 200, 0), new Color(1, 1, 1, 0.3F), 6f, true);
            DrawGrid(new Rect(0, 0, position.width + 100, position.height + 100));

            Texture2D tex = new Texture2D((int)wr2.width, 50);
            for (int i = 0; i < tex.width; i++)
            {
                for (int j = 0; j < tex.height; j++)
                {
                    tex.SetPixel(i, j, new Color(1, 0, 0, (float)j / tex.height));
                }
            }
            tex.Apply();

            Drawing.DrawLine(Vector3.zero, new Vector3(200, 300, 0), 2);
            GUIStyle ms = new GUIStyle((GUIStyle)"flow node 2");
            GUIUtility.RotateAroundPivot(45, new Vector2(100, 0));
            GUI.DrawTexture(new Rect(100, 0, tex.width * 2, tex.height * 2), tex);
            GUI.matrix = Matrix4x4.identity;
            BeginWindows();
            {
                wr = GUI.Window(0, wr, doWindow, "hello");
                
                ms.onHover.background = ((GUIStyle)"flow node 2 on").normal.background;
                ms.onNormal.background = ((GUIStyle)"flow node 2 on").normal.background;
                wr2 = GUI.Window(1, wr2, doWindow, "xxx");
                wr3 = GUI.Window(2, wr3, doWindow, "!");
                //wr4 = GUI.Window(2, wr4, doWindow, "!");

            }
            EndWindows();
            //GUI.FocusWindow(1);
            if (focusedWindowId >= 0)
            {
                Selection.activeObject = this.m_windows[focusedWindowId];
            }
            else
            {
                Selection.activeObject = null;
            }

            //Event currentEvent = Event.current;
            //if (currentEvent.button != 2)
            //    return;

            //int controlID = GUIUtility.GetControlID(FocusType.Passive);

            //switch (currentEvent.rawType)
            //{
            //    case EventType.mouseDown:
            //        GUIUtility.hotControl = controlID;
            //        currentEvent.Use();
            //        break;
            //    case EventType.mouseUp:
            //        if (GUIUtility.hotControl == controlID)
            //        {
            //            GUIUtility.hotControl = 0;
            //            currentEvent.Use();
            //        }
            //        break;
            //    case EventType.mouseDrag:
            //        //if (GUIUtility.hotControl == controlID)
            //        //{
            //        //    UpdateScrollPosition(scrollPosition - currentEvent.delta);
            //        //    currentEvent.Use();
            //        //}
            //        break;
            //}

            GUI.EndScrollView();
        }
        GUILayout.EndHorizontal();
        
    }

    void DrawGrid(Rect rect)
    {
        if (Event.current.type != EventType.Repaint)
        {
            return;
        }
        float divider = 12f;
        int dividerCount = 10;
        int row = (int)(rect.height / divider);
        int col = (int)(rect.width / divider);
        row = row % 2 == 0 ? row + 1 : row;
        col = col % 2 == 0 ? col + 1 : col;
        Color color1 = new Color(0, 0, 0, 0.65f);
        Color color2 = new Color(0, 0, 0, 0.25f);
        for(int i=0;i<row;i++)
        {
            float rowPos = rect.height * 0.5f - ((row - 1) * 0.5f - i) * divider;
            DrawLine(new Vector3(0,rowPos), new Vector3(rect.width, rowPos), (i - (row -1) * 0.5f) % dividerCount == 0 ? color1 : color2);
        }
        for(int i=0;i<col;i++)
        {
            float colPos = rect.width * 0.5f - ((col - 1) * 0.5f - i) * divider;
            DrawLine(new Vector3(colPos,0f), new Vector3(colPos,rect.height), (i - (col - 1) * 0.5f) % dividerCount == 0 ? color1 : color2);
        }
        
    }

    void DrawLine(Vector3 start,Vector3 end,Color color)
    {
        //CreateMaterial().SetPass(0);
        //GL.PushMatrix();
        //GL.MultMatrix(Handles.matrix);
        //GL.Color(color);
        //GL.Begin(GL.LINES);
        //GL.Vertex(start);
        //GL.Vertex(end);
        //GL.End();
        //GL.PopMatrix();
        Handles.color = color;
        Handles.DrawLine(start, end);
    }
}

public class WindowObject : ScriptableObject
{
    [SerializeField]
    public int index;
}
