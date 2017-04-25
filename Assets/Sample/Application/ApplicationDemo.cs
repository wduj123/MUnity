using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using MUnity.Utility;

public class ApplicationDemo : MonoBehaviour
{
    private List<GUIAcition> mActions;
    void Start()
    {
        Matrix4x4 M =  GUI.matrix;

        this.mActions = new List<GUIAcition>(new GUIAcition[] {
            (top,height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.absoluteURL : {0}", Application.absoluteURL));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.cloudProjectId : {0}", Application.cloudProjectId));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.dataPath : {0}", Application.dataPath));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.persistentDataPath : {0}", Application.persistentDataPath));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.dataPath : {0}", Application.dataPath));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.productName : {0}", Application.productName));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.streamedBytes : {0}", Application.streamedBytes));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.streamingAssetsPath : {0}", Application.streamingAssetsPath));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.temporaryCachePath : {0}", Application.temporaryCachePath));
            },
            (top, height) => {
                GUI.Label(new Rect(0, top, 1000, height), string.Format("Application.platform : {0}", Application.platform));
            }
        });
    }
    void OnGUI()
    {
        GUI.color = Color.red;
        MGUIUtility.ShowList(mActions);
    }
}
