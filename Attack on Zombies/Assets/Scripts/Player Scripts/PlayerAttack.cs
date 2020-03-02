using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private Transform arrow_Bow_StartPosition;

    void Awake() {        
        weapon_Manager = GetComponent<WeaponManager>();       
        zoomCameraAnim = GameObject.FindGameObjectWithTag(Tags.ZOOM_CAMERA).GetComponent<Animator>();        
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot() {
        // if we have assault riffle
        if(weapon_Manager.GetCurrentSelectedWeapon().fire_Type == WeaponFireType.MULTIPLE) {
            // if we press and hold left mouse click AND
            // if Time is greater than the nextTimeToFire
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire) {
                nextTimeToFire = Time.time + 1f / fireRate;
                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                 //BulletFired();
            }
            // if we have a regular weapon that shoots once
        } else {
            if(Input.GetMouseButtonDown(0)) {
                // handle melee weapons such as axe
                if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.MELEE_TAG) {
                    Debug.Log("melee attack");
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                // handle shoot
                if(weapon_Manager.GetCurrentSelectedWeapon().Bullet_Type == BulletType.BULLET) {
                    Debug.Log("ranged attack");
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    //BulletFired();
                } else {
                    // we have an arrow or spear
                    if(is_Aiming) {
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                        if(weapon_Manager.GetCurrentSelectedWeapon().Bullet_Type == BulletType.ARROW) {
                            // throw arrow
                            ThrowProjectile(Tags.ARROW);

                        } else if(weapon_Manager.GetCurrentSelectedWeapon().Bullet_Type == BulletType.SPEAR) {

                            // throw spear
                            ThrowProjectile(Tags.SPEAR);
                        }
                    }
                } // else
            } // if input get mouse button 0
        } // else
    } // weapon shoot

    void ZoomInAndOut() {

        // we are going to aim with our camera on the weapon
        if(weapon_Manager.GetCurrentSelectedWeapon().Weapon_Aim == WeaponAim.AIM) {

            // if we press and hold right mouse button
            if(Input.GetMouseButtonDown(1)) {

                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);

                crosshair.SetActive(false);
            }

            // when we release the right mouse button click
            if (Input.GetMouseButtonUp(1)) {

                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);

                crosshair.SetActive(true);
            }

        } // if we need to zoom the weapon

        if(weapon_Manager.GetCurrentSelectedWeapon().Weapon_Aim == WeaponAim.SELF_AIM) {

            if(Input.GetMouseButtonDown(1)) {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);
                is_Aiming = true;
            }
            if (Input.GetMouseButtonUp(1)) {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;
            }
        } // weapon self aim
    } // zoom in and out

    void ThrowProjectile(string projectileType)
    {
        if (projectileType == Tags.ARROW)
        {
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Bow_StartPosition.position;
            arrow.GetComponent<Projectile>().Fire(mainCam);
        }
        else if (projectileType == Tags.SPEAR) 
        {
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Bow_StartPosition.position;
            spear.GetComponent<Projectile>().Fire(mainCam);
        }
    } // throw arrow or spear

    /*void BulletFired() {
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {
            if(hit.transform.tag == Tags.ENEMY_TAG) {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    } // bullet fired*/
} // class































