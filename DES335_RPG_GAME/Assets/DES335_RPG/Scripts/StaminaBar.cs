using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Stamina _stamina;
    private Image _barImage;

    private void Awake()
    {
        _barImage = transform.Find("bar").GetComponent<Image>();
        _stamina = new Stamina();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_stamina.bStaminaDrain)
            {
                _stamina.SpendStamina(20);
            }
            _stamina.ToggleDrain();
        }

        _stamina.Update();
        _barImage.fillAmount = _stamina.GetStaminaNormalized();
    }
    
}

public class Stamina
{
    public bool bStaminaDrain;
    public const int _MaxStamina = 100;
    private float _CurrStamina;
    private float _StaminaRegen;
    private float _StaminaDrain;
    

    public Stamina()
    {
        _CurrStamina = 0;
        _StaminaRegen = 30f;
        _StaminaDrain = 5f;
        bStaminaDrain = false;
    }

    public void Update()
    {
        if (bStaminaDrain == true)
        {
            _CurrStamina -= _StaminaDrain * Time.deltaTime;
            if (_CurrStamina <= 0)
            {
                ToggleDrain();
            }
        }
        else // Stamina Regenerates
        {
            _CurrStamina += _StaminaRegen * Time.deltaTime;   
        }
        _CurrStamina = Mathf.Clamp(_CurrStamina, 0f, _MaxStamina);
    }

    public void SpendStamina(int amount)
    {
        if (_CurrStamina >= amount)
        {
            _CurrStamina -= amount;
        }
    }
    public float GetStaminaNormalized()
    {
        return _CurrStamina / _MaxStamina;    
    }
    public void ToggleDrain()
    {
        bStaminaDrain = !(bStaminaDrain);
    }
}