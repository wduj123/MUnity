using UnityEngine;
using System.Collections;
using System;
using ProtoData;
using System.IO;

namespace MUnity.Net
{
    public class AsyncProtoServer : AsyncSocketServer<MessageData>
    {
        protected override MessageData Decode(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes, 0, bytes.Length))
            {
                MessageData data = ProtoBuf.Serializer.Deserialize<MessageData>(stream);
                Debug.Log(string.Format("接收到消息:data.cmd:{0}", data.cmd));
                return data;
            }
        }

        protected override byte[] Encode(MessageData obj)
        {
            MemoryStream stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize<MessageData>(stream, obj);
            byte[] bytes = new byte[stream.Length];
            Array.Copy(stream.GetBuffer(), bytes, stream.Length);
            return bytes;
        }
    }
}

