using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using MUnity.Utility;

public class TimeDemo : MonoBehaviour {

    private int m_updateCount = 0;
    private int m_fixedUpdateCount = 0;
    private int m_lateUpdateCount = 0;

    private List<GUIAcition> mGUIActions;
    private string mTimeScale = "1";

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        this.mGUIActions = new List<GUIAcition>(new GUIAcition[] {
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("DateTime.Now.Ticks : {0}", DateTime.Now.Ticks));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("Time.frameCount : {0}", Time.frameCount));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("Time.frame : {0}", Time.frameCount/this.m_updateCount));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("Time.realtimeSinceStartup : {0}", Time.realtimeSinceStartup));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("Time.timeScale : {0}", Time.timeScale));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("Time.time : {0}", Time.time));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("Time.timeSinceLevelLoad : {0}", Time.timeSinceLevelLoad));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("UpdateCount : {0}", this.m_updateCount));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("FixedUpdateCount : {0}", this.m_fixedUpdateCount));
            },
            (top,height) => {
                GUI.Label(new Rect(0, top, 500, height), string.Format("LateUpdateCount : {0}", this.m_lateUpdateCount));
            },
            (top,height) => {
                mTimeScale = GUI.TextField(new Rect(0, top, 100, height), mTimeScale);
            },
            (top,height) => {
                if (GUI.Button(new Rect(0, top, 100, height), "改变缩放值"))
                {
                    try
                    {
                        int scale = int.Parse(mTimeScale);
                        if(Time.timeScale != scale) Time.timeScale = scale;
                    }
                    catch
                    {
                        Debug.Log("请输入一个整数！");
                    }
                }
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
        this.m_updateCount++;
	}

    void FixedUpdate()
    {
        this.m_fixedUpdateCount++;
    }

    void LateUpdate()
    {
        this.m_lateUpdateCount++;
    }

    

    void OnGUI()
    {
        GUI.color = Color.red;
        MGUIUtility.ShowList(mGUIActions);
    }
}
