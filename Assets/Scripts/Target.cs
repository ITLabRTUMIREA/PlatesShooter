using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    private PlayerStats playerStats;
    private bool died = false;

	// Use this for initialization
	void Start () {
        playerStats = FindObjectOfType<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && !died)
        {
            playerStats.AddScore(1);
            died = true;
        }
    }
}
