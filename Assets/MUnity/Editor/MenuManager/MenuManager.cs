using MUnity.MEditor.MGenerator;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace MUnity.MEditor
{
    /// <summary>
    /// 菜单统一管理类
    /// </summary>
    public class MenuManager
    {
        #region Assets
        [MenuItem("Assets/资源工具/自动切图", false,0)]
        public static void TrimTexture()
        {
            TextureEditor.TrimTexture();
        }

        [MenuItem("Assets/资源工具/打包图集", false, 0)]
        public static void PackTexture()
        {
            int count = 1000;
            string info = "";
            float progress = 0;
            for (int i = 0; i < count; i++)
            {
                info = "切图中" + (int)((float)i / count * 100) + "%";
                progress = (float)i / count;
                EditorUtility.DisplayProgressBar("正在切图", info, progress);
            }
            EditorUtility.ClearProgressBar();
        }

        [MenuItem("Assets/资源工具/打包音频", false, 0)]
        public static void PackAudio()
        {
            int count = 1000;
            string info = "";
            float progress = 0;
            for (int i = 0; i < count; i++)
            {
                info = "切图中" + (int)((float)i / count * 100) + "%";
                progress = (float)i / count;
                EditorUtility.DisplayProgressBar("正在切图", info, progress);
            }
            EditorUtility.ClearProgressBar();
        }

        [MenuItem("Assets/资源工具/打包材质", false, 0)]
        public static void PackMaterial()
        {
            int count = 1000;
            string info = "";
            float progress = 0;
            for (int i = 0; i < count; i++)
            {
                info = "切图中" + (int)((float)i / count * 100) + "%";
                progress = (float)i / count;
                EditorUtility.DisplayProgressBar("正在切图", info, progress);
            }
            EditorUtility.ClearProgressBar();
        }
        #endregion

        #region MUnity

        [MenuItem("MUnity/资源工具/打包材质", false, 0)]
        public static void PackMaterial1()
        {
            int count = 1000;
            string info = "";
            float progress = 0;
            for (int i = 0; i < count; i++)
            {
                info = "切图中" + (int)((float)i / count * 100) + "%";
                progress = (float)i / count;
                EditorUtility.DisplayProgressBar("正在切图", info, progress);
            }
            EditorUtility.ClearProgressBar();
        }

        [MenuItem("MUnity/设置", false, 0)]
        public static void ShowSettingWindow()
        {
            SettingWindow window = EditorWindow.GetWindow<SettingWindow>(true,"设置");
            window.maxSize = new Vector2(320, 400);
            window.minSize = new Vector2(320, 400);
        }

        [MenuItem("MUnity/Test", false, 0)]
        public static void Test()
        {
            MethodGenerator method = new MethodGenerator("Test");
            method.SetAuthority(MethodAuthorityType.Internal);
            method.SetDecorate(MethodDecorateType.Virtual);
            method.SetParms(new string[] { "int", "string" });
            method.SetReturn("bool");
            MethodGenerator method1 = new MethodGenerator("Test1");
            method1.SetAuthority(MethodAuthorityType.Internal);
            method1.SetDecorate(MethodDecorateType.Virtual);
            method1.SetParms(new string[] { "int", "string" });
            method1.SetReturn("bool");
            ClassGernerator textClass = new ClassGernerator("TestClass");
            textClass.SetUsingName(new string[] { "xxxx", "aaaa" });
            textClass.SetBaseClass("xxcvsdf");
            textClass.SetDecorate(ClassDecorateType.Abstract);
            textClass.SetAuthority(ClassAuthorityType.Public);
            textClass.SetInterfaces(new string[] { "asdfsadf", "asdfasdf" });
            textClass.SetNamespace("masdjf");
            textClass.AddMethod(method);
            textClass.AddMethod(method1);
            string classValue = textClass.ToString();
            Debug.Log(classValue);
        }
        #endregion
    }
}

