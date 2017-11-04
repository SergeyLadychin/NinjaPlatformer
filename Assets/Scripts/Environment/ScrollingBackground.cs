using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private float backgroundDistance;
    private float previousCameraPosition;
    private Vector3 scrollDirection;

    public Transform mainCamera;
    public MoveAxis type;
    public Transform[] backgrounds;
    public float parallaxSpeed;
    
    void Awake()
    {
        backgroundDistance = GetDistance(backgrounds[1].position, backgrounds[0].position);
        previousCameraPosition = 0.0f;
        scrollDirection = type == MoveAxis.Horizontal ? Vector3.right : Vector3.up;
    }

    void Update()
    {
        var currentDistance = GetDistance(mainCamera.position, backgrounds[1].position);
        if (currentDistance > backgroundDistance)
        {
            backgrounds[0].position = backgrounds[2].position + backgroundDistance * scrollDirection;
            SwapBackgrounds(0, 2);
        }
        else if (currentDistance < -backgroundDistance)
        {
            backgrounds[2].position = backgrounds[0].position - backgroundDistance * scrollDirection;
            SwapBackgrounds(2, 0);
        }
        if (Mathf.Abs(previousCameraPosition - mainCamera.position.x) > Mathf.Epsilon)
        {
            transform.position = transform.position + parallaxSpeed * Time.deltaTime * scrollDirection;
        }
        previousCameraPosition = mainCamera.position.x;
    }

    private float GetDistance(Vector3 point1, Vector3 point2)
    {
        return type == MoveAxis.Horizontal ? point1.x - point2.x : point1.y - point2.y;
    }

    private void SwapBackgrounds(int index1, int index2)
    {
        var temp1 = backgrounds[1];
        var temp2 = backgrounds[index2];
        backgrounds[index2] = backgrounds[index1];
        backgrounds[1] = temp2;
        backgrounds[index1] = temp1;
    }
}

public enum MoveAxis
{
    Horizontal,
    Vertical
}
