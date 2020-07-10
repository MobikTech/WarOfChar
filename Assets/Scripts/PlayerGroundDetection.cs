using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetection : MonoBehaviour
{
    public bool IsGrounded = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            IsGrounded = true;
        }
    }
}
