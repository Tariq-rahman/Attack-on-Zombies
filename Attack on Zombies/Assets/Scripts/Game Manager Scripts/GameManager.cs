using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] requiredVaccines;
    [SerializeField]
    private int kill_Requirement_Count;
    private int kill_Count;
    private int vaccine_Count;
    private int player_Score;    
    private PlayerStats player_stats;
    public GameObject inGameMenu;  
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        
        player_stats = player.GetComponent<PlayerStats>();
        Set_Totals();        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inGameMenu.activeSelf)
        {
            inGameMenu.SetActive(false);
            StartGame();
        } else if(Input.GetKeyDown(KeyCode.Escape) && !inGameMenu.activeSelf)
        {
            inGameMenu.SetActive(true);
            StopGame();            
        }
    }
    public bool Can_End()
    {       
        if (vaccine_Count == requiredVaccines.Length && kill_Count >= kill_Requirement_Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }   
    public void SetScore(int score)
    {
        player_Score = score;
    }
    // for the use of the savemanager to load in the remaining vaccines
    public void setRequiredVaccines(GameObject[] vaccines)
    {
        requiredVaccines = vaccines;
    }
    public GameObject[] GetRequiredVaccines()
    {
        Debug.Log(requiredVaccines);
        return requiredVaccines;
    }
    public int GetKillCount()
    {
        return kill_Count;
    }
    public void SetKillCount(int val)
    {
        kill_Count = val;
        player_stats.Display_Kill_Count(kill_Count);
    }
    public void SetVaccineCount(int val)
    {
        vaccine_Count = val;
        player_stats.Display_Vaccine_Count(vaccine_Count);
    }
    public int GetVaccineCount()
    {
        return vaccine_Count;
    }
    public void Set_Totals()
    {
        player_stats.Display_Vaccine_Total(requiredVaccines.Length);
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
    public int Calculate_Score()
    {
        // minus time and bullets used.        
        player_Score = (kill_Count * 50) + (vaccine_Count * 200);        
        player_stats.Display_Player_Score(player_Score);
        PlayerPrefs.SetInt("PlayerScore",player_Score);
        return player_Score;
    }
    public void StopGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().enabled = false;
        }
        EnemyManager.instance.StopSpawning();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerAttack>().enabled = false;
        player.GetComponent<PlayerFootsteps>().enabled = false;
        player.GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
    }   
    public void StartGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().enabled = true;
        }
        EnemyManager.instance.CheckToSpawnEnemies();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerAttack>().enabled = true;
        player.GetComponent<PlayerFootsteps>().enabled = true;
        player.GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(true);
    }
}