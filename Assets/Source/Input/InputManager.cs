using UnityEngine;
using Zetta.Generics;

namespace Zetta.InputWrapper
{
    /// <summary>
    /// Global event driven inputManager
    /// </summary>
    public partial class InputManager : LazySingleton<InputManager>
    {
        public delegate void UpdateDelegate();

        public static UpdateDelegate UpdateEvent;

        public delegate void ButtonActionClickDelegate();

        public static event ButtonActionClickDelegate ClickShift;

        public static event ButtonActionClickDelegate ClickEsc;

        public static event ButtonActionClickDelegate ClickF10;

        public delegate void ButtonKeypressClickDelegate(char keyPressed);

        public static event ButtonKeypressClickDelegate ClickKeypress;

        public delegate void InputAxisDelegate(Vector2 input);

        public static event InputAxisDelegate InputAxis;

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
            if (horizontalAxis != 0f || verticalAxis != 0f)
            {
                InputAxis?.Invoke(new Vector2(horizontalAxis, verticalAxis));
            }
        }

        [RuntimeInitializeOnLoadMethod]
        public static void EchoThis() => Echo();
    }
}