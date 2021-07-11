using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCtr : MonoBehaviour
{
    public float speed = 4;
    private float preSpeed;
    private float preGravScale;
    private MoveMode preMoveMode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            preGravScale = collision.attachedRigidbody.gravityScale;
            preMoveMode = collision.GetComponent<PlayerController>().moveMode;
            preSpeed = collision.GetComponent<PlayerController>().moveSpeed;
            collision.GetComponent<PlayerController>().moveSpeed = speed;
            collision.attachedRigidbody.gravityScale = 0;
            collision.GetComponent<PlayerController>().moveMode = MoveMode.OnLadder;
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //if (collision.tag == "Player" && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
    //    //{
    //    //    collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.position.x, Input.GetAxisRaw("Vertical") * vertSpeed);
    //    //}
    //    //else
    //    //{
    //    //    collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.position.x, 0);
    //    //}

    //    if (collision.tag == "Player" && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D" && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
    //    {
    //        collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * speed);
    //    }
    //    else
    //    {
    //        collision.attachedRigidbody.velocity = new Vector2(0, 0);
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            collision.attachedRigidbody.gravityScale = preGravScale;
            collision.GetComponent<PlayerController>().moveMode = preMoveMode;
            collision.GetComponent<PlayerController>().moveSpeed = preSpeed;
        }
    }
}
