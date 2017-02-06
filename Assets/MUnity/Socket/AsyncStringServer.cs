using UnityEngine;
using System.Collections;
using System;
using System.Text;

namespace MUnity.Net
{
    public class AsyncStringServer : AsyncSocketServer<string>
    {

        protected override string Decode(byte[] bytes)
        {
            string data = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            Debug.Log(string.Format("收到客户端消息:{0}", data));
            return data;
        }

        protected override byte[] Encode(string obj)
        {
            string data = obj as string;
            return System.Text.Encoding.UTF8.GetBytes(data);
        }
    }
}

