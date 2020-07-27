using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    private Rigidbody2D rb;
    private float moveInput;
    public UnityEvent OnDead;
    private Vector3 facingRight;
    private Vector3 facingLeft;


    private bool isGrounded = true;
    public LayerMask checkLayer;
    public BoxCollider2D boxCollider;
    public float extraDis = 0.1f;


    void Start()
    {
        facingRight = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        facingLeft = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

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
            OnDead.Invoke();
        }

        #endregion

        #region Vertical moving

        //Collider2D[] colliders = Physics2D.OverlapCircleAll(checkOnGround.position, checkRadius, checkLayer);

        //isGrounded = colliders.Length > 0;
        //isGrounded = Physics2D.OverlapCircle(checkOnGround.position, checkRadius, checkLayer);
        //isGrounded = Physics2D.OverlapArea(PointA.position,PointB.position,checkLayer);
        IsGrounded();
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            Jump();
        }

        #endregion

    }
    public void TeleportToStart()
    {
        transform.position = new Vector3(1, 0, transform.position.z);
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void IsGrounded()
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



        Debug.DrawRay(new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y), Vector2.down *  extraDis, rayColor);
        Debug.DrawRay(new Vector2(boxCollider.bounds.min.x + boxCollider.bounds.extents.x, boxCollider.bounds.min.y), Vector2.down * extraDis, rayColor);
        Debug.DrawRay(new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y), Vector2.down * extraDis, rayColor);

        //Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extraDis), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraDis), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraDis), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.size.x), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extraDis), Vector2.right * (boxCollider.bounds.size.x), rayColor);
        //isGrounded = ray.collider != null;
        if (ray1.collider != null || ray2.collider != null || ray3.collider != null )
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
