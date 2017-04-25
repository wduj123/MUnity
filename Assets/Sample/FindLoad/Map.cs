﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour,
IBeginDragHandler, ICancelHandler, IDeselectHandler, IDragHandler, IDropHandler, IEndDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, IMoveHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IScrollHandler, ISelectHandler, ISubmitHandler, IUpdateSelectedHandler
{

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag called.");
    }

    public void OnCancel(BaseEventData data)
    {
        Debug.Log("OnCancel called.");
    }

    public void OnDeselect(BaseEventData data)
    {
        Debug.Log("OnDeselect called.");
    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log("OnDrag called.");
    }

    public void OnDrop(PointerEventData data)
    {
        Debug.Log("OnDrop called.");
    }

    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log("OnEndDrag called.");
    }

    public void OnInitializePotentialDrag(PointerEventData data)
    {
        Debug.Log("OnInitializePotentialDrag called.");
    }

    public void OnMove(AxisEventData data)
    {
        Debug.Log("OnMove called.");
    }

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("OnPointerClick called.");
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown called.");
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("OnPointerEnter called.");
    }

    public void OnPointerExit(PointerEventData data)
    {
        Debug.Log("OnPointerExit called.");
    }

    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("OnPointerUp called.");
    }

    public void OnScroll(PointerEventData data)
    {
        Debug.Log("OnScroll called.");
    }

    public void OnSelect(BaseEventData data)
    {
        Debug.Log("OnSelect called.");
    }

    public void OnSubmit(BaseEventData data)
    {
        Debug.Log("OnSubmit called.");
    }

    public void OnUpdateSelected(BaseEventData data)
    {
        Debug.Log("OnUpdateSelected called.");
    }

}
