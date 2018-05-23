using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {

    private const int max_len = 20;
    private int col = 0;
    private List<GameObject> maga = new List<GameObject>();
    private SteamVR_TrackedObject trackedObj;
    public GameObject Magazine;
    private GameObject magob;
    private SteamVR_Controller.Device Controller;
    private float distance = 0.5f;
    public float range;
    public int mag;
    private bool p;
    private int i;
    public int power;
    public string typefire;
    public float cooldown;

    void Awake()
    { if (gameObject.transform.parent != null)
        {
            
        }
    }

    void Start() {
        i = mag + 1;
    }

    // Update is called once per frame
    void Update() {
        if (gameObject.transform.parent != null)
        {
            if (trackedObj == null)
            {
                trackedObj = gameObject.transform.parent.GetComponent<SteamVR_TrackedObject>();
                Controller = SteamVR_Controller.Input((int)trackedObj.index);
            }
            if (Controller.GetHairTriggerDown() && trackedObj != null) {
                if (i > 0)
                {
                    if (typefire == "Semi")
                    {
                        i--;
                        //StartCoroutine("FireSemi");
                    }
                    else if (typefire == "Auto")

                    {
                        StartCoroutine("FireAuto");
                    }
                }
                else
                {
                    magob = gameObject.transform.Find("Magazine").gameObject;
                    StartCoroutine("maglife");
                    i = mag;
                }
            }
        }
    }

    //IEnumerator FireSemi () {
    //    RaycastHit hit;
    //    Vector3 fwd = GO.transform.TransformDirection(Vector3.right);
    //    Ray ray = new Ray(GO.transform.position, fwd);

    //    if (Physics.Raycast(ray, out hit, range))
    //    {
    //        if (hit.collider.tag == "Enemy")
    //        {
    //            Debug.Log(hit.collider.gameObject.name);
    //            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
    //            enemy.health -= power;
    //        }
    //    }
    //    i--;
    //    yield return null;

    //}

    //IEnumerator FireAuto()
    //{
    //    RaycastHit hit;
    //    Vector3 fwd = GO.transform.TransformDirection(Vector3.right);
    //    Ray ray = new Ray(GO.transform.position, fwd);

    //    if (Physics.Raycast(ray, out hit, range))
    //    {
    //        if (hit.collider.tag == "Enemy")
    //        {
    //            Debug.Log(hit.collider.gameObject.name);
    //            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
    //            enemy.health -= power;
    //        }
    //    }
    //    i--;
    //    yield return new WaitForSeconds(cooldown);

    //}

    IEnumerator maglife()
    {
        magob.name = "Old " + magob.transform.parent.name + " " + col.ToString();
        col++;
        maga.Add(magob);
        magob.AddComponent<Rigidbody>();
        magob.GetComponent<Rigidbody>().useGravity = true;
        magob.AddComponent<BoxCollider>();
        magob.GetComponent<BoxCollider>().size = new Vector3(25, 48, 88);
        magob.transform.SetParent(null);        
        yield return new WaitForSecondsRealtime(5);
        Destroy(magob);
    }
}
