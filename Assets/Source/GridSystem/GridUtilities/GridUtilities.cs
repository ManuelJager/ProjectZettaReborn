using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GridUtilities
{
    /// <summary>
    /// Rotates a target towards the mouse at a constant rate
    /// </summary>
    /// <param name="target">Target GameObject</param>
    /// <param name="camera">Rendering camera</param>
    /// <param name="turningRate">Speed at which the object should turn to any side</param>
    public static Quaternion MouseLookAtRotation(Transform target, float turningRate, Camera camera = null)
    {
        camera = camera ?? Camera.main;
        Quaternion q = GetMouseWorldPos(target, camera);
        var zStep = CalculateZStep(target.rotation.eulerAngles, q.eulerAngles, turningRate);
        return Quaternion.Euler(target.rotation.eulerAngles + new Vector3(0f, 0f, zStep));
    }

    /// <summary>
    /// Rotates an object towards a target at a constant rate
    /// </summary>
    /// <param name="current">The gameObject that needs to rotate</param>
    /// <param name="target">Target GameObject</param>
    /// <param name="turningRate">Speed at which the object should turn to any side</param>
    /// <returns></returns>
    public static Quaternion ObjectLookAtRotation(GameObject current, GameObject target, float turningRate)
    {
        Quaternion q = GetRotationToTarget(current, target);
        var zStep = CalculateZStep(current.transform.rotation.eulerAngles, q.eulerAngles, turningRate);
        return Quaternion.Euler(current.transform.rotation.eulerAngles + new Vector3(0f, 0f, zStep));
    }

    /// <summary>
    /// gets rotation relative from target to mouse position
    /// </summary>
    public static Quaternion GetMouseWorldPos(Transform target, Camera camera = null)
    {
        camera = camera ?? Camera.main;
        Vector3 vectorToTarget = camera.ScreenToWorldPoint(Input.mousePosition) - target.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// gets rotation relative from current to target
    /// </summary>
    public static Quaternion GetRotationToTarget(GameObject current, GameObject target)
    {
        Vector3 vectorToTarget = current.transform.position - target.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// Calculates the z angle step linearly
    /// <para>
    /// Prevents over rotation
    /// </para>
    /// </summary>
    /// <param name="targetRotation">Rotation from object to target</param>
    /// <param name="currentRotation">Rotation of object</param>
    /// <param name="turningRate">Speed at which the object should turn to any side</param>
    /// <returns></returns>
    private static float CalculateZStep(Vector3 targetRotation, Vector3 currentRotation, float turningRate)
    {
        float zDif = GetZDif(targetRotation, currentRotation);
        //wether to rotate left(true) or right(false)
        bool isTargetRotationLeftToRotation = IsTargetRotationLeftToRotation(targetRotation, currentRotation);
        //angle in degrees the rotation should added uppon
        float zStep = isTargetRotationLeftToRotation ? turningRate : -turningRate;
        zStep *= Time.deltaTime;
        //the effective value of zStep
        return Mathf.Abs(zStep) > zDif ? isTargetRotationLeftToRotation ? zDif : -zDif : zStep;
    }

    /// <summary>
    /// returns wether the target rotation is to the left or to the right of the current rotation
    /// </summary>
    private static bool IsTargetRotationLeftToRotation(Vector3 targetRotation, Vector3 currentRotation)
    {
        return ((targetRotation.z - currentRotation.z + 360f) % 360f) > 180.0f;
    }

    /// <summary>
    /// returns the smallest difference between the two vector 3 rotations along the z axis
    /// </summary>
    private static float GetZDif(Vector3 targetRotation, Vector3 currentRotation)
    {
        return Mathf.Min(Mathf.Abs(targetRotation.z - currentRotation.z), Mathf.Abs(currentRotation.z - targetRotation.z));
    }

    /// <summary>
    /// Rotates a vector2 along the z axis
    /// </summary>
    public static Vector2 RotateVector2(Vector2 vector, float angle)
    {
        var theta = angle * Mathf.Deg2Rad;

        var cs = Mathf.Cos(theta);
        var sn = Mathf.Sin(theta);

        var px = vector.x * cs - vector.y * sn;
        var py = vector.x * sn + vector.y * cs;

        return new Vector2(px, py);
    }
}
