// ========================================================
// 描 述：CsharpGernerator.cs 
// 作 者：郑贤春 
// 时 间：2017/01/02 19:09:31 
// 版 本：5.4.1f1 
// ========================================================
using MUnity.Editor.AssetBundle;
using MUnity.Editor.CodeGenerator;
using MUnity.Editor.Utility;
using MUnity.Os;
using MUnity.Utility;
using System;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace MUnity.Editor
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
            UnityEngine.Object[] objs = Selection.objects;
            AssetBundlePacker.PackTogether(objs);
        }

        [MenuItem("Assets/资源工具/打包图集", true, 0)]
        public static bool CheckPackTexture()
        {
            if (Selection.objects == null || Selection.objects.Length < 1) return false;
            return true;
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

		[MenuItem("Assets/Excel/ExcelToFieldClass",false,0)]
		public static void ExcelToFieldClass()
		{
            if (Selection.objects == null || Selection.objects.Length > 1)
                return;
            UnityEngine.Object obj = Selection.activeObject;
            string path = AssetDatabase.GetAssetPath(obj);
            path = UnityEngine.Application.dataPath + path.Replace("Assets", "");
            if (!path.EndsWith(".xlsx")) return;
            ExcelUtility.ToFieldClass(path);
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Excel/ExcelToFieldData", false, 0)]
        public static void ExcelToFieldData()
        {
            if (Selection.objects == null || Selection.objects.Length > 1)
                return;
            UnityEngine.Object obj = Selection.activeObject;
            string path = AssetDatabase.GetAssetPath(obj);
            path = UnityEngine.Application.dataPath + path.Replace("Assets", "");
            if (!path.EndsWith(".xlsx")) return;
            ExcelUtility.ToFieldObjectDict(path);
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Excel/ExcelToProtoClass",false, 0)]
		public static void ExcelToProtoClass()
		{
            if(Selection.objects == null || Selection.objects.Length > 1)
                return;
            UnityEngine.Object obj = Selection.activeObject;
            string path = AssetDatabase.GetAssetPath(obj);
            path = UnityEngine.Application.dataPath + path.Replace("Assets", "");
            if (!path.EndsWith(".xlsx")) return;
            ExcelUtility.ToProtoClass(path);
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Excel/ExcelToProtoData", false, 0)]
        public static void ExcelToProtoData()
        {
            if (Selection.objects == null || Selection.objects.Length > 1)
                return;
            UnityEngine.Object obj = Selection.activeObject;
            string path = AssetDatabase.GetAssetPath(obj);
            path = UnityEngine.Application.dataPath + path.Replace("Assets", "");
            if (!path.EndsWith(".xlsx")) return;
            object protoDict = ExcelUtility.ToProtoObjectDict(path);
            Type type = protoDict.GetType();
            string savePath = path.Replace(".xlsx", ".dat");
            ProtoUtility.ProtoDataToFile(savePath, protoDict);
            object dict1 = ProtoUtility.FileToProtoData(savePath, type);
            AssetDatabase.Refresh();
        }

		[MenuItem("Assets/Excel/ExcelToFieldClass", true, 0)]
        [MenuItem("Assets/Excel/ExcelToFieldData", true, 0)]
        [MenuItem("Assets/Excel/ExcelToProtoClass", true, 0)]
        [MenuItem("Assets/Excel/ExcelToProtoData", true, 0)]
        public static bool CheckExcelSelection()
		{
			if (Selection.objects == null || Selection.objects.Length > 1)
				return false;
            UnityEngine.Object obj = Selection.activeObject;
			string path = AssetDatabase.GetAssetPath(obj);
			if (!path.EndsWith (".xlsx"))
				return false;
			return true;
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

        [MenuItem("MUnity/CodeGenerator/GenerateCode1", false, 0)]
        public static void GenerateCode1()
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
            ClassGenerator textClass = new ClassGenerator("TestClass");
            textClass.SetUsingName(new string[] { "xxxx", "aaaa" });
            textClass.SetBaseClass("xxcvsdf");
            textClass.SetDecorate(ClassDecorateType.Abstract);
            textClass.SetAuthority(AuthorityType.Public);
            textClass.SetInterfaces(new string[] { "asdfsadf", "asdfasdf" });
            textClass.SetNamespace("masdjf");
            textClass.AddMethod(method);
            textClass.AddMethod(method1);
            string classValue = textClass.ToString();
            TxtUtility.StringToFile(classValue);
        }

        [MenuItem("MUnity/CodeGenerator/GenerateCode2", false, 0)]
        public static void GenerateCode2()
        {
            FieldGenerator field = new FieldGenerator();
            field.SetAuthority(AuthorityType.Private);
            field.SetFieldName("m_xxxx");
            field.SetFieldType("bool");
            FieldGenerator field1 = new FieldGenerator();
            field1.SetAuthority(AuthorityType.Private);
            field1.SetFieldName("m_xxxx");
            field1.SetFieldType("bool");
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
            ClassGenerator textClass = new ClassGenerator("TestClass");
            //textClass.SetUsingName(new string[] { "xxxx", "aaaa" });
            //textClass.SetNamespace("masdjf");
            textClass.SetBaseClass("xxcvsdf");
            textClass.SetDecorate(ClassDecorateType.Abstract);
            textClass.SetAuthority(AuthorityType.Public);
            textClass.SetInterfaces(new string[] { "asdfsadf", "asdfasdf" });
            textClass.AddMethod(method);
            textClass.AddMethod(method1);
            textClass.AddField(field);
            textClass.AddField(field1);
            string classValue = textClass.ToString();
            TxtUtility.StringToFile(classValue);
            //Debug.Log(classValue);
        }

        [MenuItem("MUnity/CodeGenerator/GenerateCode3", false, 0)]
        public static void GenerateCode3()
        {
            ProtoGenerator textClass = new ProtoGenerator("TestClass");
            textClass.SetNamespace("MM");
            textClass.AddProperty("id", "int");
            textClass.AddProperty("id1", "int");
            string classValue = textClass.ToString();
            TxtUtility.StringToFile(classValue);
            //Debug.Log(classValue);
        }

        [MenuItem("MUnity/Shell/RunShellDemo",false,0)]
        public static void RunShell()
        {
            string command = @"C:\Users\Administrator\Desktop\三英逗吕布破击源码\WindowsFormsApplication2\WindowsFormsApplication2\bin\Debug\WindowsFormsApplication2.exe 4545454";
            ShellUtility.RunAsyncShell(command);
        }

        [MenuItem("MUnity/Shell/RunShellDemo1",false,0)]
        public static void RunShell1()
        {
            string command = @"C:\Users\Administrator\Desktop\TxtGenerator\TxtGenerator\bin\Debug\TxtGenerator.exe";
            ShellUtility.RunShellInSilence(command);
            Thread.Sleep(100);
            string text = TxtUtility.FileToString("C:/222.txt");
            Debug.Log(text);
        }
        #endregion
    }
}

