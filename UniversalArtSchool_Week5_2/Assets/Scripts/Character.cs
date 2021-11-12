using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float direction;
    [SerializeField] protected int lifePoint;
    [SerializeField] protected float rayLenghtFromFeet;

    public enum MovementType {PatrollingCheckingGround, HeroMovement};
    [SerializeField] protected MovementType movementType;

    protected Animator animator;
    protected BoxCollider2D boxCollider2D;
    protected Vector2 leftFoot;
    protected Vector2 rightFoot;
    protected bool isOnGround;
    protected bool facingForward;
    protected int ground = 1 << 6;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void GetAnimator()
    {
        if (GetComponent<Animator>())
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }

    protected void GetBoxCollider()
    {
        if (GetComponent<BoxCollider2D>())
        {
            boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        }
    }

    public void Move()
    {
        switch (movementType)
        {
            case MovementType.PatrollingCheckingGround:
                PatrollingCheckingGround();
                break;

            case MovementType.HeroMovement:
                HeroMovement();
                break;

            default: break;
        }
    }
    public void PatrollingCheckingGround()
    {
        transform.position = new Vector2(transform.position.x + (speed * direction * Time.deltaTime), transform.position.y);
    }
    public void HeroMovement()
    {
        Rigidbody2D rb;

        if(GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }
    }

    private protected void StopMoving()
    {
        Rigidbody2D rb;

        if (GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x * 0, rb.velocity.y);
        }
    }

    private protected void Jump(int jF)
    {
        int jumpForce = jF;
        Rigidbody2D rb;
        if (GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void TakeDamage(int damage)
    {
        lifePoint -= damage;
        if (lifePoint <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    protected void EmittingRaysFromFeet()
    {
        leftFoot = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);
        rightFoot = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y);

        RaycastHit2D leftFootRays = Physics2D.Raycast(leftFoot, Vector2.down, rayLenghtFromFeet, ground);
        RaycastHit2D rightFootRays = Physics2D.Raycast(rightFoot, Vector2.down, rayLenghtFromFeet, ground);

        CheckIfGround(leftFootRays, rightFootRays);
        DrawRaysFromFeet();
    }

    private void CheckIfGround(RaycastHit2D lFRays, RaycastHit2D rFRays)
    {

        if ((!lFRays) || (!rFRays))
        {
            isOnGround = false;
           
            if (gameObject.name == "Flying_Eye")
            {
                transform.Rotate(0f, 180f, 0f);
                direction = direction * -1;
            }
            //direction *= direction - 1;
        }
        else
            isOnGround = true;
        Debug.Log("isOnGround" + isOnGround);

    }
    private void DrawRaysFromFeet()
    {
        Debug.DrawRay(leftFoot, Vector2.down * rayLenghtFromFeet, Color.blue);
        Debug.DrawRay(rightFoot, Vector2.down * rayLenghtFromFeet, Color.blue);
    }
}
