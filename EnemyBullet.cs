using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float bulletSpeed;
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);
            PlayerHealthController.instance.DamagePlayer(); //calling the damanageplayer function, which has the health damage ammount

        }

        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
