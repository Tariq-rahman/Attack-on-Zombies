using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine : MonoBehaviour {
    
    private GameManager GM;
    private PlayerStats player_stats;

    void Awake()
    {
        GM = gameObject.GetComponentInParent<GameManager>();        
    }

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER_TAG)
        {
            gameObject.SetActive(false);
            GM.Increment_Vaccine_Count();
        }
    }
}
