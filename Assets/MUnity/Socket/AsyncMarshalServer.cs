using UnityEngine;
using System.Collections;
using System;

namespace MUnity.Net
{
    public class AsyncMarshalServer : AsyncSocketServer<MarshalStruct>
    {
        protected override MarshalStruct Decode(byte[] bytes)
        {
            object data = MarshalBuffer.ByteToStructData(bytes, typeof(MarshalStruct));
            MarshalStruct msg = (MarshalStruct)data;
            Debug.Log(string.Format("接收到客户端消息type{0},data{1}", msg.type, msg.data));
            return msg;
        }

        protected override byte[] Encode(MarshalStruct obj)
        {
            byte[] bytes = MarshalBuffer.StructToByteData(obj);
            return bytes;
        }
    }
}

