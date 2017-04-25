using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputFieldDemo : MonoBehaviour {

	private InputField m_input;
	private Text m_inputValue;

	// Use this for initialization
	void Start () {
		this.m_input = GameObject.Find ("InputField").GetComponent<InputField> ();
		this.m_inputValue = GameObject.Find ("InputValue").GetComponent<Text> ();
		//数值变化监听
		this.m_input.onValueChanged.AddListener (delegate(string input) {
			this.OnValueChanged(input);
		});
	}
	
	void OnValueChanged(string input)
	{
		this.m_inputValue.text = input;
	}
}
