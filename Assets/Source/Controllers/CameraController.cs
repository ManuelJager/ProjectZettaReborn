using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Singleton instance
    public static CameraController Instance;

    public CameraController()
    {
        // The set instance of the singleton
        Instance = this;
    }

    // The current camera of the camera controller
    private Camera currentCamera;

    // Contants
    private const float CAMERA_BIAS = 1.9f;
    private const float LERP_TIME = 0.8f;

    // The field of view of the camera
    public float FOV = 60f;

    // The size of the ship on the screen
    [Range(0.0f, 6.0f)] public float sizeOnScreen = 4.8f;

    public void Awake()
    {
        // Set the current camera
        currentCamera = GetComponent<Camera>();

        // Set the field of view of the current camera
        currentCamera.fieldOfView = FOV;
    }

    /// <summary>
    /// Starts to lerp towards the position of the size of the vector2
    /// </summary>
    /// <param name="size">The size of the grid</param>
    public void StartLerpZoom(Vector2 size)
    {
        StartCoroutine(LerpZoom(currentCamera.transform.position, ZoomCamera(size), LERP_TIME));
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
        float posZ = -biggest * (CAMERA_BIAS * (6f - Math.Abs(sizeOnScreen)));

        // Set the new position
        Vector3 currentPosition = currentCamera.transform.position;
        currentPosition.z = posZ;

        if(setPosition)
            currentCamera.transform.position = currentPosition;

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
}
