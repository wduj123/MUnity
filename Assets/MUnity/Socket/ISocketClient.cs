using UnityEngine;
using System.Collections;

namespace MUnity.Net
{
    public interface ISocketClient
    {
        void Receive(byte[] bytes);
    }
}

