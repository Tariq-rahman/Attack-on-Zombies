using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeHouse : MonoBehaviour {   
    private GameObject player;
    [SerializeField]
    private GameObject end_Level_screen;

    void Awake()
    {        
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
    }
	
    private void OnTriggerEnter(Collider other)
    {
        // stop enemies
        // open up menu for next level and in game shop
        EnemyManager.instance.StopSpawning();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<EnhancedMovement>().enabled = false;
        player.GetComponent<PlayerAttack>().enabled = false;
        player.GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        end_Level_screen.SetActive(true);

    }
}
