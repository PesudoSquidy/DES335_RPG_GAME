using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public bool bStaminaDrain;
    [SerializeField] public const int _MaxStamina = 100;
    [SerializeField] private float _CurrStamina;
    [SerializeField] private float _StaminaRegen;
    [SerializeField] private float _StaminaDrain;

    public PlayerStamina()
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
                bStaminaDrain = false;
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
}
