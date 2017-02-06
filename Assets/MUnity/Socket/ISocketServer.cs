using UnityEngine;
using System.Collections;

namespace MUnity.Net
{
    public interface ISocketServer
    {
        void Receive(byte[] bytes);
    }
}

