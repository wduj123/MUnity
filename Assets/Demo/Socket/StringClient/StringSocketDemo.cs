using UnityEngine;
using MUnity.Net;
using System.Collections;
using System;
using System.Collections.Generic;

public class StringSocketDemo : MonoBehaviour
{
    private AsyncStringServer m_server;
    private string mServerMsg;
    List<AsyncStringClient> m_clients;
    List<string> m_clientMsgs;

    void OnEnable()
    {
        mServerMsg = "";
        m_clientMsgs = new List<string>();
        m_clients = new List<AsyncStringClient>();
    }

    void OnDisable()
    {
        this.m_server.Dispose();
        this.m_server = null;
    }
    
    void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,100,50),"启动服务器"))
        { 
            this.m_server = new AsyncStringServer();
        }
        if (GUI.Button(new Rect(100, 0, 100, 50), "关闭服务器"))
        {
            if (this.m_server != null)
            {
                this.m_server.ShutDown();
                this.m_server.Dispose();
                this.m_server = null;
            }
        }
        if (GUI.Button(new Rect(200, 0, 100, 50), "发送消息"))
        {
            if (this.m_server != null) this.m_server.PostAll(mServerMsg);
        }
        mServerMsg = GUI.TextField(new Rect(300, 0, 200, 50), mServerMsg);
        if (GUI.Button(new Rect(0, 50, 100, 50), "启动一个客户端"))
        {
            AsyncStringClient client = new AsyncStringClient();
            this.m_clients.Add(client);
            this.m_clientMsgs.Add("");
        }
        for(int i = 0;i< this.m_clients.Count;i++)
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
                this.m_clients[i].Post(this.m_clientMsgs[i]);
            }
            this.m_clientMsgs[i] = GUI.TextField(new Rect(300, 50 * i + 100, 200, 50), this.m_clientMsgs[i]);
        }
    }
}
