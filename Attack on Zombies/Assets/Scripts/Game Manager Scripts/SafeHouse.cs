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
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
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
}
