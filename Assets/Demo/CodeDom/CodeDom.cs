//========================================================
//描 述：CodeDom.cs
//作 者：郑贤春
//时 间：2017/02/06 21:16:43 
//版 本：5.4.1f1 
//========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEditor;

public class CodeDom : MonoBehaviour
{

    void Start()
    {

    }
    //#region 主程序入口
    /// <summary>
    ///主程序入口 
    /// </summary>
    /// <param name = "args" ></ param >
    //static void Main(string[] args)
    //{
    //    1 > 实例化C#代码服务提供对象
    //    CSharpCodeProvider provider = new CSharpCodeProvider();
    //    2 > 声明编译器参数
    //    CompilerParameters parameters = new CompilerParameters();
    //    parameters.GenerateExecutable = false;
    //    parameters.GenerateInMemory = true;
    //    try
    //    {
    //        3 > 动态编译
    //        CompilerResults result = provider.CompileAssemblyFromSource(parameters, BuildCSharpCode());
    //        if (result.Errors.Count > 0)
    //        {
    //            Console.Write("编译出错！");
    //        }
    //        4 > 如果编译没有出错，此刻已经生成动态程序集LCQ.LCQClass
    //        5 > 开始玩C#映射
    //        Assembly assembly = result.CompiledAssembly;
    //        object obj = assembly.CreateInstance("LCQ.LCQClass");
    //        Type type = assembly.GetType("LCQ.LCQClass");
    //        6 > 获取对象方法
    //        MethodInfo method = type.GetMethod("Sum");
    //        object[] objParameters = new object[2] { 1, 5 };
    //        int iResult = Convert.ToInt32(method.Invoke(obj, objParameters));//唤醒对象，执行行为
    //        Console.Write(iResult);
    //        Console.Read();
    //    }
    //    catch (System.NotImplementedException ex)
    //    {
    //        Console.Write(ex.Message);
    //    }
    //    catch (System.ArgumentException ex)
    //    {
    //        Console.Write(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.Write(ex.Message);
    //    }
    //}
    //#endregion

    //#region 生成代码块
    /// <summary>
    /// 生成代码块
    /// </summary>
    /// <returns></returns>
    //private static string BuildCSharpCode()
    //{
    //    string fileName = AppDomain.CurrentDomain.BaseDirectory.Replace("Debug", string.Empty).Replace("Release", string.Empty) + "CodeFile.cs";
    //    string strCodeDom = File.ReadAllText(fileName);
    //    return strCodeDom;
    //}
    //#endregion
}

public class TestRunShell : ScriptableObject
{
    [MenuItem("Menu/RunShell")]
    public static void RunShell()
    {
        // 这里不开线程的话，就会阻塞住unity的主线程，当然如果你需要阻塞的效果的话可以不开
        Thread newThread = new Thread(new ThreadStart(RunShellThreadStart));
        newThread.Start();
    }

    private static void RunShellThreadStart()
    {
//        string cmdTxt = @"echo test
//notepad C:\Users\pc\Desktop\1.txt
//explorer.exe D:\
//pause";
        string cmdTxt = @"C:\Users\Administrator\Desktop\三英逗吕布破击源码\WindowsFormsApplication2\WindowsFormsApplication2\bin\Debug\WindowsFormsApplication2.exe 4545454
";

        RunCommand(cmdTxt);
        //RunProcessCommand("notepad", @"C:\Users\pc\Desktop\1.txt");
        //RunProcessCommand("explorer.exe", @"D:\");
    }

    private static void RunCommand(string command)
    {
        Process process = new Process();
        process.StartInfo.FileName = "powershell";
        process.StartInfo.Arguments = command;

        process.StartInfo.CreateNoWindow = false; // 获取或设置指示是否在新窗口中启动该进程的值（不想弹出powershell窗口看执行过程的话，就=true）
        process.StartInfo.ErrorDialog = true; // 该值指示不能启动进程时是否向用户显示错误对话框
        process.StartInfo.UseShellExecute = false;
        //process.StartInfo.RedirectStandardError = true;
        //process.StartInfo.RedirectStandardInput = true;
        //process.StartInfo.RedirectStandardOutput = true;

        process.Start();

        //process.StandardInput.WriteLine(@"explorer.exe D:\");
        //process.StandardInput.WriteLine("pause");

        //process.WaitForExit();
        //process.Close();
    }

    private static void RunProcessCommand(string command, string argument)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = command;
        start.Arguments = argument;

        start.CreateNoWindow = false;
        start.ErrorDialog = true;
        start.UseShellExecute = false;

        Process p = Process.Start(start);
        p.WaitForExit();
        p.Close();
    }
}
