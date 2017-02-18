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
