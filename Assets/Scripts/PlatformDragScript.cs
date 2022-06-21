using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDragScript : MonoBehaviour
{
    private GameObject player1;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            player1 = other.transform.gameObject;
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (player1 == other.gameObject)
        {
            other.transform.parent = null;
        }
    }
}
