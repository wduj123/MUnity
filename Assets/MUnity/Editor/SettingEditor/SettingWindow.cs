// ========================================================
// 描 述：SettingEditor.cs 
// 作 者： 
// 时 间：2017/01/02 16:49:28 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace MUnity.MEditor
{
    public class SettingWindow : EditorWindow
    {
        string m_developerName;

        void OnEnable()
        {
            this.m_developerName = Setting.GetDeveloperName();
        }

        void OnGUI()
        {
            m_developerName = EditorGUILayout.TextField("作者名:", m_developerName);
            if(GUILayout.Button("保存",GUILayout.Height(17)))
            {
                Setting.SetDeveloperName(m_developerName);
            }
        }
    }

    public class Setting
    {

        public const string DEVELOPER_NAME = "Game.Developer";

        public static string GetDeveloperName()
        {
            return PlayerPrefs.GetString(DEVELOPER_NAME);
        }

        public static void SetDeveloperName(string name)
        {
            PlayerPrefs.SetString(DEVELOPER_NAME, name);
            PlayerPrefs.Save();
        }
    }
}

