using UnityEngine;
using System.Collections;
using System;
using MUnity.Net;
using System.Collections.Generic;

public class MarshalClientDemo : MonoBehaviour
{
    private AsyncMarshalServer m_marshalServer;
    private string mServerMsg;
    List<AsyncMarshalClient> m_clients;
    List<string> m_clientMsgs;

    void OnEnable()
    {
        mServerMsg = "";
        m_clientMsgs = new List<string>();
        m_clients = new List<AsyncMarshalClient>();
    }

    void OnDisable()
    {
        if(this.m_marshalServer != null) this.m_marshalServer.Dispose();
        this.m_marshalServer = null;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "启动服务器"))
        {
            this.m_marshalServer = new AsyncMarshalServer();
        }
        if (GUI.Button(new Rect(100, 0, 100, 50), "关闭服务器"))
        {
            if (this.m_marshalServer != null)
            {
                this.m_marshalServer.ShutDown();
                this.m_marshalServer.Dispose();
                this.m_marshalServer = null;
            }
        }
        if (GUI.Button(new Rect(200, 0, 100, 50), "发送消息"))
        {
            MarshalStruct msg = new MarshalStruct();
            msg.type = 12;
            msg.data = mServerMsg;
            if (this.m_marshalServer != null) this.m_marshalServer.PostAll(msg);
        }
        mServerMsg = GUI.TextField(new Rect(300, 0, 200, 50), mServerMsg);
        if (GUI.Button(new Rect(0, 50, 100, 50), "启动一个客户端"))
        {
            AsyncMarshalClient client = new AsyncMarshalClient();
            this.m_clients.Add(client);
            this.m_clientMsgs.Add("");
        }
        for (int i = 0; i < this.m_clients.Count; i++)
        {
            GUI.Label(new Rect(0, 50 * i + 100, 100, 50), "客户端" + i);
            if (GUI.Button(new Rect(100, 50 * i + 100, 100, 50), "关闭客户端"))
            {
                this.m_clients.RemoveAt(i);
                this.m_clientMsgs.RemoveAt(i);
                return;
            }
            if (GUI.Button(new Rect(200, 50 * i + 100, 100, 50), "发送消息"))
            {
                MarshalStruct msg = new MarshalStruct();
                msg.type = 12;
                msg.data = mServerMsg;
                this.m_clients[i].Post(msg);
            }
            this.m_clientMsgs[i] = GUI.TextField(new Rect(300, 50 * i + 100, 200, 50), this.m_clientMsgs[i]);
        }
    }

}
