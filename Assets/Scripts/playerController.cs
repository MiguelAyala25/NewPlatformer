using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;



    [SerializeField] private float gravity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool IsRightSidePlayer;

    private bool jumpPressed = false;
    private float verticalMove = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        GameManager.Instance.RegisterPlayer(this.gameObject,originalColor);
        //display shield visual
        if (this.gameObject == GameManager.Instance.playerWithShield)
        {
            spriteRenderer.color = Color.blue;
        }

    }

    void Update()   
    {
        //move input
        if (IsRightSidePlayer)
        {
            verticalMove = Input.GetAxisRaw("Vertical(P1)") * moveSpeed;
        }
        else
        {
            verticalMove = Input.GetAxisRaw("Vertical(P2)") * moveSpeed;
        }

        //animations and flip
        animator.SetInteger("speed", (int)verticalMove);
        if(verticalMove < 0f)
        {
         spriteRenderer.flipX = false;
        }
        else if(verticalMove > 0f)
        {
            spriteRenderer.flipX = true;
        }
        //jump input
        if ((IsRightSidePlayer && Input.GetKeyDown("d")) || (!IsRightSidePlayer && Input.GetKeyDown("left")))
        {
            jumpPressed = true;
        }

        //shield
        if (GameManager.Instance.playerWithShield == this.gameObject)
        {
            // if right
            if (IsRightSidePlayer && Input.GetKeyDown(KeyCode.A))
            {
                GameManager.Instance.TransferShield(this.gameObject);
            }
            // if left player
            else if (!IsRightSidePlayer && Input.GetKeyDown(KeyCode.RightArrow))
            {
                GameManager.Instance.TransferShield(this.gameObject);
            }
        }


    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.AddForce(new Vector2(IsRightSidePlayer ? -gravity : gravity, 0));
        }

        rb.velocity = new Vector2(rb.velocity.x, verticalMove);

        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(IsRightSidePlayer ? jumpForce : -jumpForce, rb.velocity.y);
            animator.SetBool("IsJumping", true);
            jumpPressed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platforms")
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platforms")
        {
            isGrounded = false;
        }
    }
}