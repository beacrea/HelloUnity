using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Public Variables
    public float runSpeed, jumpPower;
    [Range(0, 1)]
    public float jumpBoost;
    public Vector3 jumpTouch;
    public Transform groundChecker;
    public LayerMask groundLayer;

    // Private Variables
    private float moveInput;
    private Rigidbody2D charBody;
    private bool facingRight = true;
    private bool doubleJumped;
    private Animator anim;

    // Base Functions
    void Awake()
    {
        charBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        doubleJumped = false;
    }

    void Update()
    {
        Movement();
        groundDetectJump();
    }

    // Defines Player Movement
    void Movement()
    {
        moveInput = Input.GetAxis("Horizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        charBody.velocity = new Vector2(moveInput, charBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && doubleJumped == false)
        {
            if (charBody.velocity.y > 0)
            {
                charBody.velocity = new Vector2(charBody.velocity.x, (jumpPower * jumpBoost) + jumpPower);
                print("Double Jumped");
                doubleJumped = true;
            }
        }

        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
        {
            CharacterFlip();
        }
    }

    // Check Jump
    void groundDetectJump()
    {
        Collider2D charTouchGround = Physics2D.OverlapBox(groundChecker.position, jumpTouch, 0, groundLayer);

        if (charTouchGround != null)
        {
            doubleJumped = false;
            if (charTouchGround.gameObject.tag == "Ground" && Input.GetKeyDown(KeyCode.Space))
            {
                charBody.velocity = new Vector2(charBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(groundChecker.position, jumpTouch);
    }

    // Changes Character Sprite Reflection on Movement
    void CharacterFlip()
    {
        facingRight = !facingRight;

        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }
}
