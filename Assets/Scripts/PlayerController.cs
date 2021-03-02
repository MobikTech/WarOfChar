using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    private GameObject spawner;
    private GameObject mainCamera;
    private Rigidbody2D rb;
    private float moveInput;
    private Vector3 facingRight;
    private Vector3 facingLeft;

    private bool isGrounded = true;
    public LayerMask checkLayer;
    public CapsuleCollider2D boxCollider;
    public float extraDis = 0.1f;

    void Start()
    {
        facingRight = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        facingLeft = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        rb = GetComponent<Rigidbody2D>();
        spawner = GameObject.FindGameObjectWithTag("Respawn");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Respawn();
    }

    void Update()
    {
        //IsGrounded();

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        #region Horizontal moving
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0) transform.localScale = facingRight;
        else if (moveInput < 0) transform.localScale = facingLeft;

        #endregion

        #region Check on dead
        if (transform.position.y < -7)
        {
            Respawn();
        }
        #endregion

    }
    private void Jump()
    {

        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Respawn()
    {
        transform.position = spawner.transform.position;
    }
    private bool IsGrounded()
    {
        //RaycastHit2D ray = Physics2D.BoxCast(boxCollider.bounds.center,
        //    boxCollider.bounds.size * 0.9f, 0f, Vector2.down, extraDis, checkLayer);
        RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y), Vector2.down, extraDis, checkLayer);
        RaycastHit2D ray2 = Physics2D.Raycast(new Vector2(boxCollider.bounds.min.x + boxCollider.bounds.extents.x, boxCollider.bounds.min.y), Vector2.down, extraDis, checkLayer);
        RaycastHit2D ray3 = Physics2D.Raycast(new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y), Vector2.down, extraDis, checkLayer);



        Color rayColor;
        if (ray1.collider != null || ray2.collider != null || ray3.collider != null)
        {
            rayColor = Color.green;
        }
        else rayColor = Color.red;



        Debug.DrawRay(new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y), Vector2.down * extraDis, rayColor);
        Debug.DrawRay(new Vector2(boxCollider.bounds.min.x + boxCollider.bounds.extents.x, boxCollider.bounds.min.y), Vector2.down * extraDis, rayColor);
        Debug.DrawRay(new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y), Vector2.down * extraDis, rayColor);

        //Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extraDis), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraDis), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraDis), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.size.x), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extraDis), Vector2.right * (boxCollider.bounds.size.x), rayColor);
        //isGrounded = ray.collider != null;
        if (ray1.collider != null || ray2.collider != null || ray3.collider != null)
        {
            return true;
            //isGrounded = true;
        }
        else
        {
            return false;
            //isGrounded = false;
        }
    }
}
