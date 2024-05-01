using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomSlider : Slider, IPointerDownHandler, IPointerUpHandler
{
    public bool IsFocused { get; private set; }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        IsFocused = true;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        IsFocused = false;
    }


   // public bool IsFocused { get; private set; }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        IsFocused = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        IsFocused = false;
    }
}
