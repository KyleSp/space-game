using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject healthText;
    private Health playerHealth;

    public GameObject controlPanel;
    public float cooldownDuration = 0.3f;
    private float cooldown;

    private bool controlPanelActive = false;

    void Start()
    {
        playerHealth = GameObject.Find("player").GetComponent<Health>();

        cooldown = Time.time - cooldownDuration;
    }

    void Update()
    {
        healthText.GetComponent<Text>().text = "Health: " + playerHealth.GetHealth();
    }

    public void ControlPanelToggle()
    {
        if (Time.time < cooldown + cooldownDuration) return;

        controlPanelActive = !controlPanelActive;
        controlPanel.SetActive(controlPanelActive);

        cooldown = Time.time;
    }

    public void ControlPanelClose()
    {
        controlPanelActive = false;
        controlPanel.SetActive(controlPanelActive);

        cooldown = Time.time;
    }
}
