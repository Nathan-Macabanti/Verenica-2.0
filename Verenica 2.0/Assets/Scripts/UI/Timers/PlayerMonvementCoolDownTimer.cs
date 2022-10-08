using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMonvementCoolDownTimer : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float alpha;

    [SerializeField] private Image image;
    [Header("Reference")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Color originalColor = new Color();
    [SerializeField] private Color cantMoveColor = new Color();
    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer != null)
        {
            originalColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a);
            cantMoveColor.a = originalColor.a * (1.0f - alpha);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement == null) return;

        //UpdateCharacterAlpha();
        UpdateTimer();
    }

    void UpdateCharacterAlpha()
    {
        if (spriteRenderer == null) return;
        if (!playerMovement.canMove)
        {
            spriteRenderer.color = cantMoveColor;
        }
        else
        {
            spriteRenderer.color = originalColor;
        }
    }

    void UpdateTimer()
    {
        //Debug.Log(image.fillAmount.ToString());
        if (image == null) return;

        image.fillAmount = playerMovement.coolDownTimerPercent;

        if (image.fillAmount <= 0.0f)
        {
            image.color = ColorUtils.invisibleColor;
        }
        else
        {
            image.color = ColorUtils.fullyVisibleColor;
        }
    }
}
