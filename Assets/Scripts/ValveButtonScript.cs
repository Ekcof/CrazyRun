using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nameofthegame.Inputs
{
    public class ValveButtonScript : MonoBehaviour
{
        [SerializeField] private GameObject button;
        [SerializeField] private GameObject gas;
        [SerializeField] private GameObject UIprefab;
        [SerializeField] private GameObject сanvas1;
        private Animator buttonAnimator;

        private GameObject newHint; 
        private bool isNear;
        private ParticleSystem party;
        private CreateHint createHint;
        private GameObject deadSpace;

        private void Start()
        {
            buttonAnimator = button.GetComponent<Animator>();
            party = gas.GetComponent<ParticleSystem>();
            deadSpace = gas.transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            if (isNear)
            {
                IfPlayerIsNear();
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("player"))
            {
                CreateNewHint();
                isNear = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("player"))
            {
                if (newHint) { Destroy(newHint); };
                isNear = false;
            }
        }

        private void IfPlayerIsNear()
        {
            if (Input.GetButtonDown("Submit"))
            {
                if (!buttonAnimator.GetBool("Pressed"))
                {
                    buttonAnimator.SetBool("Pressed", true);
                    party.Stop();
                    deadSpace.SetActive(false);
                }
                else
                {
                    buttonAnimator.SetBool("Pressed", false);
                    party.Play();
                    deadSpace.SetActive(true);
                }
            }
        }

        private void CreateNewHint()
        {
            newHint = Instantiate(UIprefab, transform.position, transform.rotation);
            newHint.transform.SetParent(сanvas1.transform);
            createHint = newHint.GetComponent<CreateHint>();
            createHint.HintText = "Press Space";

        }
    }
}
