using UnityEngine;
using System.Collections;

namespace MUnity.Net
{
    public class ResponseHandler : IResponseHandler
    {
        Callback mCallback;
        private ResponseHandler() { }

        public ResponseHandler(Callback callback)
        {
            this.mCallback = callback;
        }
    }
}
