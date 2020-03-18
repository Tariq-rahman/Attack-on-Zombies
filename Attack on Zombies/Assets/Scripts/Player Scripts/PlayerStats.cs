using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image HP_Bar, stamina_bar;
    [SerializeField]
    private Text kill_count, kill_total, vaccine_count, vaccine_total, player_score;

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

    public void Display_Vaccine_Count(int val)
    {
        vaccine_count.text = val.ToString();
    }
    public void Display_Kill_Count(int val)
    {
        kill_count.text = val.ToString();
    }
    public void Display_Vaccine_Total(int val)
    {
        vaccine_total.text = val.ToString();
    }
    public void Display_Kill_Total(int val)
    {
        kill_total.text = val.ToString();
    }
    public void Display_Player_Score(int val)
    {                
        player_score.text = val.ToString();       
    }
}


