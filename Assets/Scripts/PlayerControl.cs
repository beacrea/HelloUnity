using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Public Variables
    public float characterRunSpeed;

    // Private Variables
    private float moveInput;
    private Rigidbody2D charBody;

    void Awake()
    {
        charBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        moveInput = Input.GetAxisRaw("Horizontal") * characterRunSpeed;

        charBody.velocity = new Vector2(moveInput, charBody.velocity.y);
    }
}
