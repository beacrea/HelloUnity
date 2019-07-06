using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Public Variables
    public float runSpeed, jumpPower, jumpHeight;
    public Vector3 jumpTouch;
    public Transform groundChecker;
    public LayerMask groundLayer;

    // Private Variables
    private float moveInput;
    private Rigidbody2D charBody;
    private bool facingRight = true;

    // Base Functions
    void Awake()
    {
        charBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        groundDetectJump();
    }

    // Defines Player Movement
    void Movement()
    {
        moveInput = Input.GetAxisRaw("Horizontal") * runSpeed;

        charBody.velocity = new Vector2(moveInput, charBody.velocity.y);

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
            if (charTouchGround.gameObject.tag == "Ground" && Input.GetKeyDown(KeyCode.Space))
            {
                charBody.velocity = new Vector2(charBody.velocity.x, jumpPower);
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
