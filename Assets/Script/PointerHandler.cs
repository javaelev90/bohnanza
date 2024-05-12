using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Action<PointerEventData> onEnter;
    private Action<PointerEventData> onLeftDown;
    private Action<PointerEventData> onScrollDown;
    private Action<PointerEventData> onRightDown;
    private Action<PointerEventData> onLeftUp;
    private Action<PointerEventData> onScrollUp;
    private Action<PointerEventData> onRightUp;
    private Action<PointerEventData> onExit;
    private Action<PointerEventData> onDrag;

    bool isInteractable = true;

    public void Initialize(Action<PointerEventData> onEnter = null,
        Action<PointerEventData> onLeftDown = null,
        Action<PointerEventData> onScrollDown = null,
        Action<PointerEventData> onRightDown = null,
        Action<PointerEventData> onLeftUp = null,
        Action<PointerEventData> onScrollUp = null,
        Action<PointerEventData> onRightUp = null,
        Action<PointerEventData> onExit = null,
        Action<PointerEventData> onDrag = null)
    {
        this.onEnter = onEnter;
        this.onLeftDown = onLeftDown;
        this.onScrollDown = onScrollDown;
        this.onRightDown = onRightDown;
        this.onLeftUp = onLeftUp;
        this.onScrollUp = onScrollUp;
        this.onRightUp = onRightUp;
        this.onExit = onExit;
        this.onDrag = onDrag;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onEnter?.Invoke(eventData);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInteractable)
            return;

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                onLeftDown?.Invoke(eventData);
                break;
            case PointerEventData.InputButton.Right:
                onRightDown?.Invoke(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                onScrollDown?.Invoke(eventData);
                break;
            default:
                break;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onExit?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                onLeftUp?.Invoke(eventData);
                break;
            case PointerEventData.InputButton.Right:
                onRightUp?.Invoke(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                onScrollUp?.Invoke(eventData);
                break;
            default:
                break;
        }
    }
}
