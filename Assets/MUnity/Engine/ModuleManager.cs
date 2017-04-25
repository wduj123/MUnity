using UnityEngine;
using System;
using System.Collections;

namespace MUnity.Core
{
    public class ModuleManager : MonoBehaviour, IDisposable
    {
        bool initialized = false;

        public static ModuleManager moduleManager;
        public static ModuleManager Instance
        {
            get{
                if(moduleManager == null)
                {
                    
                }
                return moduleManager;
            }
        }

        public void OpenModule(string ModuleName,Bundle bundle)
        {

        }

        public void Dispose()
        {

        }

        void Initialize()
        {

        }

        void Awake()
        {

        }

        void OnEnable()
        {

        }

        void OnDisable()
        {

        }

        
    }
}

