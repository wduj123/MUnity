using UnityEngine;
using System.Collections;
using System;
using MUnity.Os;
using System.Threading;
using DG.Tweening;

public class MessageTest : MonoBehaviour
{
    private Handler handler;

    void Start () {
        handler = new Handler((msg) => {
            switch (msg.what)
            {
                case 0:
                    Debug.Log("msg:0");
                    break;
                case 1:
                    Debug.Log("msg:1");
                    break;
                default:
                    Debug.Log("default");
                    break;
            }
        });
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,100,50),"显示Text"))
        {
            new Thread(() => {
                Message msg = new Message();
                msg.what = 2;
                handler.SendMessage(msg);
                Thread.Sleep(2000);
                Message msg1 = new Message();
                msg1.what = 1;
                handler.SendMessage(msg1);
            }).Start();
        }
        if (GUI.Button(new Rect(100, 0, 100, 50), "显示Text"))
        {
            new Thread(() => {
                handler.PostDelayed(() =>
                {
                    Debug.Log("success2!");
                    transform.DOLocalMoveX(10, 2);
                }, 2000);
            }).Start();
            Debug.Log("success1!");
        }
    }

}
