using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Public Variables
    public float runSpeed, jumpPower, jumpBoost, playerVelocityY;
    public bool extraJump;
    public Vector3 jumpTouch;
    public Transform groundChecker;
    public LayerMask groundLayer;

    // Private Variables
    private float moveInput;
    private Rigidbody2D charBody;
    private bool facingRight = true;
    private Animator anim;

    // Base Functions
    void Awake()
    {
        charBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        groundDetectJump();
        playerVelocityY = charBody.velocity.y;
    }

    // Defines Player Movement
    void Movement()
    {
        moveInput = Input.GetAxis("Horizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetFloat("VelocityY", charBody.velocity.y);

        charBody.velocity = new Vector2(moveInput, charBody.velocity.y);

        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
        {
            CharacterFlip();
        }
    }

    // Jump Actions
    void groundDetectJump()
    {
        Collider2D charTouchGround = Physics2D.OverlapBox(groundChecker.position, jumpTouch, 0, groundLayer);

        if (charTouchGround != null)
        {
            extraJump = false;

            if (Input.GetButton("Jump") && charTouchGround.gameObject.tag == "Ground")
            {
                charBody.velocity = new Vector2(charBody.velocity.x, 0);
                charBody.AddForce(new Vector2(0, jumpPower));
                extraJump = true;
            }

            else
            {
                anim.SetBool("Jump", false);
            }
        }
        if (charTouchGround == null) {
            doubleJump();
        }
    }

    void doubleJump()
    {
        if (extraJump == true && Input.GetButtonDown("Jump"))
        {
            charBody.velocity = new Vector2(charBody.velocity.x, 0);
            charBody.AddForce(new Vector2(0, jumpBoost));
            extraJump = false;
        }
    }

    // Adds Visual Hitbox for Floor
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
