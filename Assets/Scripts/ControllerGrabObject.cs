using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;
    private Collider collide;

    private bool Taken = false;

    private Collider[] Touchcollider;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        collide = col;
        collidingObject = col.gameObject;   
        col.gameObject.transform.SetParent(gameObject.transform);
        collide.attachedRigidbody.useGravity = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        col.gameObject.transform.position = gameObject.transform.position;
        collide.attachedRigidbody.useGravity = false;
        collidingObject.GetComponent<Collider>().enabled = false;
        collidingObject.GetComponent<DebugGun>().AttachController(trackedObj);
    }

    public void OnTriggerEnter(Collider other)
    {

    }

     public void OnTriggerStay(Collider other)
    {
        if (Controller.GetHairTrigger() && !Taken && other.tag == "Gun")
        {
            SetCollidingObject(other);
            Taken = true;
            print("Triggered " + other.name);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (Taken) { collide.gameObject.transform.position = gameObject.transform.position; }
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip) && Taken)
        {
            Taken = false;
            collidingObject.gameObject.transform.SetParent(null);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            collidingObject.GetComponent<Collider>().enabled = true;
            collide.attachedRigidbody.useGravity = true;
            collidingObject.GetComponent<DebugGun>().DetachController();
        }

        //if (Controller.GetHairTrigger() && !Taken)
        //{
        //    Touchcollider = Physics.OverlapSphere(trackedObj.transform.position, 0.005f);

        //    foreach (Collider col in Touchcollider) {
        //        if (col.gameObject.name == "pm" || col.gameObject.name == "ak") {
        //            GrabObject(col.gameObject);
        //            Taken = true;
        //            break;
        //        }
        //    }
        //}

        //if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        //{
        //    if (objectInHand)
        //    {
        //        ReleaseObject();
        //        Taken = false;
        //    }
        //}

    }

    IEnumerator half()
    {
        yield return new WaitForSeconds(2);
    }
}