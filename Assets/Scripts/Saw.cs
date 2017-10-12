using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private Transform startTransform;
    private Transform endTransform;
    private Transform sawTransform;

    private float t = 0.0f;
    private int multiplier = 1;

    [HideInInspector]public bool dontMove;
    public float speed = 0.5f;
    public float angularSpeed = 300.0f;

	void Awake ()
	{
	    startTransform = transform.Find("Start");
	    endTransform = transform.Find("End");
	    sawTransform = transform.Find("Saw");

	    dontMove = endTransform.localPosition == startTransform.localPosition;
	    if (!dontMove)
	    {
	        t = (sawTransform.localPosition - startTransform.localPosition).sqrMagnitude / (endTransform.localPosition - startTransform.localPosition).sqrMagnitude;
        }
	}

	void Update ()
	{
        sawTransform.Rotate(new Vector3(0, 0, Time.deltaTime * angularSpeed));

        if (!dontMove)
        {
            sawTransform.localPosition = Vector3.Lerp(startTransform.localPosition, endTransform.localPosition, t);

            t += multiplier * Time.deltaTime * speed;
            if (t > 1.0f)
            {
                t = 1.0f;
                multiplier = -1;
            }
            else if (t < 0.0f)
            {
                t = 0.0f;
                multiplier = 1;
            }
        }
    }
}
