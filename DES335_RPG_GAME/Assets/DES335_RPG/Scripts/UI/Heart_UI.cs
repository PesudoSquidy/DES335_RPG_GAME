using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Heart_UI : MonoBehaviour
{
    public Texture[] heart_Texture;

    private int heart_amount;

    private RawImage rawImage;

    void Start()
    {
        // Index 
        // 0 - Empty
        // 1 - Half
        // 2 - Full
        heart_amount = heart_Texture.Length - 1;

        Debug.Log("Heart Amount: " + heart_amount);

        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        UpdateHeartUI();
    }

    void UpdateHeartUI()
    {
        rawImage.texture = heart_Texture[heart_amount];
    }

    public void UpdateHeartAmount(int damage)
    {
        heart_amount = damage;

        if (heart_amount < 0)
            heart_amount = 0;
    }
}
