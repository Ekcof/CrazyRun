using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillerScript : MonoBehaviour
{
    [SerializeField] private GameObject UISystem;
    [SerializeField] private GameObject prefab;
    private LevelManager levelManager;
    private Animator animator;
    private GameObject newPrefab;

    private void Start()
    {
        levelManager = UISystem.GetComponent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelManager.DeadMenu();
            newPrefab = Instantiate(prefab, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            //newPrefab.transform.SetParent(other.transform, true);
        }
    }
}
