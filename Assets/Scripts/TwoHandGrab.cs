using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandGrab : MonoBehaviour {
    public DebugGun gunConmponent;
    public GameObject pivotPoint;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Controller" && gunConmponent.IsAttached())
        {
            SteamVR_TrackedObject trackedObject = collider.gameObject.GetComponent<SteamVR_TrackedObject>();
            SteamVR_Controller.Device controller = SteamVR_Controller.Input((int)trackedObject.index);

            controller.TriggerHapticPulse(100);
            if (controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                gunConmponent.transform.forward = trackedObject.transform.position - gunConmponent.transform.position;
            }
        }
    }

    void Start () {
    }
	
	void Update () {
    }
}
