using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] private float gravity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool IsRightSidePlayer;

   private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void Update()
    {
        if(IsRightSidePlayer)
        {
            UpdateRightPlayer();
        }
        else
        {
            UpdateLeftPlayer();
        }
    }

    private void UpdateRightPlayer()
    {
        if (isGrounded != true)
        {
            rb.AddForce(new Vector2(-gravity, 0));
        }

        float verticalMove = Input.GetAxisRaw("Vertical(P1)") * moveSpeed;
        rb.velocity = new Vector2(rb.velocity.x, verticalMove);

        if (Input.GetKeyDown("d") && isGrounded == true)
        {
            rb.velocity = new Vector2(+jumpForce, rb.velocity.y);
        }
    }
    private void UpdateLeftPlayer()
    {
        if (isGrounded != true)
        {
            rb.AddForce(new Vector2(gravity, 0));
        }

        float verticalMove = Input.GetAxisRaw("Vertical(P2)") * moveSpeed;
        rb.velocity = new Vector2(rb.velocity.x, verticalMove);

        if (Input.GetKeyDown("left") && isGrounded == true)
        {
            rb.velocity = new Vector2(-jumpForce, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platforms")
        {
            isGrounded = true;
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
