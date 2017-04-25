using UnityEngine;
using System.Collections;
using System;

namespace MUnity.Core
{
    public abstract class Module : MonoBehaviour, IModule
    {
        public virtual void OnCreate(Bundle bundle)
        {
            
        }

        public virtual void OnDestroy()
        {
            
        }

        public virtual void OnPause()
        {
            
        }

        public virtual void OnResume()
        {
            
        }

        public virtual void OpenModule()
        {

        }

        public virtual void OpenModule(Bundle bundle)
        {
            
        }
    }
}

