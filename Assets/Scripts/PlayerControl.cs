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

    // Base Functions
    void Awake()
    {
        charBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    // Actions
    void Movement()
    {
        moveInput = Input.GetAxisRaw("Horizontal") * runSpeed;

        charBody.velocity = new Vector2(moveInput, charBody.velocity.y);
    }
}
