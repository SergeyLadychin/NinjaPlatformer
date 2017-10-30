using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    private const int damage = 1000000;
    private float t = 0.0f;
    private int multiplier = 1;

    [HideInInspector] public bool dontMove;
    public Transform start;
    public Transform end;
    public float speed = 0.5f;
    public float angularSpeed = 300.0f;

	void Awake ()
	{
	    dontMove = start == null || end == null || end.localPosition == start.localPosition;
	    if (!dontMove)
	    {
	        t = (transform.localPosition - start.localPosition).sqrMagnitude / (end.localPosition - start.localPosition).sqrMagnitude;
        }
	}

	void Update ()
	{
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * angularSpeed));

        if (!dontMove)
        {
            transform.localPosition = Vector3.Lerp(start.localPosition, end.localPosition, t);

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            var character = other.GetComponent<Character>();
            character.TakeDamage(damage);
        }
    }
}
