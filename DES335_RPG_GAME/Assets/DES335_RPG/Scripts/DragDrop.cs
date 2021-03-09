using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler//, IDropHandler
{
    private Canvas canvas;

    private RectTransform rectTrans;
    private CanvasGroup canvasGroup;

    [HideInInspector]
    public Vector3 startPos;

    private void Awake()
    {
        if(canvas == null)
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("On Begin Drag");

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;   
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Drag");

        // Movement delta - Mouse move since previous frame
        //rectTrans.anchoredPosition += eventData.delta;
        rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("On End Drag");
        
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    // Called when the mouse is pressed when on top of the object
    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = rectTrans.localPosition;

        //Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (rectTrans.localPosition == startPos)
            GetComponentInParent<InventorySlot>().UseItem();

        //Debug.Log("OnPointerUp");
    }

    //public void OnDrop(PointerEventData eventData)
    //{
    //    throw new System.NotImplementedException();
    //}
}
