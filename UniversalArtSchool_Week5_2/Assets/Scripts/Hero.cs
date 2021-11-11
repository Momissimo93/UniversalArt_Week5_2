using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    [SerializeField] int jumpForce;
    private bool facingForward = true;
    private float speedLocalReference;
    private bool isFiring;

    int ground = 1 << 6;
    BoxCollider2D boxCollider2D;

    protected Vector2 leftFoot;
    protected Vector2 rightFoot;

    RaycastHit2D rayCastFromLeftFoot;

    private void Awake()
    {
        if (GetComponent<BoxCollider2D>())
        {
            boxCollider2D = gameObject.GetComponent<BoxCollider2D>();

            leftFoot = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);
            rightFoot = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y);
            rayCastFromLeftFoot = Physics2D.Raycast(leftFoot, Vector2.down, 0.09f, ground);
            Debug.Log(leftFoot);
        }
    }
    void Start()
    {
        if (GetComponent<BoxCollider2D>())
        {
            boxCollider2D = gameObject.GetComponent<BoxCollider2D>();

            leftFoot = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);
            rightFoot = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y);
            Debug.Log(leftFoot);
        }
        if (GetComponent<Animator>())
        {
            animator = gameObject.GetComponent<Animator>();
            Debug.Log("animator");
        }

        speedLocalReference = speed;
    }

    // Update is called once per frame
    void Update()
    {
        rayCastFromLeftFoot = Physics2D.Raycast(leftFoot, Vector2.down, 0.09f, ground);
        leftFoot = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);
        Debug.DrawRay(leftFoot, Vector2.down * 10f, Color.blue);
        checkIfButtonDownPressed();
        EmittingRays();
        if (rayCastFromLeftFoot)
        {
            Debug.Log( " Left Foot is on the Ground");
        }

    }

    private void FixedUpdate()
    {
        checkIfButtonPressed();
        Debug.DrawRay(leftFoot, Vector2.down * 10f, Color.blue);
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
            Debug.Log("Fire");
            animator.SetTrigger("fire");
            isFiring = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            // && ((feet.leftOnGround) || feet.rightOnGround)
            Debug.Log("Jump");
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
        //RaycastHit2D leftFootRays = Physics2D.Raycast(leftFoot, Vector2.down, 3f, ground);
        Debug.DrawRay(leftFoot, Vector2.down * 10f, Color.blue);
        Debug.Log("EmittingRays");
    }
}
