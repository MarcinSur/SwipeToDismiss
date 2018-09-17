using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Swipe : MonoBehaviour
{

    public float swipeDetectionLimitLR;
    public float swipeDetectionLimitUD;
    public UnityEvent swipeLeft;
    public UnityEvent swipeRight;
    public UnityEvent swipeUp;
    public UnityEvent swipeDown;

    public Vector2 startPos;
    public Vector2 offSet;

    private Vector2 mousePosition;

    void Start()
    {

    }

    void Update()
    {

#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            offSet = mousePosition - startPos;
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

    public Vector2 GetOffset()
    {
        return offSet;
    }

    private void CalculateSwipeDirection()
    {
        if (offSet.x > swipeDetectionLimitLR)
        {
            swipeRight.Invoke();
            Debug.Log("Swipe RIGHT");
        }
        if (offSet.x < -swipeDetectionLimitLR)
        {
            swipeLeft.Invoke();
            Debug.Log("Swipe LEFT");
        }
        if (offSet.y > swipeDetectionLimitUD)
        {
            swipeUp.Invoke();
            Debug.Log("Swipe UP");
        }
        if (offSet.y < -swipeDetectionLimitUD)
        {
            swipeDown.Invoke();
            Debug.Log("Swipe DOWN");
        }

    }
}
