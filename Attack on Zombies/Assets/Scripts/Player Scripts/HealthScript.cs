using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthScript : MonoBehaviour {
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;   
    private bool is_Dead;

    //private EnemyAudio enemyAudio;
    //private PlayerStats player_Stats;

    void Awake()
    {
        if (gameObject.tag == Tags.PLAYER_TAG)
        {
            //player stats
        } else if(gameObject.tag == Tags.ENEMY_TAG)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        
    }
	
	public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;
        health -= damage;
        Debug.Log(gameObject.tag.ToString() + " health is now: " + health.ToString());
        if(gameObject.tag == Tags.ENEMY_TAG)
        {
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
        }

        if (health <= 0)
        {
            Die();
            is_Dead = true;
        }
    }

    public void Die()
    {
        if (gameObject.tag == Tags.PLAYER_TAG)
        {
            Debug.Log("Player has died");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            // call enemy manager to stop spawning enemies
            //EnemyManager.instance.StopSpawning();

            /* GetComponent<FirstPersonController>().enabled = false;
             GetComponent<EnhancedMovement>().enabled = false;
             GetComponent<PlayerAttack>().enabled = false;
             GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);*/
            gameObject.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(SceneNames.DEATH_SCREEN, LoadSceneMode.Single);
        } else if(gameObject.tag == Tags.ENEMY_TAG)
        {
            Debug.Log("Enemy has died");    
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            //StartCoroutine(DeadSound());

            // EnemyManager spawn more enemies
            //EnemyManager.instance.EnemyDied(true);
        }
    }
}
