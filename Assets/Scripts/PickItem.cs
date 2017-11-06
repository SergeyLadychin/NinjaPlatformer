using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    private bool pickedUp;

    public PickUpParameters[] parameters;

    void Awake()
    {
        for (int i = 0; i < parameters.Length; i++)
        {
            parameters[i].Init();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!pickedUp)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (other.CompareTag(parameters[i].targetTag))
                {
                    parameters[i].Add();
                    pickedUp = true;
                }
            }
            if (pickedUp)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

[Serializable]
public class PickUpParameters
{
    private IPickable pickObject;

    public string targetTag;
    public GameObject pickUpCounter;

    public void Add()
    {
        pickObject.Add();
    }

    public void Init()
    {
        pickObject = pickUpCounter.GetComponent<IPickable>();
        if (pickObject == null)
        {
            Debug.LogError("Pick Up Counter object does not implement IPickable interface.");
        }
    }
}
