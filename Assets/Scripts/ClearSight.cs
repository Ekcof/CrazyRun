using UnityEngine;
using System.Collections;

public class ClearSight : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float distanceToPlayer = 3.0f;
    public Material transparentMaterial = null;
    public float targetTransparency = 0.3f;

    private void Start()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
    }
    private void Update()
    {
        RaycastHit[] hits;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position) - 0.5f;
        hits = Physics.RaycastAll(transform.position, transform.forward, distanceToPlayer);
        if (hits.Length > 0)
        {
            GetHits(hits);
        }
    }
    private void GetHits(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            Renderer R = hit.collider.GetComponent<Renderer>();
            if (R == null)
            {
                continue;
            }

            AutoTransparent AT = R.GetComponent<AutoTransparent>();
            if (AT == null)
            {
                AT = R.gameObject.AddComponent<AutoTransparent>();
                AT.transparentMaterial = transparentMaterial;
                AT.targetTransparency = targetTransparency;
            }
            AT.BeTransparent();
        }
    }
}