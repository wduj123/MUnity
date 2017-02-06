using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MUnity.Utility
{
    public delegate void GUIAcition(int top, int height);
    public class MGUIUtility
    {
        public static int HEIGHT = 30;
        
        public static void ShowList(List<GUIAcition> actions)
        {
            if (actions == null) throw new ArgumentNullException("actions can't be null!");
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i](i*HEIGHT,HEIGHT);
            }
        }
    }
}

