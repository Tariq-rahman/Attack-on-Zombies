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
    private EnemySoundController enemy_Sound;
    public float health = 100f;   
    private bool is_Dead;
    public Animator death_Transition;    
    private PlayerStats player_Stats;

    void Awake()
    {
        if (gameObject.tag == Tags.PLAYER_TAG)
        {
            player_Stats = GetComponent<PlayerStats>();
        } else if(gameObject.tag == Tags.ENEMY_TAG)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            enemy_Sound = GetComponent<EnemySoundController>();
        }
        death_Transition = GameObject.FindGameObjectWithTag(AnimationTags.DEATH).GetComponent<Animator>();
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
                enemy_Sound.Play_Scream_Sound();
            }
        }
        if(gameObject.tag == Tags.PLAYER_TAG)
        {
            //play hurt sounds
            player_Stats.Display_HealthStats(health);
        }
        if (health <= 0)
        {
            is_Dead = true;
            Die();            
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
            EnemyManager.instance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            //GetComponent<MouseLook>().enabled = false;
            GetComponent<EnhancedMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);          
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            death_Transition.SetBool(AnimationTags.IS_DEAD, is_Dead);

        } else if(gameObject.tag == Tags.ENEMY_TAG)
        {                                             
            enemy_Anim.Dead();
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            StartCoroutine(DeadSound());
            // EnemyManager spawn more enemies
            EnemyManager.instance.Enemy_Died();

            Invoke("TurnOffGameObject", 3f);
        }        
    }
    public void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemy_Sound.Play_Death_Sound();
    }
}
