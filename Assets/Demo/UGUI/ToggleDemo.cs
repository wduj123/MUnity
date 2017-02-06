using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleDemo : MonoBehaviour {

	private Toggle m_toggle;

	private ToggleGroup m_toggleGroup;

	private string[] m_toggleNames = new string[]
	{
		"Toggle1","Toggle2","Toggle3"
	};

	private string[] m_BtnNames = new string[] {
		"Button1","Button2","Button3"
	};

	// Use this for initialization
	void Start () {

		//单个toggle选中与否监听
		GameObject togObj = GameObject.Find ("Toggle0");

		if (togObj == null) {
			Debug.Log ("do not find the toggle0");
		} else {
			this.m_toggle = togObj.GetComponent<Toggle> ();
			this.m_toggle.onValueChanged.AddListener (delegate(bool selected) {

				if(selected)
				{
					Debug.Log("this toggle is selected");
				}
				else
				{
					Debug.Log("toggle is unSelected");
				}

			});
		}
			
		//多个toggle监听设置
		foreach (string toggleName in this.m_toggleNames) {
			GameObject toggleObj = GameObject.Find (toggleName);
			if (toggleObj == null) {
				Debug.Log ("did not found the " + toggleName);
			} else {
				Toggle mToggle = toggleObj.GetComponent<Toggle> ();
				mToggle.onValueChanged.AddListener (delegate(bool selected) {
					this.OnValueChanged(toggleObj,selected);
				});
			}

		}

		//toggle group操作
		GameObject groupObj = GameObject.Find("ToggleGroup");
		if (groupObj == null) {
			groupObj = new GameObject("ToggleGroup");
		}

		groupObj.AddComponent<ToggleGroup> ();
		this.m_toggleGroup = groupObj.GetComponent<ToggleGroup> ();

		for(int i=0;i<this.m_toggleNames.Length;i++)
		{
			GameObject toggleObj = GameObject.Find (this.m_toggleNames[i]);
			if (toggleObj == null) {
				Debug.Log ("did not found the " + this.m_toggleNames[i]);
			} else {
				Toggle mToggle = toggleObj.GetComponent<Toggle> ();
				this.m_toggleGroup.RegisterToggle (mToggle);
			}

		}

		//button 点击操作组
		foreach (string btnName in m_BtnNames) {
			GameObject btnObj = GameObject.Find (btnName);
			if (btnObj == null) {
				Debug.Log ("the button" + btnName + "did not found");
				continue;
			}
			Button btn = btnObj.GetComponent<Button> ();
			btn.onClick.AddListener(delegate {
				this.OnClick(btnObj);
			});
		}


	
	}

	//多个toggle值改变事件
	void OnValueChanged(GameObject obj,bool selectedValue)
	{
		string name = obj.name;
		switch (name) {
		case "Toggle1":
			Debug.Log ("toggle1 value changed");
			break;
		case "Toggle2":
			Debug.Log ("toggle2 value changed");
			break;
		case "Toggle3":
			Debug.Log ("toggle3 value changed");
			break;
		default:
			break;
		}
	}

	//button 点击操作组
	void OnClick(GameObject btnObj)
	{
		string name = btnObj.name;
		switch (name) {
		case "Button1":
			this.m_toggleGroup.SetAllTogglesOff ();
			break;
		case "Button2":
			foreach (Toggle tog in this.m_toggleGroup.ActiveToggles()) {
				tog.isOn = true;
			}

			break;
		case "Button3":
			break;
		default:
			break;
		}
	}

}
