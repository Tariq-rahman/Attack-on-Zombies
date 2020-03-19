using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData {

    public float health;
    public float[] position;
    public int kills;
    public int collectedVaccines;
    public int score;
    public int gameLevel;   
    public Dictionary<int,bool> vaccines;
    
    //public int ammo;

	public PlayerData(GameObject player, GameManager GM)
    {
        vaccines = new Dictionary<int, bool>();
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        health = player.GetComponent<HealthScript>().health;
        gameLevel = SceneManager.GetActiveScene().buildIndex;
        kills = GM.GetKillCount();
        collectedVaccines = GM.GetVaccineCount();
        score = GM.Calculate_Score();
        GameObject[] vacc = GameObject.FindGameObjectsWithTag(Tags.VACCINE);
        for(int i = 0; i < vacc.Length ; i++)
        {
            Debug.Log(vacc[i].GetComponent<Vaccine>().GetID());
            vaccines.Add(vacc[i].GetComponent<Vaccine>().GetID(), vacc[i].activeSelf);
        }
    }
}
