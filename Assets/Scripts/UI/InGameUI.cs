using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : BaseUI
{
    public Transform followTarget;
    public Vector2 followOffset;

    private void LateUpdate()
    {
        if (followTarget == null)
        {
            GameManager.UI.CloseInGameUI(this);
        }
        if (followTarget.IsValid())
        {
            transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + (Vector3)followOffset;
        }


    }

    public void SetTarget(Transform target)
    {
        this.followTarget = target;
    }

    public void SetOffset(Vector2 offset)
    {
        this.followOffset = offset;
        if(followTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + (Vector3)followOffset;
        }
    }

}
