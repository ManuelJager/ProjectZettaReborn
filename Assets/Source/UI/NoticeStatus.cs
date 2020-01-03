using System;
using UnityEngine;

namespace Zetta.UI
{
    public class NoticeStatus
    {
        public float targetYPos;
        public float lifetimeRemaining;
        public CanvasGroup group;
        public bool fading;
        public NoticeStatus(float targetYPos, float lifetimeRemaining, CanvasGroup group)
        {
            this.targetYPos = targetYPos;
            this.lifetimeRemaining = lifetimeRemaining;
            this.group = group;
            this.fading = false;
        }
    }
}