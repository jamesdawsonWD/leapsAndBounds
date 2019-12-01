using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    // TODO: Need classes to delegate these things to
    private bool alive;
    private MeshRenderer meshRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        animator = GetComponent<Animator>();
        alive = true;
    }

    protected override void ComputeVelocity()
    {
        //TODO: Move elsewhere
        if (!alive)
        {
            return;
        }

        //TODO: Refactor into PlayerActionController
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            animator.SetBool("jump", true);
            velocity.y = jumpTakeOffSpeed;
            
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
            animator.SetBool("jump", false);
        }


        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DeathZone"))
        {
            Debug.Log("yup");
            PlayerDeath();
        }
    }


    public void PlayerDeath()
    {
        Debug.Log("aye");

        FindObjectOfType<GameManager>().EndGame();
        alive = true;
        animator.SetFloat("Health", 100f);
    }



}