using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButtom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool touched;
    private int pointerID;
    private bool canFire;

    private Vector2 smoothDirection;

    private void Awake()
    {
        touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            pointerID = eventData.pointerId;
            canFire = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            canFire = false;
            touched = false;
        }
    }

    public bool CanFire
    {
        get { return canFire; }
    }
}
