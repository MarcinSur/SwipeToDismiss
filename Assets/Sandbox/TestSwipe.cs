using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestSwipe : MonoBehaviour, IDragSwipe
{

    private float _maxDistanceWidth;
    private float _maxDistanceHeight;
    private float _normalizeValueX;
    private float _normalizeValueY;
    [SerializeField]
    private Vector2 _startPos;
    private Vector2 _mousePosition;

    public bool enableSwipe;
    public bool enableVerticalSwipe;
    public bool enableHorizontalSwipe;
    public float swipeDetectionLimitLeftRight;
    public float swipeDetectionLimitUpDown;
    public Vector2 offSet;
    public Vector2 offSetByPixel;

    event EventHandler OnSwipeLeft;
    event EventHandler OnSwipeRight;
    event EventHandler OnSwipeUp;
    event EventHandler OnSwipeDown;
    event EventHandler OnSwipeCancel;
    event EventHandler<DragArgs> OnDrag;
    #region ITestSwipe Event
    event EventHandler ISwipe.SwipeLeftEvent
    {
        add
        {
            OnSwipeLeft += value;
        }

        remove
        {
            OnSwipeLeft -= value;
        }
    }

    event EventHandler ISwipe.SwipeRightEvent
    {
        add
        {
            OnSwipeRight += value;
        }

        remove
        {
            OnSwipeRight -= value;
        }
    }

    event EventHandler ISwipe.SwipeUpEvent
    {
        add
        {
            OnSwipeUp += value;
        }

        remove
        {
            OnSwipeUp -= value;
        }
    }

    event EventHandler ISwipe.SwipeDownEvent
    {
        add
        {
            OnSwipeUp += value;
        }

        remove
        {
            OnSwipeUp -= value;
        }
    }

    event EventHandler ISwipe.SwipeCanceledEvent
    {
        add
        {
            OnSwipeCancel += value;
        }

        remove
        {
            OnSwipeCancel -= value;
        }
    }

    event EventHandler<DragArgs> IDrag.DragEvent
    {
        add
        {
            OnDrag += value;
        }

        remove
        {
            OnDrag -= value;
        }
    }
    #endregion

    #region ITestSwipe Method implementation
    public void SetupMaxSwipeDistance()
    {
        _maxDistanceWidth = Screen.width;
        _maxDistanceHeight = Screen.height;
    }

    public void Tick()
    {
        CheckIsSwipeEnabled();
        StartDrag();
        Drag();
        EndDrag();
    }

    public void CheckIsSwipeEnabled()
    {
        if (!enableSwipe)
        {
            return;
        }
    }

    public void CompareByPixel()
    {
        if (offSetByPixel.x > swipeDetectionLimitLeftRight && enableHorizontalSwipe)
        {
            OnSwipeRight?.Invoke(this, EventArgs.Empty);
            return;
        }
        if (offSetByPixel.x < -swipeDetectionLimitLeftRight && enableHorizontalSwipe)
        {
            OnSwipeLeft?.Invoke(this, EventArgs.Empty);
            return;
        }
        if (offSetByPixel.y > swipeDetectionLimitUpDown && enableVerticalSwipe)
        {
            OnSwipeUp?.Invoke(this, EventArgs.Empty);
            return;
        }
        if (offSetByPixel.y < -swipeDetectionLimitUpDown && enableVerticalSwipe)
        {
            OnSwipeDown?.Invoke(this, EventArgs.Empty);
            return;
        }
        OnSwipeCancel?.Invoke(this, EventArgs.Empty);
    }

    public void CompareByNormalizedFloat()
    {
        if (offSet.x > swipeDetectionLimitLeftRight && enableHorizontalSwipe)
        {
            OnSwipeRight?.Invoke(this, EventArgs.Empty);
            return;
        }
        if (offSet.x < swipeDetectionLimitLeftRight && enableHorizontalSwipe)
        {
            OnSwipeLeft?.Invoke(this, EventArgs.Empty);
            return;
        }
        if (offSet.y > swipeDetectionLimitUpDown && enableVerticalSwipe)
        {
            OnSwipeUp?.Invoke(this, EventArgs.Empty);
            return;
        }
        if (offSet.y < swipeDetectionLimitUpDown && enableVerticalSwipe)
        {
            OnSwipeDown?.Invoke(this, EventArgs.Empty);
            return;
        }
        OnSwipeCancel?.Invoke(this, EventArgs.Empty);

    }
    #endregion

    private void Start()
    {
        SetupMaxSwipeDistance();
    }

    private void Update()
    {
        Tick();
    }

    private void StartDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
        }
    }

    private void Drag()
    {
        DragArgs dragArgs = new DragArgs();
        if (Input.GetMouseButton(0))
        {
            _mousePosition = Input.mousePosition;
            offSetByPixel = _mousePosition - _startPos;
            offSet = _mousePosition / _startPos;
            _normalizeValueX = _mousePosition.x / _maxDistanceWidth;
            _normalizeValueY = _mousePosition.y / _maxDistanceHeight;

            dragArgs.drag = new Vector2(_normalizeValueX, _normalizeValueY);
            dragArgs.offset = offSet;
            OnDrag?.Invoke(this, dragArgs);
        }
    }

    private void EndDrag()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CompareByPixel();
        }
    }

}
