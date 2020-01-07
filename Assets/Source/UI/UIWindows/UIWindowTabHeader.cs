#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Zetta.UI.UIWindows
{
    [RequireComponent(typeof(Image))]
    public class UIWindowTabHeader : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Image image;
        [SerializeField] private Sprite defaultGraphic;
        [SerializeField] private Sprite disabledGraphic;
        [SerializeField] private Text text;

        public delegate void ClickDelegate();
        public event ClickDelegate Click;

        public string Text
        {
            set => text.text = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            image.sprite = disabledGraphic;
            Click?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            image.sprite = defaultGraphic;
        }

        private void Start()
        {
            image = GetComponent<Image>();
        }
    }
}