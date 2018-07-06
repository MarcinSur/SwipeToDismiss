using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragTransform : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Color lerpedColor = Color.blue;
    [Tooltip("If 0 use screen width")]
    public float distanceToDismiss = 0;
    public DragState dragState;
    public UnityEvent onRightCompleted;
    public UnityEvent onLeftCompleted;
    public UnityEvent onMiddleCompleted;
    public UnityEvent onDrag;

    private Vector2 startDragPos;
    private Vector2 endDragPos;
    private Vector3 offSet;
    private float timeCount;
    private Vector2 deltaValue = Vector2.zero;
    private float screenWidth;

    public float deltaX
    {
        get { return deltaValue.x; }
    }

    void Start()
    {
        screenWidth = Camera.main.pixelWidth;
        dragState = DragState.NONE;
        //distanceToDismiss = (screenWidth - GetComponent<SpriteRenderer>().bounds.size.x) / 2;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        deltaValue = Vector2.zero;
        dragState = DragState.MIDDLE;

        startDragPos = eventData.position;
        offSet = transform.position - eventData.pointerCurrentRaycast.worldPosition;
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
        if (dragState == DragState.RIGHT)
        {
            onRightCompleted.Invoke();
        }
        else if (dragState == DragState.LEFT)
        {
            onLeftCompleted.Invoke();
        }
        else if (dragState == DragState.MIDDLE)
        {
            onMiddleCompleted.Invoke();
        }

        endDragPos = eventData.position;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        deltaValue = Vector2.zero;

        dragState = DragState.NONE;

    }

    float worldToViewportPoint;
    void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 rotateEulers;
        rotateEulers = new Vector3(0, 0, Mathf.RoundToInt(transform.rotation.z - (deltaValue.x/30)));
        if (rotateEulers.z >= 8)
        {
            rotateEulers.z = 8;
        }
        else if (rotateEulers.z <= -8)
        {
            rotateEulers.z = -8;
        }
        transform.position = eventData.pointerCurrentRaycast.worldPosition + offSet; 
        transform.eulerAngles = rotateEulers;

        //Debug.Log("SetDraggedPosition = " + deltaValue.x + " " + distanceToDismiss + " " + Camera.main.ViewportToWorldPoint(transform.position));
    }
    Vector3 viewPos;
    void CalculateSwipeDirection(float delta)
    {
        viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x > .6f)
        {
            dragState = DragState.RIGHT;
        print("target is on the right side! " + viewPos.x);

        }
        else if(viewPos.x < .4f)
        {
            dragState = DragState.LEFT;
        print("target is on the left side! " + viewPos.x);

        }
        else
        {
            dragState = DragState.MIDDLE;
        }
    }
}
