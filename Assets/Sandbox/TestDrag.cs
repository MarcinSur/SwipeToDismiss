using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITestDrag : IBeginDragHandler, IDragHandler, IEndDragHandler
{

}

public class TestDragCanvasItem : ITestDrag
{
    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}

public class TestDragItem : ITestDrag
{
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}

public interface IDragSwipe: IDrag, ISwipe
{

}

public interface ISwipe
{
    void SetupMaxSwipeDistance();
    void CheckIsSwipeEnabled();
    void Tick();
    void CompareByPixel();
    void CompareByNormalizedFloat();

    event EventHandler SwipeLeftEvent;
    event EventHandler SwipeRightEvent;
    event EventHandler SwipeUpEvent;
    event EventHandler SwipeDownEvent;
    event EventHandler SwipeCanceledEvent;
}

public interface IDrag
{
    event EventHandler<DragArgs> DragEvent;
}

public class SwipeArgs : EventArgs
{
    public float maxDistance { get; set; }
}
public class DragArgs : EventArgs
{
    public Vector2 drag { get; set; }
    public Vector2 offset { get; set; }
}