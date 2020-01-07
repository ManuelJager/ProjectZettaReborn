using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zetta.UI.Utilities;

namespace Zetta.UI.EventTriggers
{
    public class UIElementDragger : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        private RectTransform rectTransform;
        private Vector2 offset;
        private Vector2[] lastFrameRectCorners;
        private Vector2 lastFrameMousePos;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            enabled = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var mousePos = Input.mousePosition;
            var mousePosv2 = new Vector2(
                mousePos.x,
                mousePos.y);

            lastFrameMousePos = mousePosv2;

            var center = RectTransformUtilities.GetScreenSpacePos(rectTransform);
            lastFrameRectCorners = RectTransformUtilities.GetScreenCoordinatesOfCorners(rectTransform, center);

            offset = mousePosv2 - center;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePos = Input.mousePosition;
            var mousePosv2 = new Vector2(
                mousePos.x,
                mousePos.y);

            Vector2[] fourCornerArray;

            if (DetermineValidMousepos(mousePosv2, out fourCornerArray))
            {
                RectTransformUtilities.SetScreenSpacePos(rectTransform, mousePosv2 - offset);
            }

            lastFrameMousePos = mousePosv2;
            lastFrameRectCorners = fourCornerArray;
        }

        /// <summary>
        /// Determine if the next mouse pos would result in the rect staying within screenbounds
        /// </summary>
        /// <param name="newMousePos"></param>
        /// <param name="fourCornerArray"></param>
        /// <returns></returns>
        private bool DetermineValidMousepos(Vector2 newMousePos, out Vector2[] fourCornerArray)
        {
            var diff = lastFrameMousePos - newMousePos;
            fourCornerArray = lastFrameRectCorners;
            for (int i = 0; i < 4; i++)
            {
                fourCornerArray[i] = new Vector2(
                    fourCornerArray[i].x - diff.x,
                    fourCornerArray[i].y - diff.y);
            };
            return RectTransformUtilities.CornersAreWithinScreenBounds(fourCornerArray);
        }
    }
}
