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
    private PlayerSounds player_sounds;
    private GameManager GM;

    void Awake()
    {
        if (gameObject.tag == Tags.PLAYER_TAG)
        {
            player_Stats = GetComponent<PlayerStats>();
            player_sounds = GetComponent<PlayerSounds>();
            death_Transition = GameObject.FindGameObjectWithTag(AnimationTags.DEATH).GetComponent<Animator>();
        } else if (gameObject.tag == Tags.ENEMY_TAG)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            enemy_Sound = GetComponent<EnemySoundController>();
        }        
        GM = GameObject.FindGameObjectWithTag(Tags.GAME_MANAGER).GetComponent<GameManager>();     
    }

    public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;

        health -= damage;
        if (gameObject.tag == Tags.ENEMY_TAG)
        {
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 70f;
                enemy_Sound.Play_Scream_Sound();
            }
        }
        if (gameObject.tag == Tags.PLAYER_TAG)
        {
            //play hurt sound todo
            player_sounds.Play_Hurt_Sound();
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
            GM.StopGame();
            // Player death sound todo
            player_sounds.Play_Death_Sound();
            death_Transition.SetBool(AnimationTags.IS_DEAD, is_Dead);

        } else if (gameObject.tag == Tags.ENEMY_TAG)
        {
            enemy_Anim.Dead();
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            StartCoroutine(DeadSound());
            // EnemyManager spawn more enemies after 5 seconds
            Invoke("Spawn_Enemies", 5f);
            GM.GetComponent<GameManager>().Increment_Kill_Count();            
            Invoke("TurnOffGameObject", 3f);
        }
    }

    public void Spawn_Enemies()
    {
        EnemyManager.instance.Enemy_Died();
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
