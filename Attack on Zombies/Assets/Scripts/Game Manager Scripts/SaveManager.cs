using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {
    PlayerData playerData;
    bool loading = false;
    public void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        GameManager GM = GameObject.FindGameObjectWithTag(Tags.GAME_MANAGER).GetComponent<GameManager>();
        SaveSystem.SaveData(player, GM);
        Debug.Log("Game Saved");
    }
    public void LoadGame()
    {
        Debug.Log("Loading...");
        playerData = SaveSystem.LoadData();
        //do the thing
        if (playerData != null)
        {
            SceneManager.LoadSceneAsync(playerData.gameLevel);
            loading = true;
        } else
        {
            Debug.LogError("There is no save file!");
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (loading)
        {
            GameManager GM = GameObject.FindGameObjectWithTag(Tags.GAME_MANAGER).GetComponent<GameManager>();
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);           
            GameObject[] vaccines = GameObject.FindGameObjectsWithTag(Tags.VACCINE);
            
            for(int i = 0; i < vaccines.Length; i++)
            {
                if (!playerData.vaccines.ContainsKey(vaccines[i].GetComponent<Vaccine>().GetID()))
                {
                    vaccines[i].SetActive(false);
                }                              
            }                      
            player.GetComponent<HealthScript>().health = playerData.health;
            player.GetComponent<PlayerStats>().Display_HealthStats(playerData.health);
            Vector3 position = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);
            player.transform.position = position;
            GM.SetKillCount(playerData.kills);
            GM.SetVaccineCount(playerData.collectedVaccines);
            GM.SetScore(playerData.score);
            Debug.Log("Loading complete");
            loading = false;
        }        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
