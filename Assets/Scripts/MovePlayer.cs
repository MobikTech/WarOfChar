using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 0.12f;
    public float jumpForce = 1f;
    public UnityEvent OnDead;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(Vector2.left * moveSpeed);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.Translate(Vector2.right * moveSpeed);
        }
        if (transform.position.y < -7)
        {
            OnDead.Invoke();
        }
    }
    public void TeleportToStart()
    {
        transform.position = new Vector3(1, 0, transform.position.z);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
