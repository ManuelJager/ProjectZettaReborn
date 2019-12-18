using System;
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
    private const float LERP_SPEED = 1.5f;

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

    public void Update()
    {
        // Update the lerp
        LerpZoom();
    }

    // Lerp variables
    private float startTime;
    private float length;
    private Vector3 startPosition;
    private Vector3 endPosition;

    /// <summary>
    /// Starts to lerp towards the position of the size of the vector2
    /// </summary>
    /// <param name="size">The size of the grid</param>
    public void StartLerpZoom(Vector2 size)
    {
        startPosition = currentCamera.transform.position;
        endPosition = ZoomCamera(size);

        startTime = Time.time;
        length = Vector3.Distance(startPosition, endPosition);
    }

    /// <summary>
    /// Update the lerp towards the target
    /// </summary>
    public void LerpZoom()
    {
        // Calculate the current distance covered in the lerp
        float distCovered = (Time.time - startTime) * LERP_SPEED;

        // Set the position of the current camera to respect the lerp
        currentCamera.transform.position = Vector3.Lerp(startPosition, endPosition, distCovered);
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
