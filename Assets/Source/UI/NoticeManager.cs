using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NoticeManager : MonoBehaviour
    {
        public static NoticeManager Instance;

        [SerializeField] private int padding;
        [SerializeField] private GameObject NoticeTextBoxPrefab;
        private float maxHeight;
        private Dictionary<RectTransform, NoticeStatus> notices = new Dictionary<RectTransform, NoticeStatus>();
        private Zetta.Math.BezierCurve fadeCurve = new Zetta.Math.BezierCurve(
            new Vector2(0.29f, 0.95f),
            new Vector2(0.29f, 0.95f));

        public NoticeManager()
        {
            Instance = this;
        }

        public void Prompt(string value, float duration = 3f)
        {
            var NoticeTextBox = Instantiate(NoticeTextBoxPrefab, transform);

            NoticeTextBox.GetComponent<TextWrapController>().Text = value;
            var rect = NoticeTextBox.GetComponent<RectTransform>();

            // Hide component for now
            // Cannot unable gameobject because the gameobject needs to be active for the layout group to calculate
            rect.anchoredPosition = new Vector2(0, -1000);
            // Unhide and set pos of element after layout group calculated element height
            StartCoroutine(LatePrompt(rect, duration));
        }

        private void Start()
        {
            maxHeight = Screen.height / 3f;
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
                if ((rect.anchoredPosition.y >= maxHeight || status.lifetimeRemaining < 0.3f) && !status.fading)
                {
                    status.fading = true;
                    Zetta.UI.FadingUtilities.FadeOut(status.group, fadeCurve, 0.3f);
                }
                if (status.lifetimeRemaining < 0f)
                {
                    notices.Remove(rect);
                    Destroy(rect.gameObject);
                }

                // if notice is where it is not supossed to be
                if (status.targetYPos != rect.anchoredPosition.y)
                {
                    var currPos = rect.anchoredPosition.y;
                    var nextPos = Zetta.Math.MixedInterpolate(currPos, status.targetYPos, 0.05f, 1f);

                    rect.anchoredPosition = new Vector2(0, nextPos);
                }
            }
        }

        private IEnumerator LatePrompt(RectTransform rect, float duration)
        {
            yield return new WaitForEndOfFrame();
            
            var rectHeight = rect.rect.height;
            rect.anchoredPosition = new Vector2(0, -rectHeight);

            foreach (var notice in notices)
            {
                notice.Value.targetYPos = notice.Value.targetYPos + padding + rectHeight;
            }

            notices[rect] = new NoticeStatus(
                padding, 
                duration, 
                rect.gameObject.GetComponent<CanvasGroup>());
        }
    }
}

