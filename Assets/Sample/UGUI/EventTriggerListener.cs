using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class EventTriggerListener : EventTrigger {

	public delegate void VoidDelegate(GameObject obj);

	public VoidDelegate OnClick;
	public VoidDelegate OnMouseDown;
	public VoidDelegate OnMouseUp;
	public VoidDelegate OnMouseMove;
	public VoidDelegate OnMouseScroll;

	public static EventTriggerListener GetListener(GameObject obj)
	{
		EventTriggerListener listener = obj.GetComponent<EventTriggerListener> ();
		if (listener == null) {
			listener = obj.AddComponent<EventTriggerListener> ();
		}
		return listener;    
	}

	public override void OnPointerDown(PointerEventData data)
	{
		if(OnMouseDown != null) OnMouseDown (gameObject);
	}

	public override void OnPointerClick(PointerEventData data)
	{
		if (OnClick != null) OnClick (gameObject);
	}

	public override void OnPointerUp(PointerEventData data)
	{
		if (OnMouseUp != null) OnMouseUp (gameObject);
	}


	public override void OnMove(AxisEventData data)
	{
		if (OnMouseMove != null) OnMouseMove (gameObject);
	}


	public override void OnScroll(PointerEventData data)
	{
		if (OnMouseScroll != null) OnMouseScroll (gameObject);
	}
}
