using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rigidBody;
    [HideInInspector]
    public Animator animator;
    public float moveSpeed;
    Vector2 moveDir = new Vector2();
    Vector2 lastMoveDir;
    private Vector3 boundary1;
    private Vector3 boundary2;
    //creating instance to allow refrence from other scripts
    public static PlayerController instance;
    public bool canMove = true;
    
    public void SetBounds(Vector3 bound1, Vector3 bound2)
    {
        boundary1 = bound1 + new Vector3(.5f, 1f, 0f);
        boundary2 = bound2 + new Vector3(-.5f, -1f, 0f);
    }
   
   void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (instance == null)
        {
            instance = this;
        } else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveCharacter()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        bool isIdle = moveDir.x == 0 && moveDir.y == 0;

        if (!canMove)
        {
            isIdle = true;
        }

        

        if (isIdle)
        {
            rigidBody.velocity = Vector2.zero;
            animator.SetBool("IsWalking", false);
        }
        else
        {
            lastMoveDir = moveDir;
            rigidBody.velocity = moveDir * moveSpeed;
            animator.SetFloat("MoveX", moveDir.x);
            animator.SetFloat("MoveY", moveDir.y);
            animator.SetBool("IsWalking", true);
        }

    }

        void FixedUpdate() 
    {
        MoveCharacter();
    }

   
}
