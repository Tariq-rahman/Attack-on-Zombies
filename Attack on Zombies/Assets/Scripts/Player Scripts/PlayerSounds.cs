using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    [SerializeField]
    private AudioSource out_Of_Breath_Sound, hurt_Sound, death_Sound;

	public void Play_Death_Sound()
    {
        death_Sound.Play();
    }
    public void Play_Out_Of_Breath_Sound()
    {
        out_Of_Breath_Sound.Play();
    }

    public void Play_Hurt_Sound()
    {
        hurt_Sound.PlayOneShot(hurt_Sound.clip);
    }
}
