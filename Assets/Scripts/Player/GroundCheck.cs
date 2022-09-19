using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private bool onGround, wasOnGround;

    public bool OnGround { get => onGround; set => onGround = value; }
    public bool WasOnGround { get => wasOnGround; set => wasOnGround = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            wasOnGround = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            wasOnGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onGround = false;
        wasOnGround = true;
    }
}
