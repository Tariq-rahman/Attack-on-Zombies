using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image HP_Bar, stamina_bar;

    public void Display_HealthStats(float healthValue)
    {
        healthValue /= 100f;
        HP_Bar.fillAmount = healthValue;
    }

    public void Display_StaminaStats(float staminaValue)
    {
        staminaValue /= 100f;
        stamina_bar.fillAmount = staminaValue;
    }
}


