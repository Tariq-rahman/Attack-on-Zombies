using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour {

    [SerializeField]
    private AudioSource attack_Sound, scream_Sound, short_Scream_Sound, death_Sound, injured_Sound, idle_sound;
    
    public void Play_Attack_Sound()
    {
        attack_Sound.Play();
    }
    public void Play_Scream_Sound()
    {
        scream_Sound.Play();
    }
    public void Play_Death_Sound()
    {
        death_Sound.Play();
    }

    public void Play_Injured_Sound()
    {
        injured_Sound.Play();
    }  
    public void Play_Idle_Sound()
    {
        idle_sound.Play();
    }
    public void Play_Short_Scream_Sound()
    {
        short_Scream_Sound.Play();
    }
}
