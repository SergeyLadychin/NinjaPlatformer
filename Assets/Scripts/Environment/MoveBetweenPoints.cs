using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    private float fraction;
    private int multiplier;

    public Transform start;
    public Transform end;
    public float speed = 0.5f;
    public bool moveCircular;

    public bool stopOnEvent;
    public string eventName;

    void OnEnable()
    {
        if (stopOnEvent)
        {
            EventManager.StartListen(eventName, StopMoving);
        }
    }

    void OnDisable()
    {
        if (stopOnEvent)
        {
            EventManager.StopListen(eventName, StopMoving);
        }
    }

    void Awake()
    {
        multiplier = 1;
        fraction = (transform.position - start.position).sqrMagnitude / (end.position - start.position).sqrMagnitude;
        StartCoroutine("Move");
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(start.position, end.position, fraction);
            fraction += multiplier * Time.deltaTime * speed;
            if (fraction > 1.0f || fraction < 0.0f)
            {
                if (moveCircular)
                {
                    fraction = multiplier == 1 ? 1.0f : 0.0f;
                    multiplier *= -1;
                }
                else
                {
                    StopCoroutine("Move");
                }
            }
            yield return null;
        }
    }

    private void StopMoving()
    {
        StopCoroutine("Move");
    }
}
