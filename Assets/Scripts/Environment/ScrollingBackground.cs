using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private float backgroundDistance;
    private float previousCameraPosition;

    public Transform mainCamera;
    public Transform[] backgrounds;
    public float parallaxSpeed;
    
    void Awake()
    {
        backgroundDistance = backgrounds[1].position.x - backgrounds[0].position.x;
        previousCameraPosition = 0.0f;
    }

    void Update()
    {
        var currentDistance = mainCamera.position.x - backgrounds[1].position.x;
        if (currentDistance > backgroundDistance)
        {
            backgrounds[0].position = backgrounds[2].position + new Vector3(backgroundDistance, 0, 0);
            SwapBackgrounds(0, 2);
        }
        else if (currentDistance < -backgroundDistance)
        {
            backgrounds[2].position = backgrounds[0].position + new Vector3(-backgroundDistance, 0, 0);
            SwapBackgrounds(2, 0);
        }
        if (Mathf.Abs(previousCameraPosition - mainCamera.position.x) >  Mathf.Epsilon)
        {
            transform.position = new Vector3(transform.position.x + parallaxSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        previousCameraPosition = mainCamera.position.x;
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
