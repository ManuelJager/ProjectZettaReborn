using System;
using UnityEngine;

namespace UI
{
    public class NoticeStatus
    {
        public float targetYPos;
        public float lifetimeRemaining;
        public bool fading;
        public CanvasGroup group;
        public NoticeStatus(float targetYPos, float lifetimeRemaining, CanvasGroup group)
        {
            this.targetYPos = targetYPos;
            this.lifetimeRemaining = lifetimeRemaining;
            this.group = group;
            this.fading = false;
        }
    }
}