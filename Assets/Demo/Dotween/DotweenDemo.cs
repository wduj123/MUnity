using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DotweenDemo : MonoBehaviour {

	private RectTransform m_buttonRectTrans;
	public Ease ease;
	private static List<Action> m_updater = new List<Action>();

    public PathType type;
    public PathMode mode;

    void Awake () 
	{
		m_buttonRectTrans = GetComponent<RectTransform> ();
	}

	IEnumerator DoSizeDeltaIE()
	{
		yield return m_buttonRectTrans.DOSizeDelta(new Vector2(900,900),1).WaitForCompletion();
		yield return new WaitForSeconds (2);
		yield return m_buttonRectTrans.DOSizeDelta(new Vector2(900,900),1).WaitForCompletion();
		Debug.Log ("finish");
	}

	private void CallBack(int x,int y = 10)
	{
		Debug.Log ("finish");
	}
	
	void Update ()
    {
		for(int i = 0; i < m_updater.Count; i ++)
		{
			m_updater [i] ();
		}
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Move"))
        {
            DoMove();
        }
        else if (GUI.Button(new Rect(0, 100, 100, 100), "LocalMove"))
        {
            DoLocalMove();
        }
    }

    private void DoMove()
    {
        CallBack(10);
        Tweener tweener = transform.DOMove(new Vector3(10, 0, 0), 1)
            .SetEase(this.ease)
            .SetDelay(1)
            .SetRecyclable()
            .OnComplete(() =>
            {
                Debug.Log("finish");
            }).OnPause(() =>
            {
                Debug.Log("OnPause");
            });
        List<Vector3> path = new List<Vector3>();
        path.Add(Vector3.zero);
        path.Add(Vector3.one);
        path.Add(Vector3.one * 3);
        transform.DOPath(path.ToArray(), 1, type, mode, 10);
    }

    private void DoLocalMove()
    {
        transform.DOLocalMove(new Vector3(10, 0, 0), 1);
    }
}
