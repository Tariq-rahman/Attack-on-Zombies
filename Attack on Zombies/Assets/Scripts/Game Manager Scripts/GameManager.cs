using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int vaccine_Requirement_Count;
    [SerializeField]
    private int kill_Requirement_Count;
    private int kill_Count;
    private int vaccine_Count;
    private int player_Score;    
    private PlayerStats player_stats;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        player_stats = player.GetComponent<PlayerStats>();
        Set_Totals();
    }
    public bool Can_End()
    {
        if (vaccine_Count == vaccine_Requirement_Count && kill_Count >= kill_Requirement_Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Set_Totals()
    {
        player_stats.Display_Vaccine_Total(vaccine_Requirement_Count);
        player_stats.Display_Kill_Total(kill_Requirement_Count);
    }

    public void Increment_Vaccine_Count()
    {
        vaccine_Count++;
        player_stats.Display_Vaccine_Count(vaccine_Count);
    }

    public void Increment_Kill_Count()
    {
        kill_Count++;
        player_stats.Display_Kill_Count(kill_Count);
    }    
    public void Calculate_Score()
    {
        // minus time and bullets used.
        player_Score = (kill_Count * 50) + (vaccine_Count * 200);
        player_stats.Display_Player_Score(player_Score);
    }
}