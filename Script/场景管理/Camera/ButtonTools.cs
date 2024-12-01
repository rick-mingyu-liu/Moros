using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine;

/// <summary>
/// ÖØÐ´ Button °´Å¥
/// 1¡¢µ¥»÷×ó¼ü
/// 2¡¢µ¥»÷ÓÒ¼ü
/// </summary>    
public class ButtonTools : Selectable, IPointerClickHandler, ISubmitHandler
{
    [Serializable]
    public class ButtonClickedEvent : UnityEvent { }

    [FormerlySerializedAs("onClick")]
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    protected ButtonTools()
    { }


    public ButtonClickedEvent onClick
    {
        get { return m_OnClick; }
        set { m_OnClick = value; }
    }

    private void Press()
    {
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("Button.onClick", this);
        m_OnClick.Invoke();
    }


    [FormerlySerializedAs("onRightPress")]
    [SerializeField]
    private ButtonClickedEvent m_onRightPress = new ButtonClickedEvent();
    public ButtonClickedEvent onRightPress
    {
        get { return m_onRightPress; }
        set { m_onRightPress = value; }
    }



    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1 && (eventData.button.Equals(PointerEventData.InputButton.Left)))
        {
            onClick.Invoke();
        }
        else if (eventData.clickCount == 1 && (eventData.button.Equals(PointerEventData.InputButton.Right)))
        {
            m_onRightPress.Invoke();
        }
    }



    public virtual void OnSubmit(BaseEventData eventData)
    {
        Press();
        if (!IsActive() || !IsInteractable())
            return;

        DoStateTransition(SelectionState.Pressed, false);
        StartCoroutine(OnFinishSubmit());
    }

    private IEnumerator OnFinishSubmit()
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        DoStateTransition(currentSelectionState, false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }
}