using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Animation : MonoBehaviour {

    public Vector3 moveToRight;
    public Vector3 moveToLeft;
    public Animator animator;

    private Vector3 dragPos;

    public void MoveToRightWithDownAnimation()
    {
        transform.DORotate(new Vector3(0, 0, -20), .8f);
        transform.DOMove(moveToRight, 1f).OnComplete(() => {  });
    }

    public void MoveToLeftWithDownAnimation()
    {
        transform.DORotate(new Vector3(0, 0, 20), .8f);
        transform.DOLocalMove(moveToLeft, 1f).OnComplete(() => { });
    }

    public void BackToCenter()
    {
        transform.DOLocalMove(new Vector3(0, 0, 0), .5f);
        transform.DOLocalRotate(new Vector3(0, 0, 0), .5f);
    }

    public void TopBarAnimation(Vector2 dragpos)
    {
        dragPos = dragpos;
        animator.SetFloat("BlendX", dragpos.x);
    }
}
