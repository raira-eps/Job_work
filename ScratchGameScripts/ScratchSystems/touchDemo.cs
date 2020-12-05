using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
public class touchDemo : MonoBehaviour
{
    public Camera uiCamera;
    [SerializeField]
    public TouchEvent OnTouch;

    RectTransform rectTransform;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, uiCamera, out Vector2 loaclPosInRect);
            OnTouch.Invoke((int)loaclPosInRect.x, (int)loaclPosInRect.y);
        }
    }

    [System.Serializable]
    public class TouchEvent : UnityEvent<int, int>{ }
}