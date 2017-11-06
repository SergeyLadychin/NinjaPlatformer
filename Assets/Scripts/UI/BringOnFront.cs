using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringOnFront : MonoBehaviour
{
    void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}
