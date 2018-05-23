using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    public Transform gunBase;
    public GameObject controllerModel;

    private GameObject grabbedObject;

    private SteamVR_TrackedObject trackedObj;
    
    private Collider[] Touchcollider;

    private bool grabbingFinished = true;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (grabbedObject == null && other.tag == "Gun")
        {
            Controller.TriggerHapticPulse(1000);

            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && !other.GetComponent<DebugGun>().IsAttached())
            {
                GrabObjet(other.gameObject);
                Debug.Log("Grabbed");
            }
        }
    }

    
    public void GrabObjet(GameObject gameObject)
    {
        if (grabbedObject != null)
        {
            DropObject();
        }

        grabbedObject = gameObject;

        controllerModel.SetActive(false);

        DebugGun debugGun = grabbedObject.GetComponent<DebugGun>();

        grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        grabbedObject.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        grabbedObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
        grabbedObject.gameObject.GetComponent<Collider>().enabled = false;

        grabbedObject.gameObject.transform.SetParent(gunBase);
        grabbedObject.gameObject.transform.localPosition = debugGun.handPosition;
        grabbedObject.gameObject.transform.localRotation = Quaternion.Euler(debugGun.handRotation);

        debugGun.AttachController(trackedObj);

        grabbingFinished = false;
    }

    public void DropObject()
    {
        if (grabbedObject == null) return;

        grabbedObject.gameObject.transform.SetParent(null);

        controllerModel.SetActive(true);

        grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        grabbedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        grabbedObject.gameObject.GetComponent<Collider>().enabled = true;

        grabbedObject.GetComponent<DebugGun>().DetachController();

        grabbedObject = null;
    }
    
    void Update () {
        if (grabbingFinished && Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && grabbedObject != null)
        {
            DropObject();
            Debug.Log("Dropped");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            grabbingFinished = true;
        }
    }
}