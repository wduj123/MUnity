using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonDemo : MonoBehaviour
{
	private Button m_button;
	private string[] m_btnNames = new string[] {
		"WorldMap","Email"
	};

	void Start ()
    {
		// 多个button组合设置
		foreach (string str in this.m_btnNames) {
			GameObject objBtn = GameObject.Find (str);
			Button btn = objBtn.GetComponent<Button> ();

			btn.onClick.AddListener (delegate {
				this.OnClick(objBtn);	
			});
		}

		// 单个button点击设置
		this.m_button = GameObject.Find("Shop").GetComponent<Button>();
		this.m_button.onClick.AddListener(delegate {
			Debug.Log("this is a test");
		});
	}

	//多个button点击事件
	void OnClick(GameObject sender)
	{
		string btnName = sender.name;
		switch (btnName) {
		case "WorldMap":
			Debug.Log (btnName);
			break;
		case "Email":
			Debug.Log (btnName);
			break;
		default:
			break;
		}
	}
}
