using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pickaxe_Stamina_UI : MonoBehaviour
{
    public Slider slider;
    private Coroutine tempCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        //if (playerStamina == null)
        //    playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina_2>();

        //// Change slider value
        //slider.maxValue = playerStamina.maxStamina;
    }

    // Update is called once per frame
    
  
}
