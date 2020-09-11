using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ToggleSwitch : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    public static bool _isOn = true;
    public bool isOn
    {
        get
        {
            return _isOn;
        }
    }
    [SerializeField]
    private RectTransform toggleIndicator;
    [SerializeField]
    private Image bg;

    [SerializeField]
    private Color onColor;
    [SerializeField]
    private Color offColor;
    private float offX;
    private float onX;
    [SerializeField]
    private float tweenTime = 0.25f;

    public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;
    // Start is called before the first frame update
    void Start()
    {
        ToggleColor(isOn);

        onX = toggleIndicator.anchoredPosition.x; 
        offX = bg.rectTransform.rect.width - toggleIndicator.rect.width;

        MoveIndicator(isOn);

    }
    private void OnEnable()
    {
        Toggle(_isOn);
    }
    public void Toggle(bool value)
    {
        if (value != isOn)
        {
            _isOn = value;

            ToggleColor(isOn);
            MoveIndicator(isOn);

            if (valueChanged != null)
                valueChanged(isOn);
        }
    }
    private void ToggleColor(bool value)
    {
        if (value)
            bg.DOColor(onColor, tweenTime);
        else
            bg.DOColor(offColor, tweenTime);
    }
    private void MoveIndicator(bool value)
    {
        if (value)
            toggleIndicator.DOAnchorPosX(onX, tweenTime);
        else
            toggleIndicator.DOAnchorPosX(offX, tweenTime);

    }
    // Update is called once per frame


    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn);
    }
}
