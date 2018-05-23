using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateParticles : MonoBehaviour
{
    private float currentLifetime;
    
    void Start()
    {
        currentLifetime = gameObject.GetComponent<ParticleSystem>().main.duration;
    }
    
    void Update()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}