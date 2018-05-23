using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateGun : MonoBehaviour {
    public GameObject plateGun;
    public GameObject plateObject;
    public float impulse;
    public float cooldownTime;
    private float currentCooldownTime;
	
	void Update () {
		if (currentCooldownTime != 0)
        {
            currentCooldownTime = Mathf.Max(currentCooldownTime - Time.deltaTime, 0);
        }
	}

    public void Shoot()
    {
        if (currentCooldownTime == 0)
        {
            GameObject plate = Instantiate(plateObject);
            plate.transform.position = transform.position;
            plate.transform.rotation = transform.rotation * Quaternion.Euler(90, 0, 0);

            Vector3 direction = -plateGun.transform.forward;
            plate.GetComponent<Rigidbody>().AddForce(direction * impulse, ForceMode.Impulse);

            currentCooldownTime = cooldownTime;
        }
    }
}
