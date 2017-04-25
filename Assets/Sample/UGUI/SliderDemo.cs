using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SliderDemo : MonoBehaviour {

	private Slider m_slider;
	private Text m_sliderValue;
	private Button m_direction;
	private int m_directionInt = 0;
//	private RectTransform transform;

	// Use this for initialization
	void Start () {

		//滑动条组件
		GameObject sliderObj = GameObject.Find ("Slider");
		if (sliderObj == null) {
			Debug.Log ("did not found Slider");
		} else {
			this.m_slider = sliderObj.GetComponent<Slider> ();
			//值改变监听
			if (this.m_slider != null) {
				this.m_slider.onValueChanged.AddListener (delegate {
					this.OnSliderValueChange();
				});
			}
		}

		//改变方向按钮
		GameObject directionObj = GameObject.Find ("Direction");
		if (directionObj == null) {
			Debug.Log ("did not found Direction");
		} else {
			this.m_direction = directionObj.GetComponent<Button> ();

			if (this.m_direction != null) {
				//改变方向 按钮监听
				this.m_direction.onClick.AddListener (delegate {
					if(m_directionInt > 3)
					{
						this.m_directionInt = 0;
					}
					switch(this.m_directionInt)
					{
					case 0:
						this.m_slider.SetDirection(Slider.Direction.LeftToRight,true);
						break;
					case 1:
						this.m_slider.SetDirection(Slider.Direction.TopToBottom,true);
						break;
					case 2:
						this.m_slider.SetDirection(Slider.Direction.RightToLeft,true);
						break;
					case 3:
						this.m_slider.SetDirection(Slider.Direction.BottomToTop,true);
						break;
					default:
						break;

					}
					m_directionInt++;
				});
			}
		}

		//滑动条显示Text
		GameObject sliderValueObj = GameObject.Find("SliderValue");
		if(sliderObj == null){
			Debug.Log ("did not found sliderValueObj");
		}else{
			this.m_sliderValue = sliderValueObj.GetComponent<Text> ();
		}

	}


	void OnSliderValueChange()
	{
		if (this.m_sliderValue != null) {
			this.m_sliderValue.text = Math.Round(this.m_slider.value*100,1)  +"%";
		}
	}
}
