using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite doorOpenSprite;
    public Sprite doorCloseSprite;
    public float autoCloseCooldown;

    public Collider colliderClosed;
    public Collider colliderOpened1;
    public Collider colliderOpened2;

    private bool open = false;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    void Start()
    {
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
        if (open)
        {
            open = !open;
            Close();
        }
        else
        {
            open = !open;
            Open();
        }
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

        StartCoroutine(AutoClose());
    }

    private IEnumerator AutoClose()
    {
        int numStepsPerSec = 10;
        float timeToWait = autoCloseCooldown / numStepsPerSec;
        bool closeDoor = true;
        for (int i = 0; i < (int) (autoCloseCooldown * numStepsPerSec); ++i)
        {
            if (!open) {
                closeDoor = false;
                break;
            }
            yield return new WaitForSeconds(timeToWait);
        }
        if (open && closeDoor) {
            open = false;
            Close();
        }
        yield return null;
    }
}
