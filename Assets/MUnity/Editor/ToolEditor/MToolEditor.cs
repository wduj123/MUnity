// ========================================================
// 描 述：MyOwnEditor.cs 
// 作 者：郑贤春 
// 时 间：2017/02/08 10:20:51 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using System.Reflection;
using System;

namespace MUnity.Editor
{
    public class MToolEditor : EditorWindow
    {
        [MenuItem("MUnity/Tools/小工具", false, -1)]
        public static void Create()
        {
            window = EditorWindow.GetWindow<MToolEditor>(true, "小工具");
            window.titleContent = new GUIContent("小工具");
            window.minSize = new Vector2(300, 300);
            window.maxSize = new Vector2(300, 300);
            window.Show();
        }

        static MToolEditor window;
        ViewState viewState = ViewState.Main;
        List<MEditor> m_editors;
        int m_toolIndex;

        void OnEnable()
        {
            m_editors = new List<MEditor>();

            MImageEditor imageEditor = new MImageEditor();
            m_editors.Add(imageEditor);

            MTextEditor textEditor = new MTextEditor();
            m_editors.Add(textEditor);

            ClassEditor classEditro = new ClassEditor();
            m_editors.Add(classEditro);

            MUnitResEditor unit = new MUnitResEditor();
            m_editors.Add(unit);
        }

        void OnDisable()
        {
            
        }

        void OnGUI()
        {
            if (viewState == ViewState.Main)
            {
                ShowGUI();
            }
            else if(viewState == ViewState.Tool)
            {
                if (GUILayout.Button("返回"))
                {
                    if (viewState == ViewState.Tool) viewState = ViewState.Main;
                }
                this.m_editors[m_toolIndex].ShowGUI();
            }
        }

        void ShowGUI()
        {
            for(int i= 0;i<this.m_editors.Count;i++)
            {
                int index = i;
                if(GUILayout.Button(this.m_editors[i].GetType().Name))
                {
                    viewState = ViewState.Tool;
                    m_toolIndex = index;
                }
            }
        }

        enum ViewState : int
        {
            Main = 0,
            Tool = 1
        }

    }

    public class MImageEditor : MEditor
    {
        object m_texObject;
        Texture2D m_texture2D;
        Transform m_transform;

        public void ShowGUI()
        {
            EditorGUI.BeginChangeCheck();
            {
                m_texture2D = EditorGUILayout.ObjectField(new GUIContent("Texture2D"), m_texture2D, typeof(Texture2D), true) as Texture2D;
                m_transform = EditorGUILayout.ObjectField(new GUIContent("Transform"), m_transform, typeof(Transform), true) as Transform;
            }
            if (EditorGUI.EndChangeCheck())
            {

            }
            if (GUILayout.Button("加载", GUILayout.Height(17)))
            {
                AddImages(this.m_texture2D, this.m_transform);
            }
        }

