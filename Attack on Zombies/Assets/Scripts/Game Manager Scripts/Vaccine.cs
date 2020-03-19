using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine : MonoBehaviour {
    
    private GameManager GM;    
    [SerializeField]
    private int ID;

    void Awake()
    {
        GM = gameObject.GetComponentInParent<GameManager>();        
    }
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER_TAG)
        {
            gameObject.SetActive(false);
            GM.Increment_Vaccine_Count();
        }
    }
    public int GetID()
    {
       return ID;
    }
    public void SetID(int val)
    {
        ID = val;
    }
}
