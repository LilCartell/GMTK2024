using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI text;

    private Button _button;
    private bool _highlighted;
    private bool _pressed;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_button.interactable)
            text.color = _button.colors.disabledColor;
        else if (_pressed)
            text.color = _button.colors.pressedColor;
        else if(_highlighted)
            text.color= _button.colors.highlightedColor;
        else
            text.color = _button.colors.normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _highlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _highlighted = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
    } 
}
