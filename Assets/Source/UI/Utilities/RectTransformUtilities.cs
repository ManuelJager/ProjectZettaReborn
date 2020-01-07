using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Zetta.UI.Utilities
{
    public static class RectTransformUtilities
    {
        /// <summary>
        /// Get coordinates of corners in pixel format
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <returns></returns>
        public static Vector2[] GetScreenCoordinatesOfCorners(RectTransform rectTransform)
        {
            var center = GetScreenSpacePos(rectTransform);
            return GetScreenCoordinatesOfCorners(rectTransform, center);
        }

        /// <summary>
        /// Get coordinates of corners in pixel format with assumed rect transform pixel center
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <returns></returns>
        public static Vector2[] GetScreenCoordinatesOfCorners(RectTransform rectTransform, Vector2 center)
        {
            var worldCorners = new Vector2[4];

            var halfWidth = rectTransform.rect.width / 2;
            var halfHeight = rectTransform.rect.height / 2;

            worldCorners[0] = new Vector2(
                center.x - halfWidth,
                center.y - halfHeight);

            worldCorners[1] = new Vector2(
                center.x + halfWidth,
                center.y - halfHeight);

            worldCorners[2] = new Vector2(
                center.x + halfWidth,
                center.y + halfHeight);

            worldCorners[3] = new Vector2(
                center.x - halfWidth,
                center.y + halfHeight);

            return worldCorners;
        }

        public static bool CornersAreWithinScreenBounds(RectTransform rectTransform)
        {
            var fourCornerArray = GetScreenCoordinatesOfCorners(rectTransform);
            return CornersAreWithinScreenBounds(fourCornerArray);
        }

        public static bool CornersAreWithinScreenBounds(Vector2[] fourCornerArray)
        {
            if (fourCornerArray.Length != 4)
            {
                throw new Exception("Wrong size array");
            }
            return !fourCornerArray.ToList().Any((point) =>
            {
                return point.x < 0f || point.x > Screen.width
                || point.y < 0f || point.y > Screen.height;
            });
        }

        /// <summary>
        /// Gets the center of the rect in pixel format
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Vector2 GetScreenSpacePos(RectTransform rectTransform)
        {
            return new Vector2(
                rectTransform.anchoredPosition.x + Screen.width / 2,
                rectTransform.anchoredPosition.y + Screen.height / 2);
        }

        /// <summary>
        /// set position of rect transform with pixel format
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="pos"></param>
        public static void SetScreenSpacePos(RectTransform rectTransform, Vector2 pos)
        {
            rectTransform.anchoredPosition = new Vector2(
                pos.x - Screen.width / 2,
                pos.y - Screen.height / 2);
        }

        [System.Obsolete]
        public static void Log(this IEnumerable list)
        {
            foreach (var item in list)
            {
                Debug.Log(item);
            }
        }
    }
}