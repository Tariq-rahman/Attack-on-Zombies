using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedMovement : MonoBehaviour {

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
    public float sprint_speed = 10f;
    public float move_speed = 5f;
    public float crouch_speed = 2f;

    private Transform player_view;
    private float stand_height = 1.6f;
    private float crouch_height = 1f;

    private bool is_crouching;

    void Awake ()
    {
        fpsController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        player_view = transform.GetChild(0);
    }

    void Sprint ()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching)
        {
           // fpsController.speed = sprint_speed;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
