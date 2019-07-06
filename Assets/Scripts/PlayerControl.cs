using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Public Variables
    public float runSpeed;

    // Private Variables
    private float moveInput;
    private Rigidbody2D charBody;
    private bool facingRight;

    // Base Functions
    void Awake()
    {
        charBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    // Defines Player Movement
    void Movement()
    {
        moveInput = Input.GetAxisRaw("Horizontal") * runSpeed;

        charBody.velocity = new Vector2(moveInput, charBody.velocity.y);

        if (moveInput > 0 && !facingRight ||  moveInput < 0 && facingRight)
            CharacterFlip();
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
