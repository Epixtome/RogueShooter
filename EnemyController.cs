using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    public Animator anim;
    public int health = 150;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public SpriteRenderer theBody;
    public float shootRange;
    public bool shouldRunAway;
    public float runAwayRange;
    public bool shouldChasePlayer;
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;
    public bool shouldDropItems;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    void Start()
    {
        if(shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {

            moveDirection = Vector3.zero;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer) //where is the player and mob, and are they in range
            {
                moveDirection = PlayerController.instance.transform.position - transform.position; // if yes then add movedirection var
            }else
            {
                if(shouldWander)
                {
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        //move mob
                        moveDirection = wanderDirection;

                        if(wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                        }
                    }

                    if(pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;

                        if(pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                        }
                    }
                }

                if(shouldPatrol)
                {
                    moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                    if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f)
                    {
                        currentPatrolPoint++;
                        if(currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }
                    }
                }
            }

            if(shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runAwayRange)
            {
                moveDirection = transform.position - PlayerController.instance.transform.position;
            }


           /* else
            {
                moveDirection = Vector3.zero;
            }
           */

            moveDirection.Normalize();
            rb.velocity = moveDirection * moveSpeed;

          

            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(13);

                }
            }


        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }


    public void DamageEnemy(int damage)
    {
        health -= damage;
        AudioManager.instance.PlaySFX(2);
        Instantiate(hitEffect, transform.position, transform.rotation);

        if(health <= 0)
        { 
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(1);
            int selectedSplatters = Random.Range(0, deathSplatters.Length);
            int rotation = Random.Range(0, 3);
            Instantiate(deathSplatters[selectedSplatters], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
            if (shouldDropItems)
            {
                float dropChance = Random.Range(0f, 100f);

                if (dropChance <= itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);

                }
            }
        }
    }

}
