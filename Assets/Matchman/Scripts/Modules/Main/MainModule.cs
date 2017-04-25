// ========================================================
// 描 述：MainModule.cs 
// 作 者：郑贤春 
// 时 间：2017/02/25 14:58:30 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Matchman.Project
{
    public class MainModule : MonoBehaviour
    {
        private Button m_btnBegin;

        void Start()
        {
            this.m_btnBegin.onClick.AddListener(OnStartClick);
        }

        void OnStartClick()
        {

        }
    }
}

