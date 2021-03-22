using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float bulletSpeed = 7.5f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    private int damageToGive = 35;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //you will get the player to initialize the bullet, not the bullet itself. so put that code on the PlayerController
        rb.velocity = transform.right * bulletSpeed; // add velocity using rigidbody and make it go right
        //initialize when mouse button pressed
        //set bullet to firepoint
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(4);
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
