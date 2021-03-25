using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Heart_HUD : MonoBehaviour
{
    public Sprite[] heart_Texture;

    private int heart_amount;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        heart_amount = heart_Texture.Length - 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateHeartUI();
    }

    void UpdateHeartUI()
    {
        spriteRenderer.sprite = heart_Texture[heart_amount];
    }

    public void UpdateHeartAmount(int damage)
    {
        heart_amount = damage;

        if (heart_amount < 0)
            heart_amount = 0;
    }
}
