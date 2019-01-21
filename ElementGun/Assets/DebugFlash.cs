using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFlash : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] shootParticles;

    [SerializeField]
    private float coolDownTime;

    private bool coroutineStarted = false;

	// Use this for initialization
	void Start ()
    {
        shootParticles = GetComponentsInChildren<ParticleSystem>();//get all our particle system
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (!coroutineStarted)
                StartCoroutine(Shoot());
        }
	}

    private IEnumerator Shoot()
    {
        coroutineStarted = true;
        foreach(ParticleSystem p in shootParticles)
        {
            p.Play();
        }

        yield return new WaitForSeconds(coolDownTime);
        coroutineStarted = false;
    }
}
