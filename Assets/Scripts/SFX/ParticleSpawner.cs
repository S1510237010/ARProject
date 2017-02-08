using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles the creation and removal of particle systems.
/// </summary>
public class ParticleSpawner : MonoBehaviour
{
	[Tooltip("Add the particle systems prefabs you want to use here.")]
	public GameObject[] ParticleSystems;

    private static ParticleSpawner instance;
    public static ParticleSpawner Instance
    {
        get { return instance; }
    }

    void Start()
    {
        instance = this;
    }

	/// <summary>
	/// Spawns the particle system at a given position.
	/// </summary>
	/// <returns>The particle system.</returns>
	/// <param name="particleIndex">The index of the particle system that will be instantiated.</param>
	/// <param name="particlePosition">The position where it will be placed.</param>
    public int SpawnParticleSystem(int particleIndex, Transform particlePosition)
    {
        // Instantiate the new Particle System
        GameObject newParticleSystem = GameObject.Instantiate(ParticleSystems[particleIndex]);
        newParticleSystem.transform.position = particlePosition.position;

        //Starts a new Coroutine to destroy the particle system once it's duration is over
        int duration = (int)Math.Ceiling(newParticleSystem.GetComponent<ParticleSystem>().main.duration);
        StartCoroutine(DestroyParticleSystem(newParticleSystem, duration));
        return duration;
    }

	/// <summary>
	/// Destroys the particle system after a given time
	/// </summary>
	/// <param name="pSystem">The particle system that should be destroyed.</param>
	/// <param name="t">The time in seconds it should wait before destroying the object.</param>
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
