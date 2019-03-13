using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public enum DragState
{
    NONE,
    MIDDLE,
    LEFT,
    RIGHT
}

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransformToDrag;
    public DragState dragState;
    public UnityEvent onRightCompleted;
    public UnityEvent onLeftCompleted;
    public UnityEvent onMiddleCompleted;
    public UnityEvent onDrag;

    private Vector2 startDragPos;
    private Vector2 endDragPos;
    private Vector3 offSet;
    private Vector2 deltaValue = Vector2.zero;
    public static float screenWidth = Screen.width;
    public static float screenHeight = Screen.height;


    public float deltaX
    {
        get { return deltaValue.x; }
    }

    public void Init(RectTransform rectTransform)
    {
        this.rectTransformToDrag = rectTransform;
        dragState = DragState.NONE;
        onMiddleCompleted.AddListener(rectTransform.GetComponent<Animation>().BackToCenter);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        deltaValue = Vector2.zero;
        dragState = DragState.MIDDLE;

        startDragPos = eventData.position;
        offSet = new Vector2(rectTransformToDrag.position.x, rectTransformToDrag.position.y) - eventData.pressPosition;
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        deltaValue += eventData.delta;
        CalculateSwipeDirection(deltaValue.x);
        SetDraggedPosition(eventData);
        onDrag.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CalculateSwipeDirection(deltaValue.x);

        if (dragState == DragState.RIGHT)
        {
            //MoveToRightWithDownAnimation();
            onRightCompleted.Invoke();
            return;
        }
        else if (dragState == DragState.LEFT)
        {
            //MoveToLeftWithDownAnimation();
            onLeftCompleted.Invoke();
            return;
        }
        else if (dragState == DragState.MIDDLE)
        {
            onMiddleCompleted.Invoke();
        }

        endDragPos = eventData.position;

        deltaValue = Vector2.zero;

        dragState = DragState.NONE;
    }

    void SetDraggedPosition(PointerEventData eventData)
    {
        RotateAndDrag(eventData);
    }

    void CalculateSwipeDirection(float delta)
    {
        Vector3 screenToViewportPoint;
        screenToViewportPoint = Camera.main.ScreenToViewportPoint(rectTransformToDrag.position);

        if (screenToViewportPoint.x > .6f)
        {
            dragState = DragState.RIGHT;
            return;
        }
        if (screenToViewportPoint.x < .4f)
        {
            dragState = DragState.LEFT;
            return;
        }

        dragState = DragState.MIDDLE;

    }

    void RotateAndDrag(PointerEventData eventData)
    {
        Vector3 globalMousePosition;
        Vector3 rotateEulers;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransformToDrag, eventData.position, eventData.pressEventCamera, out globalMousePosition))
        {
            rotateEulers = new Vector3(0, 0, Mathf.RoundToInt(rectTransformToDrag.rotation.z - (deltaValue.x / 30)));

            if (rotateEulers.z >= 8)
            {
                rotateEulers.z = 8;
            }
            else if (rotateEulers.z <= -8)
            {
                rotateEulers.z = -8;
            }

            rectTransformToDrag.position = globalMousePosition + offSet;
            rectTransformToDrag.eulerAngles = rotateEulers;
        }
    }

}
