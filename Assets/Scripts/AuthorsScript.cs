using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthorsScript : MonoBehaviour
{
    [SerializeField] private Text dynamicText;
    private string initialText;
    public float delay = 0.02f;
    private Transform dialogPanel;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    private void Awake()
    {
        initialText = dynamicText.text;
        coroutine = TypeText(delay);
    }

    public void ActivateAuthorsMenu()
    {
        StartCoroutine(coroutine);
    }

    IEnumerator TypeText(float delayPerLetter)
    {
        for (int i = 0; i < initialText.Length + 1; i++)
        {
            string messageCurrent = initialText.Substring(0, i);
            dynamicText.text = messageCurrent;
            yield return new WaitForSeconds(delayPerLetter);
            if (dynamicText.text.Length < messageCurrent.Length - 1) { break; };
        }
    }
}
