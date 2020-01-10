#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Zetta.UI.UIWindows
{
    [RequireComponent(typeof(Image))]
    public class UIWindowTabHeader : MonoBehaviour, IPointerDownHandler
    {
        private bool selected;
        private Image image;
        [SerializeField] private Sprite defaultGraphic;
        [SerializeField] private Sprite disabledGraphic;
        [SerializeField] private Text text;

        public delegate void ClickDelegate();
        public event ClickDelegate Click;
        public UIWindowTabContent tabContent;

        public bool Selected
        {
            get => selected;
            set => image.sprite = value ? disabledGraphic : defaultGraphic;
        }

        public string Text
        {
            set => text.text = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            image.sprite = disabledGraphic;
            if (!Selected)
            {
                Click?.Invoke();
                Selected = true;
            }
        }

        private void Start()
        {
            image = GetComponent<Image>();
        }
    }
}