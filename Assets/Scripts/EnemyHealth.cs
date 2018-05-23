using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;
	
	// Update is called once per frame
	void Update () {

        if (health <= 0) {
            Debug.Log(health);
            Destroy(gameObject);
        }
		
	}
}
