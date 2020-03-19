using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Rigidbody body;
    public float speed = 30f;
    public float deactivate_timer = 3f;
    public float damage = 30f;
    
    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    public void Fire(Camera mainCamera)
    {
        body.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + body.velocity);
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.ENEMY_TAG)
        {

            target.GetComponent<HealthScript>().ApplyDamage(damage);

            gameObject.SetActive(false);

        }
    }
	// Use this for initialization
	void Start () {
        Invoke("DeactivateGameObject", deactivate_timer);
	}		
}
