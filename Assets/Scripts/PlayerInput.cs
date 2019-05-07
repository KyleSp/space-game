using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;

    private Rigidbody playerRb;
    private UIScript uiScript;

    private bool controllingShip = false;
    private GameObject controlledShip = null;
    private Rigidbody controlledShipRb = null;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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

        if (controllingShip)
        {
            controlledShip.GetComponent<ShipManager>().ControlShip(direction);
            // TODO: move/rotate player with respect to ship (maybe also rotate camera as well)
        } else
        {
            playerRb.velocity = speed * direction;
        }
    }

    public void StartShipControl(GameObject ship)
    {
        Debug.Log("start ship control");
        controllingShip = true;
        controlledShip = ship;
        controlledShipRb = ship.GetComponent<Rigidbody>();
        controlledShipRb.isKinematic = false;
    }

    public void StopShipControl()
    {
        Debug.Log("stop ship control");
        controllingShip = false;
        controlledShip = null;
        controlledShipRb = null;
    }

    private void CollisionHandler(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        if (Input.GetKeyDown(KeyCode.Space))
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
