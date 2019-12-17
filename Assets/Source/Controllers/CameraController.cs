using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The bias of the average camera size
    public const float cameraBias = 1.9f;

    public const float lerpSpeed = 23f;

    // The field of view of the camera
    public float FOV = 60;

    [Range(0.0f, 6.0f)]
    public float sizeOnScreen = 5.3f;

    public void Awake()
    {
        // Set the current camera to the main camera
        Camera.SetupCurrent(Camera.main);

        // Set the field of view of the current camera
        Camera.current.fieldOfView = FOV;

        ZoomCamera(new Vector2(11, 11), true);
    }

    public void Start()
    {
        StartLerpZoom(new Vector2(5, 5));
    }

    public void Update()
    {
        LerpZoom();
    }

    private float startTime;
    private float length;
    private Vector3 startPosition;
    private Vector3 endPosition;

    public void StartLerpZoom(Vector2 toPosition)
    {
        startPosition = Camera.current.transform.position;
        endPosition = ZoomCamera(toPosition);

        startTime = Time.time;
        length = Vector3.Distance(startPosition, endPosition);
    }

    public void LerpZoom()
    {
        float distCovered = (Time.time - startTime) * lerpSpeed;
        float fraction = distCovered / length;

        Camera.main.transform.position = Vector3.Lerp(startPosition, endPosition, fraction);
    }

    public Vector3 ZoomCamera(Vector2 shipSize, bool setPosition)
    {
        float biggest = shipSize.x > shipSize.y ? shipSize.x : shipSize.y;

        float posZ = -biggest * (cameraBias * (6f - Math.Abs(sizeOnScreen)));

        Vector3 currentPosition = Camera.current.transform.position;
        currentPosition.z = posZ;

        if(setPosition)
            Camera.current.transform.position = currentPosition;

        return currentPosition;
    }

    public Vector3 ZoomCamera(Vector2 shipSize)
    {
        return ZoomCamera(shipSize, false);
    }
}
