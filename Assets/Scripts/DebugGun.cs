using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGun : MonoBehaviour {
    public GameObject bulletObject;
    public GameObject shootingPoint;

    public Vector3 handPosition;
    public Vector3 handRotation;

    public float impulse;
    public float cooldownTime;
    private float currentCooldownTime;

    private SteamVR_TrackedObject attachedController = null;

    private SteamVR_Controller.Device Controller
    {
        get {
            return SteamVR_Controller.Input((int)attachedController.index);
        }
    }
	
	void Update () {
        if (currentCooldownTime != 0)
        {
            currentCooldownTime = Mathf.Max(currentCooldownTime - Time.deltaTime, 0);
        }

        if (attachedController != null && Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && currentCooldownTime == 0) {
            Vector3 direction = shootingPoint.transform.up;

            GameObject bullet = Instantiate(bulletObject);
            bullet.transform.position = shootingPoint.transform.position;
            bullet.transform.forward = direction;

            bullet.GetComponent<Rigidbody>().AddForce(direction * impulse, ForceMode.Impulse);

            Controller.TriggerHapticPulse(3999);
            GetComponent<Animation>().Stop();
            GetComponent<Animation>()["pm_shoot"].speed = 10;
            GetComponent<Animation>().Play(PlayMode.StopAll);

            currentCooldownTime = cooldownTime;
        }
	}

    public bool IsAttached()
    {
        return attachedController != null;
    }

    public void AttachController(SteamVR_TrackedObject controller)
    {
        attachedController = controller;
    }

    public void DetachController()
    {
        attachedController = null;
    }
}
