using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    public float movementSpeed;
    private Rigidbody2D playerRb;

    private Vector3 moveInputDirection;
    private Vector2 moveVelocity;
    private Vector2 aimInputDirection;
    private Vector2 bulletDirection;

    public GameObject aimingPoint;
    public GameObject gun;
    public GameObject bulletPrefab;
    public GameObject dashTrail;
    private Animator playerAnimator;

    public int dashCompt;
    public float dashTime;
    public float startDashTime;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        moveInputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        moveVelocity = moveInputDirection * movementSpeed;

        playerAnimator.SetFloat("Horizontal", moveInputDirection.x);
        playerAnimator.SetFloat("Vertical", moveInputDirection.y);

        aimInputDirection = new Vector2(Input.GetAxisRaw("HorizontalSecondJoystick"),Input.GetAxisRaw("VerticalSecondJoystick"));
        bulletDirection = new Vector2(Input.GetAxisRaw("HorizontalSecondJoystick"), Input.GetAxisRaw("VerticalSecondJoystick"));
        
        AimAndShoot();
        Dash();
   
        if (Input.GetButtonDown("Fire"))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * 20f;
            bullet.transform.Rotate(0.0f,0.0f,Mathf.Atan2(bulletDirection.y,bulletDirection.x) * Mathf.Rad2Deg);
            Destroy(bullet, 2.0f);
        }
 
       

    }

    private void FixedUpdate()
    {
        playerRb.velocity = moveVelocity;
        dashCompt = dashCompt + 1;
    }


    void AimAndShoot()
    {
        bulletDirection.Normalize();
        if (aimInputDirection.magnitude > 0.0f)
        {

            aimInputDirection.Normalize();
            aimingPoint.transform.localPosition = aimInputDirection;
            aimingPoint.SetActive(true);
        }
        else
        {
            aimingPoint.SetActive(false);
        }
      
    }
    private void Dash()
    {
        {
            if (Input.GetButtonDown("Dash"))
            {
                float dashDistance = 2f;
                transform.position += moveInputDirection * dashDistance;
                dashTrail.SetActive(true);
                dashCompt = 0;
            }
            else if(dashCompt >= 25)
            {
                dashTrail.SetActive(false);
            }
        }
    }

}
