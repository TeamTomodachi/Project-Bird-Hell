using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetween : MonoBehaviour
{
    public Vector3 StartingPosition { get; set; }
    public Vector3 LastPosition { get; set; }
    public Vector3 LastLocalPosition { get; set; }

    public Vector3 Speed;
    public Vector3 Distance;

    public bool MovingRight = true;
    public bool MovingUp = true;
    public bool MovingForward = true;

    private float MinHorizontal { get { return StartingPosition.x - Distance.x; } }
    private float MaxHorizontal { get { return StartingPosition.x + Distance.x; } }
    private float MinVertical { get { return StartingPosition.y - Distance.y; } }
    private float MaxVertical { get { return StartingPosition.y + Distance.y; } }
    private float MinDepth { get { return StartingPosition.z - Distance.z; } }
    private float MaxDepth { get { return StartingPosition.z + Distance.z; } }

    // Use this for initialization
    void Start()
    {
        StartingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Save the old values for differences
        LastLocalPosition = transform.position;
        LastPosition = transform.localPosition;

        // Grab the current Position
        Vector3 newPosition = LastPosition;

        // Move the directions
        if (MovingRight) { newPosition.x += Speed.x * Time.deltaTime; }
        else { newPosition.x -= Speed.x * Time.deltaTime; }

        if (MovingUp) { newPosition.y += Speed.y * Time.deltaTime; }
        else { newPosition.y -= Speed.y * Time.deltaTime; }

        if (MovingForward) { newPosition.z += Speed.z * Time.deltaTime; }
        else { newPosition.z -= Speed.z * Time.deltaTime; }

        // Clamp to Min/Max
        newPosition.x = Mathf.Clamp(newPosition.x, MinHorizontal, MaxHorizontal);
        newPosition.y = Mathf.Clamp(newPosition.y, MinVertical, MaxVertical);
        newPosition.z = Mathf.Clamp(newPosition.z, MinDepth, MaxDepth);

        // Flip the switches when its time
        if (newPosition.x >= MaxHorizontal) MovingRight = false;
        else if (newPosition.x <= MinHorizontal) MovingRight = true;

        if (newPosition.y >= MaxVertical) MovingUp = false;
        else if (newPosition.y <= MinVertical) MovingUp = true;

        if (newPosition.z >= MaxDepth) MovingForward = false;
        else if (newPosition.z <= MinDepth) MovingForward = true;

        // Update the Position
        transform.localPosition = newPosition;
    }
}
