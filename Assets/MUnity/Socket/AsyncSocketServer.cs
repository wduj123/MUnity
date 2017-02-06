using UnityEngine;
using System.Collections;
using System;

namespace MUnity.Net
{
    public abstract class AsyncSocketServer<T> : ISocketServer ,IDisposable
    {
        SocketServer server;

        protected abstract T Decode(byte[] bytes);

        protected abstract byte[] Encode(T obj);

        public AsyncSocketServer()
        {
            if (server == null) server = new SocketServer(this);
        }

        public void PostAll(T obj)
        {
            byte[] bytes = Encode(obj);
            server.SendMessageAll(bytes);
        }

        public void Post(T obj)
        {
            byte[] bytes = Encode(obj);
        }

        public void ShutDown()
        {

        }

        public void Dispose()
        {
            
        }

        void ISocketServer.Receive(byte[] bytes)
        {
            object data = Decode(bytes);
        }

        
    }
}

