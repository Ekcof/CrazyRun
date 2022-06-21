using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransparent : MonoBehaviour
{

    private Material[] oldMaterials = null;

    public float targetTransparency { get; set; }

    public Material transparentMaterial { get; set; }

    private bool shouldBeTransparent = true;

    private Material[] materialsList;

    public void BeTransparent()
    {
        shouldBeTransparent = true;
    }

    private void Start()
    {

        if (oldMaterials == null)
        {
            oldMaterials = GetComponent<Renderer>().materials;

            materialsList = new Material[oldMaterials.Length];

            for (int i = 0; i < materialsList.Length; i++)
            {
                materialsList[i] = Object.Instantiate(transparentMaterial);
                materialsList[i].SetColor("_Color", oldMaterials[i].GetColor("_Color"));
            }

            GetComponent<Renderer>().materials = materialsList;
        }
    }

    private void Update()
    {
        if (!shouldBeTransparent)
        {
            Destroy(this);
        }

        for (int i = 0; i < materialsList.Length; i++)
        {
            Color color1 = oldMaterials[i].GetColor("_Color");

            color1.a = targetTransparency;
            materialsList[i].color = color1;
        }

        shouldBeTransparent = false;
    }

    private void OnDestroy()
    {
        GetComponent<Renderer>().materials = oldMaterials;
    }
}