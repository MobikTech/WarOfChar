using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCtr : MonoBehaviour
{
    public float speed = 4;
    private float preSpeed;
    private float preDrag;
    private MoveMode preMoveMode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {

            preMoveMode = collision.GetComponent<PlayerController>().moveMode;
            preSpeed = collision.GetComponent<PlayerController>().moveSpeed;
            preDrag = collision.GetComponent<Rigidbody2D>().drag;
            collision.GetComponent<Rigidbody2D>().drag = 15;
            collision.GetComponent<PlayerController>().moveSpeed = speed;
            collision.GetComponent<PlayerController>().moveMode = MoveMode.OnWater;
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player" && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D" && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
    //    {
    //        collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * speed);
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            collision.GetComponent<PlayerController>().moveMode = preMoveMode;
            collision.GetComponent<PlayerController>().moveSpeed = preSpeed;
            collision.GetComponent<Rigidbody2D>().drag = preDrag;
        }
    }
}
