using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    private UIScript uiScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        uiScript = GameObject.Find("Canvas").GetComponent<UIScript>();
    }

    void Update()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        } else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }

        if (direction != Vector2.zero)
        {
            uiScript.ControlPanelClose();
        }

        rb.velocity = speed * direction;
    }

    private void CollisionHandler(Collision collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (collision.collider.CompareTag("Door"))
            {
                Door door = collision.collider.GetComponent<Door>();
                door.Toggle();
            }
            else if (collision.collider.CompareTag("ControlStation"))
            {
                uiScript.ControlPanelToggle();
            }
        }
    }

    private void TriggerHandler(Collider other)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (other.CompareTag("Door"))
            {
                Door door = other.GetComponent<Door>();
                door.Toggle();
            }
            else if (other.CompareTag("Bed"))
            {
                Debug.Log("Interacted with bed");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionHandler(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionHandler(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerHandler(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerHandler(other);
    }
}
