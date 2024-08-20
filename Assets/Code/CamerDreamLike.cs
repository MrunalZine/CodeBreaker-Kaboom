using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerDreamLike : MonoBehaviour
{
    public float movementRangeX = 1f; // Range of movement along X axis
    public float movementRangeY = 1f; // Range of movement along Y axis
    public float movementSpeed = 0.5f; // Speed of movement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offsetX = Mathf.Sin(Time.time * movementSpeed) * movementRangeX;
        float offsetY = Mathf.Cos(Time.time * movementSpeed) * movementRangeY;

        transform.position = startPosition + new Vector3(offsetX, offsetY, 0);
    }
}
