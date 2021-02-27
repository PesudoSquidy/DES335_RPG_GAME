using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public static PlayerStamina _stamina;
    private Image _barImage;

    private void Awake()
    {
        _barImage = transform.Find("bar").GetComponent<Image>();

        if (_stamina == null)
            _stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina>();
    }

    public void Update()
    {
        if (_stamina != null)
        {
            _stamina.Update();
            _barImage.fillAmount = _stamina.GetStaminaNormalized();
        }
    }
}

