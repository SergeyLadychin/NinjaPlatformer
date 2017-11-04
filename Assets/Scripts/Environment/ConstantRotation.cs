using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public float angularSpeed = 300.0f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * angularSpeed));
    }
}
