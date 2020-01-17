using UnityEngine;
using Zetta.Extensions;
using Zetta.Generics;
using Zetta.GridSystem;
using Zetta.InputWrapper;
using Zetta.UI;

namespace Zetta.Controllers
{
    /// <summary>
    /// Takes it input from global events from <see cref="InputManager"/>
    /// </summary>

    public partial class PlayerController : AutoInstanceMonoBehaviour<PlayerController>
    {
        private Ship ship;

        private Quaternion q;
        private Camera orthographicCamera;

        public Ship Ship
        {
            get => ship;
            set
            {
                Enabled = value != null;
                ship = value;
            }
        }

        public bool Enabled
        {
            get => enabled;
            set
            {
                gameObject.SetActive(value);
                UIManager.GameplayLayerActiveState = value;
            }
        }

        private new void Awake()
        {
            base.Awake();
            Debug.Log("Awoken");
            orthographicCamera = CameraExtensions.GetMainOrthgraphicCamera();
            orthographicCamera.enabled = false;
            Enabled = false;
        }

        private void RotateShipToCursor()
        {
            q = GridUtilities.GetMouseWorldPos(ship.transform, orthographicCamera);
            ship.transform.rotation = GridUtilities.MouseLookAtRotation(ship.transform, 100);
        }

        /// <summary>
        /// Ship input
        /// </summary>
        /// <param name="input"></param>
        public void OnAxis(Vector2 input)
        {
            // Calculate the force to add
            var inputRotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            inputRotation += q.eulerAngles.z + 270f;
            inputRotation %= 360f;
            input = Math.Vectorf.DegreeToVector2(inputRotation);

            // Add the force to the rigidbody
            ship.rb2d.AddForce(input);

            // Fire the accelerate event
            PlayerStartAccelerating?.Invoke(input);
        }

        public void ReleaseAxis()
        {
            PlayerStoppedAccelerating?.Invoke();
        }

        private void OnEnable()
        {
            InputManager.InputAxis += OnAxis;
            InputManager.InputAxisRelease += ReleaseAxis;
            InputManager.UpdateEvent += RotateShipToCursor;
        }

        private void OnDisable()
        {
            InputManager.InputAxis -= OnAxis;
            InputManager.InputAxisRelease -= ReleaseAxis;
            InputManager.UpdateEvent -= RotateShipToCursor;
        }
    }
}