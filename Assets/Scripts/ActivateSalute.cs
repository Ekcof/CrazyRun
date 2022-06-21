using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSalute : MonoBehaviour
{
    [SerializeField] private GameObject[] salutes;
    private SaluteScript saluteScript;

    private void Awake()
    {
        if (salutes.Length > 0)
        {
            foreach (GameObject salute in salutes)
            {
                saluteScript = salute.GetComponent<SaluteScript>();
                saluteScript.SaluteOff();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject salute in salutes)
        {
            saluteScript = salute.GetComponent<SaluteScript>();
            saluteScript.SaluteOn();
        }
    }

}
