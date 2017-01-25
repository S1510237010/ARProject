using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    private static ParticleSpawner instance;
    public static ParticleSpawner Instance
    {
        get { return instance; }
    }

    void Start()
    {
        instance = this;
    }

    public GameObject[] ParticleSystems;

    public void SpawnParticleSystem(int particleIndex, Transform particlePosition)
    {
        // Instantiate the new Particle System
        GameObject newParticleSystem = GameObject.Instantiate(ParticleSystems[particleIndex]);
        newParticleSystem.transform.position = particlePosition.position;

        //Starts a new Coroutine to destroy the particle system once it's duration is over
        int duration = (int)newParticleSystem.GetComponent<ParticleSystem>().main.duration;
        StartCoroutine(DestroyParticleSystem(newParticleSystem, duration));
    }

    IEnumerator DestroyParticleSystem(GameObject pSystem, int t)
    {
        //Waits for the specified time and continues afterwards
        yield return new WaitForSeconds(t);

        if (pSystem != null)
        {
            GameObject.Destroy(pSystem);
        }
    }
}
