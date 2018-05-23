using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour {
    private const float AIR_DENSITY = 1.2250f;

    public PlayerStats playerStats;

    public GameObject plateParticles;

    public bool physicsEnabled = false;

    public float plateRadius;
    public float resistanceCoefficient = 1.0f;

    private new Rigidbody rigidbody;
    private float plateSquare;

    private bool died = false;

    void Start()
    {
        plateSquare = 2 * Mathf.PI * Mathf.Pow(plateRadius, 2.0f);
        rigidbody = gameObject.GetComponent<Rigidbody>();

        playerStats = FindObjectOfType<PlayerStats>();
    }

    void Update () {
        if (physicsEnabled)
        {
            float v = Vector3.Dot(rigidbody.velocity, gameObject.transform.forward);
            float force = 0.5f * AIR_DENSITY * Mathf.Pow(v, 2.0f) * resistanceCoefficient;

            rigidbody.AddForce(gameObject.transform.forward * force, ForceMode.Acceleration);
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        GameObject particles = Instantiate(plateParticles);
        particles.transform.position = gameObject.transform.position;
        Destroy(this.gameObject);

        if (collider.gameObject.tag == "Bullet" && !died)
        {
            playerStats.AddScore(5);
        }

        died = true;
    }
}
