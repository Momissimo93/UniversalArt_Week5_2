using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float direction;
    [SerializeField] protected int lifePoint;

    public enum MovementType {PatrollingCheckingGround, HeroMovement};
    [SerializeField] protected MovementType movementType;

    protected Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log("StopMoving");
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
}
