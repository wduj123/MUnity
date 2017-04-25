using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using MUnity.Utility;

namespace MUnity.Net
{
    class SocketClient : IDisposable
    {
        Socket mClient;
        ISocketClient mSocketClient;
        byte[] buffer = new byte[1024];
        int tempLen;
        byte[] tempBuffer;

        public SocketClient() : this(NetUitility.GetIP(), 2000)
        {
            
        }

        public SocketClient(string ip, int port)
        {
            this.mClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                this.mClient.Connect(iPEndPoint);
                Debug.Log("Server Connected!");
                this.mClient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), mClient);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public SocketClient(ISocketClient client) : this(client,NetUitility.GetIP(), 2000)
        {

        }

        public SocketClient(ISocketClient client,string ip,int port)
        {
            this.mSocketClient = client;
            this.mClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                this.mClient.Connect(iPEndPoint);
                Debug.Log("Server Connected!");
                this.mClient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), mClient);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public void SetSocketClient(ISocketClient client)
        {
            this.mSocketClient = client;
        }

        public void SendMessage(byte[] bytes)
        {
            byte[] finalBytes = EncodeBytes(bytes);
            this.mClient.Send(finalBytes);
        }

        public void Dispose()
        {

        }

        void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var client = ar.AsyncState as Socket;
                var length = client.EndReceive(ar);
                DecodeBytes(buffer, length);
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), client);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }

        void DecodeBytes(byte[] buffer,int length)
        {
            //读取出来消息内容
            int index = 0;
            if (tempBuffer != null)
            {
                int len = tempBuffer.Length - tempLen;
                if (index + len <= length)
                {
                    Array.Copy(buffer, index, tempBuffer, tempLen, len);
                    index += tempBuffer.Length - tempLen;
                    if (mSocketClient != null) mSocketClient.Receive(tempBuffer);
                    tempBuffer = null;
                }
                else
                {
                    len = length;
                    Array.Copy(buffer, index, tempBuffer, tempLen, len);
                    index += len;
                    tempLen += len;
                }
            }
            while (index < length)
            {
                byte[] lenbytes = new byte[4];
                Array.Copy(buffer, index, lenbytes, 0, 4);
                int len = System.BitConverter.ToInt32(lenbytes, 0);
                index += 4;
                //显示消息
                byte[] bytes = new byte[len];
                if (index + len <= length)
                {
                    Array.Copy(buffer, index, bytes, 0, len);
                }
                else
                {
                    tempLen = length - index;
                    Array.Copy(buffer, index, bytes, 0, tempLen);
                    tempBuffer = bytes;
                    break;
                }
                if (mSocketClient != null) mSocketClient.Receive(bytes);
                index += len;
            }
        }

        byte[] EncodeBytes(byte[] bytes)
        {
            int length = bytes.Length;
            byte[] lenBytes = System.BitConverter.GetBytes(length);
            byte[] finalBytes = new byte[bytes.Length + lenBytes.Length];
            Array.Copy(lenBytes, 0, finalBytes, 0, lenBytes.Length);
            Array.Copy(lenBytes, 0, finalBytes, 0, lenBytes.Length);
            lenBytes.CopyTo(finalBytes, 0);
            bytes.CopyTo(finalBytes, lenBytes.Length);
            return finalBytes;
        }

        public void Shutdown()
        {
            this.mClient.Shutdown(SocketShutdown.Both);
            this.mClient.Close();
        }
    }
}



