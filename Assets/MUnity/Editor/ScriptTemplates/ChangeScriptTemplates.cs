using UnityEngine;
using System.Collections;
using System.IO;
using MUnity.Editor;

public class ChangeScriptTemplates : UnityEditor.AssetModificationProcessor
{

    // 添加脚本注释模板
    private static string annotation = "// ========================================================\r\n";
    private static string str = 
    annotation +
    "// 描 述：#ScriptName# \r\n" + 
    "// 作 者：#CoderName# \r\n" + 
    "// 时 间：#CreateTime# \r\n" +
    "// 版 本：#UnityVersion# \r\n" +
    annotation;

    // 创建资源调用
    public static void OnWillCreateAsset(string path)
    {
        // 只修改C#脚本
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string allText = str;
            allText += File.ReadAllText(path);
            // 替换作者
            allText = allText.Replace("#ScriptName#", Path.GetFileName(path));
            // 替换作者
            allText = allText.Replace("#CoderName#", Setting.GetDeveloperName());
            // 替换字符串为系统时间
            allText = allText.Replace("#CreateTime#", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            // 替换版本信息
            allText = allText.Replace("#UnityVersion#", Application.unityVersion);
            string content = File.ReadAllText(path);
            if (content.Contains(annotation)) return;
            File.WriteAllText(path, allText);
        }
    }
}
