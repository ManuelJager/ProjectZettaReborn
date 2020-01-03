#pragma warning disable CS0649
#pragma warning disable CS4014
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using Zetta;
using UniRx.Async;
using Zetta.Math.Curves;
using Zetta.UI.Controllers;

namespace Zetta.UI
{
    public class NoticeManager : MonoBehaviour
    {
        public static NoticeManager Instance;

        [SerializeField] private int padding;
        [SerializeField] private GameObject NoticeTextBoxPrefab;
        [SerializeField] private float fadeTime;
        [SerializeField] private bool extendFadeTimeOnContent;
        private float maxHeight;
        private Dictionary<RectTransform, NoticeStatus> notices = new Dictionary<RectTransform, NoticeStatus>();
        private BezierCurve fadeCurve = new BezierCurve(
            new Vector2(0.29f, 0.95f),
            new Vector2(0.29f, 0.95f));

        public NoticeManager()
        {
            Instance = this;
        }

        public async UniTask Prompt(string value, float duration = 1f)
        {
            var NoticeTextBox = Instantiate(NoticeTextBoxPrefab, transform);

            NoticeTextBox.GetComponent<TextWrapController>().Text = value;
            var rect = NoticeTextBox.GetComponent<RectTransform>();

            // Hide component for now
            // Cannot unable gameobject because the gameobject needs to be active for the layout group to calculate
            rect.anchoredPosition = new Vector2(0, -1000);

            // Wait one frame to let layout
            await UniTask.DelayFrame(1);

            var rectHeight = rect.rect.height;
            rect.anchoredPosition = new Vector2(0, -(rectHeight + padding));

            foreach (var notice in notices)
            {
                notice.Value.targetYPos = notice.Value.targetYPos + padding + rectHeight;
            }

            duration += value.Length * 0.05f;

            notices[rect] = new NoticeStatus(
                0f,
                duration,
                rect.gameObject.GetComponent<CanvasGroup>());
        }

        private void Start()
        {
            maxHeight = Screen.height / 3f;
            var rect = GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(-padding, padding);
        }

        private void Update()
        {
            // copy notices dictionary because this loop changes sizes of the original dictionary
            var noticesCopy = new Dictionary<RectTransform, NoticeStatus>(notices);
            foreach (var notice in noticesCopy)
            {
                // unpack
                var rect = notice.Key;
                var status = notice.Value;

                // handle lifetime
                status.lifetimeRemaining -= Time.deltaTime;

                if ((rect.anchoredPosition.y >= maxHeight || status.lifetimeRemaining < fadeTime) && !status.fading)
                {
                    status.fading = true;

                    FadingUtilities.FadeOut(
                        status.group,
                        fadeCurve,
                        fadeTime,
                        () => {
                            notices.Remove(rect);
                            Destroy(rect.gameObject);
                        });
                }

                // if notice is where it is not supossed to be
                if (status.targetYPos != rect.anchoredPosition.y)
                {
                    var currPos = rect.anchoredPosition.y;
                    var nextPos = Math.Interpolationf.MixedInterpolate(currPos, status.targetYPos, 0.05f, 1f);

                    rect.anchoredPosition = new Vector2(0, nextPos);
                }
            }
        }
    }
}

