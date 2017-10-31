using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public CameraDeathzone deathzone;
    public bool drawDeathzone;

    void Awake()
    {
        deathzone.position = player.position;
    }

    void Update ()
	{
        var topLeftPoint = new Vector3(deathzone.position.x - deathzone.size.x + deathzone.offset.x, deathzone.position.y + deathzone.size.y + deathzone.offset.y);
	    var bottomRightPoint = new Vector3(deathzone.position.x + deathzone.size.x + deathzone.offset.x, deathzone.position.y - deathzone.size.y + deathzone.offset.y);

        if (drawDeathzone)
        {
            var topRightPoint = new Vector3(deathzone.position.x + deathzone.size.x + deathzone.offset.x, deathzone.position.y + deathzone.size.y + deathzone.offset.y);
            var bottomLeftPoint = new Vector3(deathzone.position.x - deathzone.size.x + deathzone.offset.x, deathzone.position.y - deathzone.size.y + deathzone.offset.y);
        
            DrawDeathzone(topLeftPoint, topRightPoint, bottomLeftPoint, bottomRightPoint);
        }

	    if (topLeftPoint.x > player.position.x)
	    {
	        transform.position = new Vector3(transform.position.x - (topLeftPoint.x - player.position.x), transform.position.y, transform.position.z);
	    }
        else if (bottomRightPoint.x < player.position.x)
	    {
	        transform.position = new Vector3(transform.position.x + (player.position.x - bottomRightPoint.x), transform.position.y, transform.position.z);
	    }

	    if (topLeftPoint.y < player.position.y)
	    {
	        transform.position = new Vector3(transform.position.x, transform.position.y + (player.position.y - topLeftPoint.y), transform.position.z);
	    }
        else if (bottomRightPoint.y > player.position.y)
	    {
	        transform.position = new Vector3(transform.position.x, transform.position.y - (bottomRightPoint.y - player.position.y), transform.position.z);
	    }

	    deathzone.position = transform.position;
	}

    private void DrawDeathzone(Vector3 topLeftPoint, Vector3 topRightPoint, Vector3 bottomLeftPoint, Vector3 bottomRightPoint)
    {
        Debug.DrawLine(topLeftPoint, topRightPoint);
        Debug.DrawLine(topRightPoint, bottomRightPoint);
        Debug.DrawLine(bottomRightPoint, bottomLeftPoint);
        Debug.DrawLine(bottomLeftPoint, topLeftPoint);
    }
}

[Serializable]
public class CameraDeathzone
{
    [NonSerialized]
    public Vector2 position;
    public Vector2 size;
    public Vector2 offset;
}
