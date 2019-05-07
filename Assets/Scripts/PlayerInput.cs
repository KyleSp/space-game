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
    private Vector3 initialPlayerPosition = Vector3.zero;
    private float initialPlayerRotation = 0.0f;

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopShipControl();
            }
        } else
        {
            playerRb.velocity = speed * direction;
        }
    }

    public void StartShipControl(GameObject ship)
    {
        controllingShip = true;
        controlledShip = ship;
        controlledShipRb = ship.GetComponent<Rigidbody>();
        controlledShipRb.isKinematic = false;
        playerRb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        initialPlayerPosition = playerRb.position;
        initialPlayerRotation = controlledShipRb.rotation.eulerAngles.z;
    }

    public void StopShipControl()
    {
        // playerRb.position = controlledShipRb.position + initialPlayerPosition;
        float shipAngle = controlledShipRb.rotation.eulerAngles.z;
        float deltaAngle = initialPlayerRotation + shipAngle;
        // [A B] [x]   [Ax + By]   [cos(theta)*x - sin(theta)*y]
        // [C D] [y] = [Cx + Dy] = [sin(theta)*x + cos(theta)*y]

        float angle = Mathf.Deg2Rad * deltaAngle;
        float newX = Mathf.Cos(angle) * initialPlayerPosition.x - Mathf.Sin(angle) * initialPlayerPosition.y;
        float newY = Mathf.Sin(angle) * initialPlayerPosition.x + Mathf.Cos(angle) * initialPlayerPosition.y;
        Vector3 rotatedVector = new Vector3(newX, newY, 0);
        playerRb.position = controlledShipRb.position + rotatedVector;

        initialPlayerPosition = Vector3.zero;
        initialPlayerRotation = 0.0f;

        controlledShipRb.isKinematic = true;
        playerRb.isKinematic = false;
        controllingShip = false;
        controlledShip = null;
        controlledShipRb = null;
        GetComponent<Collider>().enabled = true;
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
