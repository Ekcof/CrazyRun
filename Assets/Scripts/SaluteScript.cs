using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaluteScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private GameObject[] lights;


    public void SaluteOn()
    {
        int countParticleSystems = particleSystems.Length;
        int countLights = lights.Length;

        if (countParticleSystems > 0)
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Play();
            }
        }
        if (countLights > 0)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }
        }
    }

    public void SaluteOff()
    {
        int countParticleSystems = particleSystems.Length;
        int countLights = lights.Length;

        if (countParticleSystems > 0)
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Stop();
            }
        }
        if (countLights > 0)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
        }
    }
}
