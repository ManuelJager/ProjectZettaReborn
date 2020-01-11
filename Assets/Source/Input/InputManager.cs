using UnityEngine;
using Zetta.Generics;

namespace Zetta.InputWrapper
{
    /// <summary>
    /// Global event driven inputManager
    /// </summary>
    public partial class InputManager : AutoInstanceMonoBehaviour<InputManager>
    {
        public delegate void UpdateDelegate();

        public delegate void ButtonActionClickDelegate();

        public delegate void ButtonKeypressClickDelegate(char keyPressed);

        public delegate void InputAxisDelegate(Vector2 input);

        public static event UpdateDelegate UpdateEvent;

        public static event ButtonKeypressClickDelegate ClickKeypress;

        public static event ButtonActionClickDelegate ClickShift;

        public static event ButtonActionClickDelegate ClickEsc;

        public static event ButtonActionClickDelegate ClickF10;

        public static event InputAxisDelegate InputAxis;

        public static event UpdateDelegate InputAxisRelease;

        private Vector2 previousInputAxis = new Vector2(0f, 0f);

        private void OnGUI()
        {
            var currentEvent = Event.current;
            if (currentEvent.isKey && currentEvent.type == EventType.KeyDown)
            {
                var keyCode = currentEvent.keyCode;
                if (keyCode == KeyCode.None)
                {
                    return;
                }
                char key;
                if (TryParseKeycode(keyCode, out key))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        key = char.ToUpper(key);
                    }
                    ClickKeypress?.Invoke(key);
                }
            }
        }

        private void Update()
        {
            UpdateEvent?.Invoke();

            // ButtonPresses
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ClickShift?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClickEsc?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                ClickF10?.Invoke();
            }

            // Axis input
            var horizontalAxis = Input.GetAxisRaw("Horizontal");
            var verticalAxis = Input.GetAxisRaw("Vertical");
            Vector2 axis = new Vector2(horizontalAxis, verticalAxis);

            if (horizontalAxis != 0f || verticalAxis != 0f)
            {
                InputAxis?.Invoke(axis);
            }
            else if (horizontalAxis == 0f && verticalAxis == 0f && (previousInputAxis.x != 0f || previousInputAxis.y != 0f))
            {
                InputAxisRelease?.Invoke();
            }
            previousInputAxis = axis;
        }
    }
}