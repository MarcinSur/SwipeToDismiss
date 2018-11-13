using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragTransform : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform target;
    public DragState dragState;
    public UnityEvent onRightCompleted;
    public UnityEvent onLeftCompleted;
    public UnityEvent onMiddleCompleted;
    public DragEvent onDrag;

    private Vector2 startDragPos;
    private Vector2 endDragPos;
    private Vector3 offSet;
    private Vector2 deltaValue = Vector2.zero;

    public float deltaX
    {
        get { return deltaValue.x; }
    }

    void Start()
    {
        dragState = DragState.NONE;

        if(target == null)
        {
            target = transform;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBegin");
        deltaValue = Vector2.zero;
        dragState = DragState.MIDDLE;

        startDragPos = eventData.position;
        offSet = target.position - eventData.pointerCurrentRaycast.worldPosition;
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.transform != target)
        {
            OnEndDrag(eventData);
            return;
        }
        deltaValue += eventData.delta;
        CalculateSwipeDirection(deltaValue.x);
        SetDraggedPosition(eventData);
        onDrag.Invoke(viewPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragState == DragState.RIGHT)
        {
            onRightCompleted.Invoke();
            return;
        }
        else if (dragState == DragState.LEFT)
        {
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

    float worldToViewportPoint;
    void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 rotateEulers;
        rotateEulers = new Vector3(0, 0, Mathf.RoundToInt(target.rotation.z - deltaX/100));
        if (rotateEulers.z >= 8)
        {
            rotateEulers.z = 8;
        }
        else if (rotateEulers.z <= -8)
        {
            rotateEulers.z = -8;
        }
        target.position = eventData.pointerCurrentRaycast.worldPosition + offSet; 
        target.eulerAngles = rotateEulers;
    }

    Vector3 viewPos;
    void CalculateSwipeDirection(float delta)
    {
        viewPos = Camera.main.WorldToViewportPoint(target.position);
        if (viewPos.x > .6f)
        {
            dragState = DragState.RIGHT;
        }
        else if(viewPos.x < .4f)
        {
            dragState = DragState.LEFT;
        }
        else
        {
            dragState = DragState.MIDDLE;
        }
    }
}
