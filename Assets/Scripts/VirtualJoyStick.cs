using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bg;
    private Image joystick;
    public Vector2 input;
	public bool isReference = false;

    void Start() {
        bg = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData e) {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bg.rectTransform, e.position, e.pressEventCamera, out pos)) {
            pos.x = (pos.x / bg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bg.rectTransform.sizeDelta.y);

            input = new Vector2(pos.x*2.0f + 1, pos.y*2.0f - 1);
            input = (input.magnitude > 1.0f)? input.normalized : input;

            joystick.rectTransform.anchoredPosition = new Vector2(input.x * bg.rectTransform.sizeDelta.x / 4.0f, input.y * bg.rectTransform.sizeDelta.y / 4.0f);
            Debug.Log(input);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnDrag(eventData);

    }

    public void OnPointerUp(PointerEventData eventData) {
        input = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

}
