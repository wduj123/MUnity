using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MUnity.Net;
using ProtoData;
using System.Text;

public class ProtoClientDemo : MonoBehaviour {

    private AsyncProtoServer m_marshalServer;
    private string mServerMsg;
    List<AsyncProtoClient> m_clients;
    List<string> m_clientMsgs;

    void OnEnable()
    {
        mServerMsg = "";
        m_clientMsgs = new List<string>();
        m_clients = new List<AsyncProtoClient>();
    }

    void OnDisable()
    {
        if (this.m_marshalServer != null) this.m_marshalServer.Dispose();
        this.m_marshalServer = null;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "启动服务器"))
        {
            this.m_marshalServer = new AsyncProtoServer();
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
            MessageData data = new MessageData();
            data.cmd = 100;
            data.data = Encoding.UTF8.GetBytes(this.mServerMsg);
            MessageHead head = new MessageHead();
            head.sequence = 5;
            head.stamp = 4;
            head.version = 1;
            data.head = head;
            if (this.m_marshalServer != null) this.m_marshalServer.PostAll(data);
        }
        mServerMsg = GUI.TextField(new Rect(300, 0, 200, 50), mServerMsg);
        if (GUI.Button(new Rect(0, 50, 100, 50), "启动一个客户端"))
        {
            AsyncProtoClient client = new AsyncProtoClient();
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
                MessageData data = new MessageData();
                data.cmd = 100;
                data.data = Encoding.UTF8.GetBytes(this.m_clientMsgs[i]);
                MessageHead head = new MessageHead();
                head.sequence = 5;
                head.stamp = 4;
                head.version = 1;
                data.head = head;
                this.m_clients[i].Post(data);
            }
            this.m_clientMsgs[i] = GUI.TextField(new Rect(300, 50 * i + 100, 200, 50), this.m_clientMsgs[i]);
        }
    }
}
