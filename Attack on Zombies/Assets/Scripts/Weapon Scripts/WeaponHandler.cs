using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE, 
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum BulletType
{
    BULLET, 
    ARROW, 
    SPEAR,
    NONE,
}
public class WeaponHandler : MonoBehaviour {

    private Animator Anim;
    public WeaponAim Weapon_Aim;
    [SerializeField]
    private GameObject muzzle_Flash;
    [SerializeField]
    private AudioSource Shoot_Sound, Reload_Sound;
    public WeaponFireType fire_Type;
    public BulletType Bullet_Type;
    public GameObject Attack_Point;

    void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        Anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);

    }
    public void Aim(bool canAim)
    {
        Anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }   
    void Turn_On_MuzzleFlash()
    {
        muzzle_Flash.SetActive(true);
    }
    void Turn_Off_MuzzleFlash()
    {
        muzzle_Flash.SetActive(false);
    }
    void Play_ShootSound()
    {
        Shoot_Sound.Play();
    }
    void Play_ReloadSound()
    {
        Reload_Sound.Play();
    }
    void Turn_On_AttackPoint()
    {
        Attack_Point.SetActive(true);
    }
    void Turn_Off_AttackPoint()
    {
        if (Attack_Point.activeInHierarchy)
        {
            Attack_Point.SetActive(false);
        }
    }
}
