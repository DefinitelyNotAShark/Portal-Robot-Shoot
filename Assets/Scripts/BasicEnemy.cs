﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IFightable
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float enemyCoolDownLength;

    [SerializeField]
    [Tooltip("The minimum distance an enemy can be to the player as a float.")]
    private float minRange;

    private GameObject player;
    private ParticleSystem particles;
    private MeshRenderer mesh;
    private BoxCollider collider;

    private bool enemyIsDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//get the player
        particles = GetComponentInChildren<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    public void SwordHit()
    {
        if(!enemyIsDead)
        StartCoroutine(DestroyEnemyCooldown());//death cooldown
    }

    private void Update()
    {
        if(!EnemyIsAtDestination())
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed *Time.deltaTime);//move in the direction of the player at specified speed
    }

    private bool EnemyIsAtDestination()//if we've arrived at where we're going, we don't need to keep moving.
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);//the distance between us and the enemy

        if (distance < minRange)
            return true;

        else return false;
    }

    IEnumerator DestroyEnemyCooldown()
    {   
        //AUDIO play enemy hit sound
        enemyIsDead = true;//so that we don't start the coroutine more than once
        particles.Play();//give us some death particles!
        mesh.enabled = false;//invisible
        collider.enabled = false;//can't hit again
        yield return new WaitForSeconds(enemyCoolDownLength);
        Destroy(this.gameObject);//DIE
    }
}
