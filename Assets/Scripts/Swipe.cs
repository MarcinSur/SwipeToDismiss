using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Swipe : MonoBehaviour
{
    public bool enableSwipe;
    public float swipeDetectionLimitLeftRight;
    public float swipeDetectionLimitUpDown;
    public UnityEvent swipedLeft;
    public UnityEvent swipedRight;
    public UnityEvent swipedUp;
    public UnityEvent swipedDown;
    public UnityEvent drag;
    public Vector2 offSet;
    public float maxDistance;
    public float normalizeValueX;

    [SerializeField]
    private Vector2 startPos;
    private Vector2 mousePosition;

    private void Start()
    {
        maxDistance = Screen.width;
    }

    void Update()
    {
        if (!enableSwipe)
            return;

#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            offSet = mousePosition - startPos;
            normalizeValueX = mousePosition.x / maxDistance;
            drag.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CalculateSwipeDirection();
        }

#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    offSet = touch.position - startPos;
                    normalizeValueX = touch.positon.x / maxDistance;
                    drag.Invoke();
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    CalculateSwipeDirection();
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
#endif
    }

    private void CalculateSwipeDirection()
    {
        float p = 1 - 100 / offSet.x;

        if (offSet.x > swipeDetectionLimitLeftRight)
        {
            swipedRight.Invoke();
        }
        if (offSet.x < -swipeDetectionLimitLeftRight)
        {
            swipedLeft.Invoke();
        }
        if (offSet.y > swipeDetectionLimitUpDown)
        {
            swipedUp.Invoke();
        }
        if (offSet.y < -swipeDetectionLimitUpDown)
        {
            swipedDown.Invoke();
        }

    }
}
