using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    [SerializeField] int jumpForce;
    [SerializeField] float raylenght;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    private bool facingForward = true;
    private bool isFiring;
    private bool isOnGround;
    private float speedLocalReference;

    int ground = 1 << 6;
    BoxCollider2D boxCollider2D;

    protected Vector2 leftFoot;
    protected Vector2 rightFoot;

    private void Awake()
    {
        if (GetComponent<BoxCollider2D>())
        {
            boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        }
    }
    void Start()
    {
        if (GetComponent<Animator>())
        {
            animator = gameObject.GetComponent<Animator>();
            Debug.Log("animator");
        }

        speedLocalReference = speed;
    }

    void Update()
    {
        checkIfButtonDownPressed();
        EmittingRays();
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
    void EmittingRays()
    {
        leftFoot = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);
        rightFoot = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y);

        RaycastHit2D leftFootRays = Physics2D.Raycast(leftFoot, Vector2.down, raylenght, ground);
        RaycastHit2D rightFootRays = Physics2D.Raycast(rightFoot, Vector2.down, raylenght, ground);

        checkIfGround(leftFootRays, rightFootRays);
        DrawRay();
    }

    void checkIfGround(RaycastHit2D lFRays, RaycastHit2D rFRays)
    {
        if ((lFRays) || (rFRays))
        {
            isOnGround = true;
        }
        else
            isOnGround = false;
    }

    void DrawRay()
    {
        Debug.DrawRay(leftFoot, Vector2.down * 1f, Color.blue);
        Debug.DrawRay(rightFoot, Vector2.down * 1f, Color.blue);
    }

    void Shoot(GameObject b, Transform spwaningBulletPosition)
    {
        GameObject bullet = Instantiate(b, spwaningBulletPosition.position, spwaningBulletPosition.rotation);
    }
}
