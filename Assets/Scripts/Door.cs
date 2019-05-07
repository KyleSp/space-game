using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite doorOpenSprite;
    public Sprite doorCloseSprite;
    public float cooldownDuration = 0.3f;
    public float autoCloseCooldown = 3.0f;

    public Collider colliderClosed;
    public Collider colliderOpened1;
    public Collider colliderOpened2;

    private bool open = false;
    private float cooldown;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    void Start()
    {
        cooldown = Time.time - cooldownDuration;

        foreach (Transform childTransform in transform)
        {
            spriteRenderers.Add(childTransform.GetComponent<SpriteRenderer>());
        }
    }

    void Update()
    {
        
    }

    public void Toggle()
    {
        if (Time.time < cooldown + cooldownDuration) return;

        if (open)
        {
            Close();
        }
        else
        {
            Open();
        }

        open = !open;
        cooldown = Time.time;
    }

    private void Close()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = doorCloseSprite;
        }

        colliderClosed.isTrigger = false;
        colliderOpened1.enabled = false;
        colliderOpened2.enabled = false;
    }

    private void Open()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = doorOpenSprite;
        }

        colliderClosed.isTrigger = true;
        colliderOpened1.enabled = true;
        colliderOpened2.enabled = true;

        // TODO: make sure coroutine stops, if it was running already
        StopCoroutine(AutoClose());
        StartCoroutine(AutoClose());
    }

    private IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(3.0f);
        if (open) Close();
        yield return null;
    }
}
