using UnityEngine;

public interface ISwipeHandler
{
    void OnSwipeRight();
    void OnSwipeLeft();
    void OnDrag(Vector2 getOffset);
}