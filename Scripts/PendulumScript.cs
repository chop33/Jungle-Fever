using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumScript : MonoBehaviour {

    Quaternion start, end;
    [SerializeField, Range(0.0f, 360f)]
    private float angle = 90.0f;
    [SerializeField, Range(0.0f, 5.0f)]
    private float speed = 2.0f;
    [SerializeField, Range(0.0f, 10.0f)]
    private float startTime = 0.0f;

    private void Start()
    {
        start = PendulumRotation(angle);
        end = PendulumRotation(-angle);
    }

    void ResetTimer()
    {
        startTime = 0.0f;
    }

    private void FixedUpdate()
    {
        startTime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(start, end, (Mathf.Sin(startTime * speed + Mathf.PI / 2) + 1.0f) / 2.0f);
    }

    Quaternion PendulumRotation(float angle)
    {
        var pendulumRotation = transform.rotation;
        var rotationAngle = pendulumRotation.eulerAngles.x + angle;

        if (rotationAngle > 180)
        {
            rotationAngle -= 360;
        }
        else if (rotationAngle < -180)
        {
            rotationAngle += 360;
        }
        pendulumRotation.eulerAngles = new Vector3(rotationAngle, pendulumRotation.eulerAngles.y, pendulumRotation.eulerAngles.z);
        return pendulumRotation;
    }
}
