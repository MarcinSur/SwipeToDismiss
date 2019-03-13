using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Swipe : MonoBehaviour
{

    private float _maxDistanceWidth;
    private float _maxDistanceHeight;
    private float _normalizeValueX;
    private float _normalizeValueY;
    [SerializeField]
    private Vector2 _startPos;
    private Vector2 _mousePosition;

    public bool enableSwipe;
    public float swipeDetectionLimitLeftRight;
    public float swipeDetectionLimitUpDown;
    public Vector2 offSet;

    public delegate void SwipeLeft();
    public static event SwipeLeft OnSwipeLeft;

    //public UnityEvent swipedLeft;
    public UnityEvent swipedRight;
    public UnityEvent swipedUp;
    public UnityEvent swipedDown;
    public UnityEvent drag;

    private void Start()
    {
        _maxDistanceWidth = Screen.width;
        _maxDistanceHeight = Screen.height;
    }

    void Update()
    {
        if (!enableSwipe)
        {
            return;
        }

#if UNITY_STANDALONE || UNITY_EDITOR

        StartDrag();
        Drag();
        EndDrag();

#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    offSet = touch.position - _startPos;
                    _normalizeValueX = touch.position.x / _maxDistanceWidth;
                    _normalizeValueY = touch.position.y / _maxDistanceHeight;
                    drag.Invoke();
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    CompareSwipeDirectionUsingPixel();
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
#endif
    }

    private void EndDrag()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CompareSwipeDirectionUsingPixel();
        }
    }

    private void Drag()
    {
        if (Input.GetMouseButton(0))
        {
            _mousePosition = Input.mousePosition;
            offSet = _mousePosition - _startPos;
            _normalizeValueX = _mousePosition.x / _maxDistanceWidth;
            _normalizeValueY = _mousePosition.y / _maxDistanceHeight;
            drag.Invoke();
        }
    }

    private void StartDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
        }
    }

    private void CompareNormalizedFloatToSwipeDirection()
    {
        if (_normalizeValueX > swipeDetectionLimitLeftRight)
        {
            swipedRight.Invoke();
        }
        if (_normalizeValueX < -swipeDetectionLimitLeftRight)
        {
            //swipedLeft.Invoke();
            OnSwipeLeft?.Invoke();
        }
        if (_normalizeValueY > swipeDetectionLimitUpDown)
        {
            swipedUp.Invoke();
        }
        if (_normalizeValueY < -swipeDetectionLimitUpDown)
        {
            swipedDown.Invoke();
        }
    }

    private void CompareSwipeDirectionUsingPixel()
    {
        if (offSet.x > swipeDetectionLimitLeftRight)
        {
            swipedRight.Invoke();
        }
        if (offSet.x < -swipeDetectionLimitLeftRight)
        {
            //swipedLeft.Invoke();
            OnSwipeLeft?.Invoke();
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
