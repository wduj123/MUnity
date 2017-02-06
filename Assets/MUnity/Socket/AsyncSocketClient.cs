using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace MUnity.Net
{
    public delegate void Callback(object data);
    public abstract class AsyncSocketClient<T> : ISocketClient, IDisposable
    {

        SocketClient client;

        Dictionary<IRequestHandler, Callback> mHandlerDict;

        protected abstract T Decode(byte[] bytes);

        protected abstract byte[] Encode(T obj);

        public AsyncSocketClient()
        {
            if (client == null)
            {
                client = new SocketClient();
                client.SetSocketClient(this);
            }
        }

        public void Post(T obj)
        {
            byte[] bytes = Encode(obj);
            client.SendMessage(bytes);
        }

        public void Get(IRequestHandler send, Callback callback)
        {
            if (mHandlerDict == null) mHandlerDict = new Dictionary<IRequestHandler, Callback>();
            mHandlerDict.Add(send, callback);
        }

        public void Get(IRequestHandler send, IResponseHandler recive)
        {
            byte[] bytes = send.Code();
            client.SendMessage(bytes);
        }

        public void Post(IRequestHandler send, IResponseHandler recive)
        {
            byte[] bytes = send.Code();
            client.SendMessage(bytes);
        }

        public void Dispose()
        {
            
        }

        public void ShutDown()
        {
            client.Shutdown();
        }

        void ISocketClient.Receive(byte[] bytes)
        {
            object data = Decode(bytes);
            if (this.mHandlerDict == null) return;
            foreach (IRequestHandler handler in this.mHandlerDict.Keys)
            {
                if (handler.IsRequest())
                {
                    this.mHandlerDict[handler](data);
                    this.mHandlerDict.Remove(handler);
                    return;
                }
            }
        }
    }
}