        void AddImages(Texture2D texture, Transform transform)
        {
            if (texture == null)
            {
                EditorUtility.DisplayDialog("提示", "Texture2D不能为空！", "确定");
                return;
            }
            Debug.Log(AssetDatabase.GetAssetPath(texture));
            UnityEngine.Object[] objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture));
            if (objs == null) return;
            foreach (UnityEngine.Object obj in objs)
            {
                Sprite sprite = obj as Sprite;
                if (sprite == null) continue;
                GameObject gameobject = new GameObject(obj.name);
                Image image = gameobject.AddComponent<Image>();
                Debug.Log(sprite == null ? "" : sprite.rect.ToString());
                image.sprite = sprite;
                if (transform != null)
                {
                    gameobject.transform.SetParent(transform);
                }
                gameobject.transform.localPosition = new Vector3(sprite.rect.x + sprite.rect.width * 0.5f - texture.width * 0.5f, sprite.rect.y + sprite.rect.height * 0.5f - texture.height * 0.5f, 0f);
                gameobject.transform.localScale = Vector3.one;
                gameobject.transform.localRotation = Quaternion.identity;
                image.SetNativeSize();
            }
        }
    }

    public class MTextEditor : MEditor
    {

        Transform m_transform;
        bool m_includeChildren = false;

        public void ShowGUI()
        {
            m_includeChildren = GUILayout.Toggle(m_includeChildren, "包含子对象");
            EditorGUI.BeginChangeCheck();
            {
                
                m_transform = EditorGUILayout.ObjectField(new GUIContent("Transform"), m_transform, typeof(Transform), true) as Transform;
            }
            if (EditorGUI.EndChangeCheck())
            {

            }
            if (GUILayout.Button("改变Text为LangText", GUILayout.Height(17)))
            {
                ChangeTextToLangText(this.m_transform, m_includeChildren);
            }
            if (GUILayout.Button("改变LangText为Text", GUILayout.Height(17)))
            {
                ChangLangTextToText(this.m_transform, m_includeChildren);
            }
        }

        void ChangeTextToLangText(Transform transform,bool includeChildren = false)
        {
            if (transform == null)
            {
                EditorUtility.DisplayDialog("提示", "transform不能为空！", "确定");
                return;
            }
            Text text = transform.GetComponent<Text>();
            if (text != null)
            {
                Assembly assembly = Assembly.Load("Assembly-CSharp");
                if (assembly == null) return;
                Type type = assembly.GetType("GG.Game.LangText");
                if (type == null) return;
                GameObject.DestroyImmediate(text);
                Component lText = transform.gameObject.AddComponent(type);
                string[] propertyNames = new string[] { "text", "alignment", "font", "supportRichText", "color", "fontSize", "fontStyle", "lineSpacing" };
                foreach(string propertyName in propertyNames)
                {
                    PropertyInfo property = type.GetProperty(propertyName);
                    if (property == null) continue;
                    property.SetValue(lText, property.GetValue(text,null), null);
                }
            }
            if (!includeChildren) return;
            for (int i = 0; i < transform.childCount; i++)
            {
                ChangeTextToLangText(transform.GetChild(i), includeChildren);
            }
        }

        void ChangLangTextToText(Transform transform,bool includeChildren)
        {
            if (transform == null)
            {
                EditorUtility.DisplayDialog("提示", "transform不能为空！", "确定");
                return;
            }
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            Type type = assembly.GetType("GG.Game.LangText");
            Component lText = transform.GetComponent(type);
            if (lText != null)
            {
                GameObject.DestroyImmediate(lText);
                Text text = transform.gameObject.AddComponent<Text>();
                string[] propertyNames = new string[] { "text", "alignment", "font", "supportRichText", "color", "fontSize", "fontStyle", "lineSpacing" };
                foreach (string propertyName in propertyNames)
                {
                    PropertyInfo property = type.GetProperty(propertyName);
                    if (property == null) continue;
                    property.SetValue(text, property.GetValue(lText, null), null);
                }
            }
            if (!includeChildren) return;
            for (int i = 0; i < transform.childCount; i++)
            {
                ChangLangTextToText(transform.GetChild(i), includeChildren);
            }
        }
    }

    public class MUnitResEditor : MEditor
    {
        public void ShowGUI()
        {
            if (GUILayout.Button("创建UnitRes", GUILayout.Height(17)))
            {
                //CreateUnits();
            }
        }

        void CreateUnits()
        {
            //ILogicDataDict<HeroBaseDB> dict  = LogicDataSystem.GetInstance().GetLogicDataDict<HeroBaseDB>(typeof(HeroBaseDB).Name);
            //using (var child = dict.GetEnumerator())
            //{
            //    while(child.MoveNext())
            //    {
            //        HeroBaseDB hero = child.Current.Value;
            //        string path = "Assets/2.Art/Textures/UnitRes";
            //        path = Path.Combine(path, hero.Id + ".asset");
            //        UnitRes temp = ScriptableObject.CreateInstance<UnitRes>();
            //        AssetDatabase.CreateAsset(temp, path);
            //        UnitResEditor.AutoSet(temp);

            //    }
            //}
        }
    }

    public class ClassEditor : MEditor
    {
        public void ShowGUI()
        {
            if (GUILayout.Button("动态生产类 并创建对象", GUILayout.Height(17)))
            {
                Test();
            }
        }

        void Test()
        {
            string classContent = "public class TestClass1{ }";
            string classExtion = "TestClass1.cs";
            string path = Application.dataPath + "/3.Scripts/Editor/ModuleEditor" + "/" + classExtion;
            FileStream stream = File.Open(path, FileMode.OpenOrCreate);
            byte[] data = System.Text.Encoding.Default.GetBytes(classContent);
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Close();
            //AssetDatabase.Refresh();

            //Assembly assembly = typeof(TestClass).Assembly;
            ////object obj = assembly.CreateInstance("TestClass1");
            //Debug.Log(obj == null ? "null" : obj.GetType().Name);
            //Assembly assembly = Assembly.Load("TestClass");
            //assembly.CreateInstance()
            //Aviator
        }
    }

    public class MUImageEditor : MEditor
    {
        public void ShowGUI()
        {
            
        }

        void ChangeUImageToImage(Transform transform)
        {

        }
    }

    public interface MEditor
    {
        void ShowGUI();
    }
}


