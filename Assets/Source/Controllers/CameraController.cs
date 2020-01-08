using System.Collections;
using UnityEngine;
using Zetta.GridSystem;
using Zetta.Math;

namespace Zetta.Controllers
{
    public class CameraController : MonoBehaviour
    {
        // Singleton instance
        public static CameraController Instance;

        // The size of the ship on the screen
        [Range(0.0f, 6.0f)] public float sizeOnScreen = 4.8f;

        // The field of view of the camera
        public float FOV = 60f;

        public float cameraLerp = 2f;
        public float cameraLerpThreshold = 0.5f;
        public float cameraBias = 1.9f;
        public float lerpTime = 1.5f;
        public float maxAccelerationDistance = 3f;
        public float maxCameraZ = 1000f;

        private Vector2 acceleration = new Vector2(0f, 0f);

        private Vector2 targetPosition;
        private Vector2 currentPosition;

        private float targetZoom = 0f;
        private float currentZoom;

        // The current camera of the camera controller
        private Camera currentCamera;

        public CameraController()
        {
            // The set instance of the singleton
            Instance = this;
        }

        public void Awake()
        {
            // Set the current camera
            currentCamera = GetComponent<Camera>();

            // Set the field of view of the current camera
            currentCamera.fieldOfView = FOV;

            // Subscribe to the acceleration event
            PlayerController.Instance.PlayerStartAccelerating += (newAcceleration) => acceleration += newAcceleration;
            PlayerController.Instance.PlayerStoppedAccelerating += () => acceleration = new Vector2(0f, 0f);
        }

        public void Update()
        {
            // Get the current player ship
            Ship toFollow = PlayerController.Instance.Ship;
            if(toFollow != null)
            {
                float shipX = toFollow.transform.position.x;
                float shipY = toFollow.transform.position.y;

                // Calculate the acceleration delta
                Vector2 accelerationDelta = new Vector2(
                    MaxValue(maxAccelerationDistance, acceleration.x / 100),
                    MaxValue(maxAccelerationDistance, acceleration.y / 100));

                // Calculate the new camera x, y position
                Vector2 normalizedVector = GetCenterMouseOffset(
                    new Vector2(Screen.width, Screen.height),
                    Input.mousePosition);

                targetPosition = new Vector2(
                    cameraLerp * (normalizedVector.x + accelerationDelta.x),
                    cameraLerp * (normalizedVector.y + accelerationDelta.y)
                    );

                currentPosition = Vector2.Lerp(currentPosition, targetPosition, Time.deltaTime * 3);

                // Calculate the new camera zoom (z axis)
                targetZoom += Input.mouseScrollDelta.y;
                targetZoom = targetZoom < 0 ? targetZoom : 0;
                targetZoom = targetZoom > -maxCameraZ ? targetZoom : -maxCameraZ;
                currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * 2);

                // Set the new position
                transform.position = new Vector3(shipX + currentPosition.x, shipY + currentPosition.y, -10 + currentZoom);
            }
        }

        /// <summary>
        /// Starts to lerp towards the position of the size of the vector2
        /// </summary>
        /// <param name="size">The size of the grid</param>
        public void StartLerpZoom(Vector2 size)
        {
            StartCoroutine(LerpZoom(currentCamera.transform.position, ZoomCamera(size), lerpTime));
        }

        /// <summary>
        /// Lerp towards the target
        /// </summary>
        public IEnumerator LerpZoom(Vector3 startPosition, Vector3 endPosition, float time)
        {
            float elapsedTime = 0;

            // Run till the time is elapsed
            while (elapsedTime < time)
            {
                // Update the position to respect the lerp
                currentCamera.transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / time));

                elapsedTime += Time.deltaTime;

                // Yield a new frame if this frame is done
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Zoom the camera
        /// </summary>
        /// <param name="size">The size of the grid</param>
        /// <param name="setPosition">Set the position or just return the new position</param>
        /// <returns>The vector of the new position of the camera</returns>
        public Vector3 ZoomCamera(Vector2 size, bool setPosition)
        {
            // Calculate the biggest value in the vector
            float biggest = size.x > size.y ? size.x : size.y;

            // Calculate the new z position
            float posZ = -biggest * (cameraBias * (6f - System.Math.Abs(sizeOnScreen)));

            // Set the new position
            Vector3 currentPosition = currentCamera.transform.position;
            currentPosition.z = posZ;

            if (setPosition)
            {
                currentCamera.transform.position = currentPosition;
            }

            return currentPosition;
        }

        /// <summary>
        /// Zoom the camera
        /// </summary>
        /// <param name="size">The size of the grid</param>
        /// <returns>The vector of the new position of the camera</returns>
        public Vector3 ZoomCamera(Vector2 size)
        {
            return ZoomCamera(size, false);
        }

        /// <summary>
        /// Maxizes and minimizes the given values
        /// </summary>
        /// <param name="max">The max value</param>
        /// <param name="val">The current value</param>
        /// <returns>The maximized value</returns>
        private float MaxValue(float max, float val)
        {
            val = val > max ? max : val;
            val = val < -max ? -max : val;
            return val;
        }

        /// <summary>
        /// Normalizes the given dimension and position to its normalized value
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="position"></param>
        /// <returns>The normalized value(-1 to 1)</returns>
        private float Normalized(float dimension, float position)
        {
            float val = Geometryf.NormalizeValue(0, dimension, position);
            val = MaxValue(1, val);

            val = System.Math.Abs(val) < cameraLerpThreshold ? 0 : val;
            return val;
        }

        /// <summary>
        /// Gets the mouse offset relative to the center of the given screen dimensions
        /// </summary>
        /// <param name="screenDimensions">The dimensions of the screen</param>
        /// <param name="mousePosition">The current mouse position</param>
        /// <returns>Vector of normalized centered offset (-1 till 1)</returns>
        private Vector2 GetCenterMouseOffset(Vector2 screenDimensions, Vector2 mousePosition)
        {
            return new Vector2(
                Normalized(screenDimensions.x, mousePosition.x),
                Normalized(screenDimensions.y, mousePosition.y));
        }

    }
}