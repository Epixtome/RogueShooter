using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; //this makes PlayerController available outside this file
    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D rb;
    public Transform gunArm;
    private Camera theCam; //added this variable so i can set it in the start method
    public Animator anim;
    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;
    public SpriteRenderer bodySR;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f;
    private float activeMovementSpeed;
    [HideInInspector]
    public float dashCounter, dashCoolCounter;
    [HideInInspector]
    public bool canMove = true;


    private void Awake()
    {
        instance = this;// this choose the controller; there is only one player
    }
    void Start()
    {
        theCam = Camera.main; //now that theCam is set i can replace searching for the camera in the update. adding it here does it once, instead of every frame
        activeMovementSpeed = moveSpeed;
    }

    void Update()
    {

        if (canMove && !LevelManager.instance.isPaused)
        {
            #region movement
            moveInput.x = Input.GetAxisRaw("Horizontal"); //class.method(statement)
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            // REMOVE CODE transform.position += new Vector3(moveInput.x * moveSpeed * Time.deltaTime, moveInput.y * moveSpeed * Time.deltaTime, 0f); 

            #region move animation
            if (moveInput != Vector2.zero)
            /* here you wanted to use moveinput.x and y with an OR statement as check if it was 0. this was the right logic but you need to use vector 2 instead of checking each value. check the whole input
             not each arguement of the input inndividually*/
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            #endregion

            /* notes
            need to set the whole thing at once //do the plus/equals to make it equal too and add each iteration
            can i create a variable for the movespeed and deltatime?
            we will use rb velocity for movement instead of the transform. this is because transform forces objects into each other and they bounce with pressure
            */
            rb.velocity = moveInput * activeMovementSpeed;

            Vector3 mousePos = Input.mousePosition;
            // the mouse position is got during the update then destroyed after the update.
            Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition); // this gets the position of the player on the screen. the arguement is the player WTSP is the method.

            #endregion

            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); //every time you want to edit a variable, create new
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one; // this is the same as making all values a 1
                gunArm.localScale = Vector3.one;
            }

            //rotate gunarm

            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y); //always need to create new variables and fill them for each thing you want to affect
                                                                                                  //above getting the difference between the mouse and screen, and calling it offset.
                                                                                                  // going to take gunarm and divide the offset into the rotation.
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; //this is the angle between the two points

            // now set the Z on the gunarm transform

            gunArm.rotation = Quaternion.Euler(0, 0, angle);
            //you're setting the QE to a vector 3 then adding the angle as the Z

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
                AudioManager.instance.PlaySFX(12);
            }

            if (Input.GetMouseButton(0))
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(12);

                    shotCounter = timeBetweenShots;

                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMovementSpeed = dashSpeed;
                    dashCounter = dashLength;
                    anim.SetTrigger("isRoll");
                    PlayerHealthController.instance.MakeInvincible(dashInvincibility);
                    AudioManager.instance.PlaySFX(8);
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMovementSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }

    

  
}
