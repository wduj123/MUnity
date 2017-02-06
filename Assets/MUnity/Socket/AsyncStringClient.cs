using UnityEngine;
using System.Collections;
using System.Text;

namespace MUnity.Net
{
    public class AsyncStringClient : AsyncSocketClient<string>
    {
        protected override string Decode(byte[] bytes)
        {
            string data = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            Debug.Log(string.Format("收到服务器消息:{0}", data));
            return data;
        }

        protected override byte[] Encode(string obj)
        {
            string data = obj as string;
            return System.Text.Encoding.UTF8.GetBytes(data);
        }
    }
}

