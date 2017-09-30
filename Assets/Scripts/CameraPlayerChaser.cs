using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerChaser : MonoBehaviour
{
    public Transform player;
    public float horizontalOffset = .0f;
    public float verticalOffset = .0f;

	void Update ()
	{
	    transform.localPosition = new Vector3(player.localPosition.x + horizontalOffset, player.localPosition.y + verticalOffset, transform.localPosition.z);
	}
}
