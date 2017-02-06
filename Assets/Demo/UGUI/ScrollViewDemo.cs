using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollViewDemo : MonoBehaviour {

	private ScrollRect m_scrollView;

	// Use this for initialization
	void Start () {
		GameObject scrollObj = GameObject.Find ("ScrollView");
        if (scrollObj == null)
        {
            return;
        }
		this.m_scrollView = scrollObj.GetComponent<ScrollRect> ();
		this.m_scrollView.onValueChanged.AddListener (delegate(Vector2 position) {
			
			//position 滑动条 方向比例
			Debug.Log(position.ToString());
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
