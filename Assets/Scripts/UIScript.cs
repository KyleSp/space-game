using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject healthText;
    private GameObject player;
    private Health playerHealth;

    public GameObject controlPanel;

    private bool controlPanelActive = false;

    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();
    }

    void Update()
    {
        healthText.GetComponent<Text>().text = "Health: " + playerHealth.GetHealth();
    }

    public void ControlPanelToggle()
    {
        controlPanelActive = !controlPanelActive;
        controlPanel.SetActive(controlPanelActive);
    }

    public void ControlPanelClose()
    {
        controlPanelActive = false;
        controlPanel.SetActive(controlPanelActive);
    }

    public void ToggleShipControl()
    {
        PlayerInput playerInput = player.GetComponent<PlayerInput>();
        playerInput.StartShipControl(player.transform.parent.gameObject);
        ControlPanelClose();
    }
}
