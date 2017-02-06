using UnityEngine;
using System.Collections;

namespace MUnity.Core
{
    public interface IModule
    {
        void OpenModule(Bundle bundle);

        void OnCreate(Bundle bundle);

        void OnResume();

        void OnPause();

        void OnDestroy();
    }
}
