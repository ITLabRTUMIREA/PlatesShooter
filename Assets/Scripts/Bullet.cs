using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float selfDestructIn = 10.0f;
	
	void Update () {
        selfDestructIn -= Time.deltaTime;

        if (selfDestructIn <= 0.0f)
        {
            Destroy(gameObject);
        }
	}
}
