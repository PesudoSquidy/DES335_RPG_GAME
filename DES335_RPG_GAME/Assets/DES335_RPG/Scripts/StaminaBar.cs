using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public static Stamina _stamina;
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
            if (!_stamina.bStaminaDrain && _stamina.SpendStamina(20))
                _stamina.DrainStamina();
            else 
                _stamina.RegenStamina();
        }

        _stamina.Update();
        _barImage.fillAmount = _stamina.GetStaminaNormalized();
    }
}

public class Stamina
{
    public bool bStaminaDrain;
    [SerializeField] public const int _MaxStamina = 100;
    [SerializeField] private float _CurrStamina;
    [SerializeField] private float _StaminaRegen;
    [SerializeField] private float _StaminaDrain;
    

    public Stamina()
    {
        _CurrStamina = _MaxStamina;
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
                RegenStamina();
            }
        }
        else // Stamina Regenerates
        {
            _CurrStamina += _StaminaRegen * Time.deltaTime;   
        }
        _CurrStamina = Mathf.Clamp(_CurrStamina, 0f, _MaxStamina);
    }

    public bool SpendStamina(int amount)
    {
        if (_CurrStamina >= amount)
        {
            _CurrStamina -= amount;
            return true;
        }

        return false;
    }
    public float GetStaminaNormalized()
    {
        return _CurrStamina / _MaxStamina;    
    }
    public void RegenStamina()
    {
        bStaminaDrain = false;
    }

    public void DrainStamina()
    {
        bStaminaDrain = true;
    }
}