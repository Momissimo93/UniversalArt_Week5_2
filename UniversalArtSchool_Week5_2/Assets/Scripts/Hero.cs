using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    [SerializeField] int jumpForce;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    private bool isFiring;
    private float speedLocalReference;

    private void Awake()
    {
        GetAnimator();
        GetBoxCollider();
    }
    void Start()
    {
        facingForward = true;
        speedLocalReference = speed;
    }

    void Update()
    {
        checkIfButtonDownPressed();
        EmittingRaysFromFeet();
    }

    private void FixedUpdate()
    {
        checkIfButtonPressed();
    }

    void checkIfButtonPressed()
    {
        if (Input.GetButton("Horizontal") && (!isFiring))
        {
            speed = speedLocalReference;
            SetHeroRotation();
            direction = Input.GetAxis("Horizontal");
            animator.SetFloat("speed", speed);

            Move();
        }
        else
        {
            animator.SetFloat("speed", 0);
            StopMoving();
        }
    }

    void checkIfButtonDownPressed()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("fire");
            isFiring = true;
            Shoot(bullet, firePoint);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
        }

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            animator.SetTrigger("jump");
            Jump(jumpForce);
        }
    }
    void SetHeroRotation()
    {
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            if (facingForward)
            {
                transform.Rotate(0f, 180f, 0f);
                facingForward = false;
            }
        }
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (!facingForward)
            {
                transform.Rotate(0f, 180f, 0f);
                facingForward = true;
            }
        }
    }

    void Shoot(GameObject b, Transform spwaningBulletPosition)
    {
        GameObject bullet = Instantiate(b, spwaningBulletPosition.position, spwaningBulletPosition.rotation);
    }
}
