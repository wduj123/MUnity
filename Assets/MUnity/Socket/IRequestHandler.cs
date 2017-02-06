using UnityEngine;
using System.Collections;

namespace MUnity.Net
{
    public interface IRequestHandler
    {
        byte[] Code();

        bool IsRequest();
    }
}


