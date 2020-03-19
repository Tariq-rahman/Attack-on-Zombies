using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeHouse : MonoBehaviour {   
    private GameObject player;
    [SerializeField]
    private GameObject end_Level_screen;
    private GameManager GM;

    void Awake()
    {        
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        GM = gameObject.GetComponentInParent<GameManager>();
    }
	//end level
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER_TAG && GM.Can_End())
        {
            GM.Calculate_Score();
            GM.StopGame();
            end_Level_screen.SetActive(true);
        }       
    }
}
