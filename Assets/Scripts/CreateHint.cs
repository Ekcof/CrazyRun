using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateHint : MonoBehaviour
{
    [SerializeField] private float height;
    private Vector3 UIpos;
    private GameObject сanvas1;
    private Camera сam;
    private string hintText;
    private Text text;

    public string HintText
    {
        set => hintText = value;
    }

    private void Start()
    {
        сanvas1 = transform.parent.gameObject;
    }

    private void Awake()
    {
        сam = Camera.main;
        UIpos = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        text = transform.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        transform.position = сam.WorldToScreenPoint(UIpos);
        transform.rotation = Quaternion.Euler(0.0f, сanvas1.transform.rotation.eulerAngles.y, сanvas1.transform.rotation.eulerAngles.z);
        text.text = hintText;
    }
}
