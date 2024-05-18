using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PointerHandler))]
public class CardV2 : MonoBehaviour
{
    PointerHandler handler;
    SpriteRenderer spriteRenderer;
    CardData card;

    //Events
    CardPressedEvent cardPressedEvent;
    CardReleasedEvent cardReleasedEvent;
    CardInteractionEvent cardInteractionEvent;

    public bool IsCardInteracting { get; private set; }
    public bool IsCardBeingDragged { get; private set; }
    public bool IsCardMoved { get; set; }

    private Coroutine transferCoRoutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        handler = GetComponent<PointerHandler>();
        cardPressedEvent = new();
        cardReleasedEvent = new();
        handler.Initialize(onLeftDown: OnPressed, onLeftUp: OnReleased, onDrag: OnDragged);

        cardInteractionEvent = new() { Card = this };
    }

    public void Initialize(CardData card)
    {
        this.card = card;
    }

    public CardData GetCardData() { return card; }

    public void SetPosition(Vector3 newPosition)
    {
        transform.SetLocalPositionAndRotation(newPosition, transform.rotation);
    }
    private void SetPosition(Vector2 newPosition)
    {
        transform.SetLocalPositionAndRotation(newPosition, transform.rotation);
    }

    public void Transfer(Vector2 targetPosition, float transferTime)
    {
        StopTransfer();
        IsCardMoved = true;
        transferCoRoutine = StartCoroutine(TransferCard(targetPosition, transferTime));
    }

    private IEnumerator TransferCard(Vector2 targetPosition, float transferTime)
    {
        float accumulatedTime = 0f;
        while(Vector2.Distance(targetPosition, transform.position) >= 0.1)
        {
            Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, accumulatedTime / transferTime);
            SetPosition(newPosition);
            accumulatedTime += Time.deltaTime;
            yield return null;
        }
        SetPosition(targetPosition);
        IsCardMoved = false;
    }

    private void StopTransfer()
    {
        if (transferCoRoutine != null)
            StopCoroutine(transferCoRoutine);
    }
    
    public bool IsAtLocation(Vector2 location)
    {
        return transform.position.Equals(location);
    }

    public void SetRenderingOrderLevel(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    public void OnPressed(PointerEventData eventData)
    {
        StopTransfer();
        IsCardBeingDragged = true;
        IsCardMoved = true;
    }

    public void OnDragged(PointerEventData eventData)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        SetPosition(new Vector2(worldPosition.x, worldPosition.y));   
    }

    public void OnReleased(PointerEventData eventData)
    {
        IsCardBeingDragged = false;
        IsCardMoved = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsInteractionLayer(collision.gameObject)) return;
        if (IsCardBeingDragged) return;

        EventSystem.Instance.FireEvent(cardInteractionEvent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInteractionLayer(collision.gameObject)) return;
        IsCardInteracting = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsInteractionLayer(collision.gameObject)) return;
        IsCardInteracting = false;
    }

    bool IsInteractionLayer(GameObject gameObject)
    {
        return gameObject.layer == LayerMask.NameToLayer("CardInteraction");
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        CardV2 objAsCard = obj as CardV2;
        if (objAsCard == null) return false;
        else return objAsCard.GetHashCode() == GetHashCode();
    }
    public override int GetHashCode()
    {
        return gameObject.GetInstanceID();
    }
}
