using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player movement")]
    public float speed;
    public float jumpForce;
    Vector2 move;

    [Header("Ground detection")]
    public float groundRadius;
    public LayerMask groundLayer;
    [SerializeField] Vector3 groundCheckPos;
    private bool isGrounded;
    private bool isDucking;

    //Animator
    readonly int ah_xSpeed = Animator.StringToHash("xSpeed");
    readonly int ah_ySpeed = Animator.StringToHash("ySpeed");
    readonly int ah_isGround = Animator.StringToHash("isGrounded");
    readonly int ah_isDucking = Animator.StringToHash("isDucking");

    private Rigidbody2D rigi;
    private Animator anim;
    private SpriteRenderer draw;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        draw = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        
    }

    void Move()
    {
        // Check bools
        isGrounded = Physics2D.OverlapCircle(transform.position + groundCheckPos, groundRadius, groundLayer);
        isDucking = Input.GetAxisRaw("Vertical") < 0f;

        // Move
        move.x = Input.GetAxis("Horizontal") * speed;
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Jump") > 0f)
            {
                rigi.AddForce(Vector2.up * jumpForce);
            }
        }

        // Update flip
        if (move.x != 0f)
            draw.flipX = move.x < 0f;

        // Apply to rigibody
        move.y = rigi.velocity.y;
        rigi.velocity = move;

        // Update Animaor
        anim.SetFloat(ah_xSpeed, Mathf.Abs(move.x));
        anim.SetFloat(ah_ySpeed, move.y);
        anim.SetBool(ah_isGround, isGrounded);
        anim.SetBool(ah_isDucking, isDucking);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + groundCheckPos, groundRadius);
    }
}
