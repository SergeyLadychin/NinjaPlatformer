using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    private IPickable pickObject;
    private bool pickedUp;

    public string targetTag;
    public GameObject pickUpCounter;

    void Awake()
    {
        pickObject = pickUpCounter.GetComponent<IPickable>();
        if (pickObject == null)
        {
            Debug.LogError("Pick Up Counter object does not implement IPickable interface.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) && !pickedUp)
        {
            pickObject.Add();
            pickedUp = true;
            Destroy(this.gameObject);
        }
    }
}
