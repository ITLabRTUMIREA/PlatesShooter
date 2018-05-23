using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateGunController : MonoBehaviour {
    public GameObject[] gunObjects = new GameObject[10];
    public float shootingInterval = 1.0f;
    public int platesPerCabin = 10;

    private float currentTime = 0.0f;
    private int currentGun = 0;
    private int platesElapsed = 0;
    private List<PlateGun> plateGuns = new List<PlateGun>();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < gunObjects.Length; ++i)
        {
            if (gunObjects[i] != null)
            {
                plateGuns.Add(gunObjects[i].GetComponent<PlateGun>());
            }
        }

        if (plateGuns.Count == 0)
        {
            Debug.LogError("No plates assigned to controller");
        }
    }
	
	// Update is called once per frame
	void Update () {
        handleKeyPress();

        currentTime += Time.deltaTime;

        if (currentTime >= shootingInterval)
        {
            handleCabin();
            currentTime = 0;
        }
	}

    private void handleKeyPress()
    {
        for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; ++key)
        {
            if (Input.GetKeyDown(key))
            {
                int i = key - KeyCode.Alpha0;
                PlateGun plateGunComponent;
                if (gunObjects[i] != null && (plateGunComponent = gunObjects[i].GetComponent<PlateGun>()) != null)
                {
                    plateGunComponent.Shoot();
                }
            }
        }
    }

    private void handleCabin()
    {
        if (platesElapsed == 0)
        {
            currentGun = Random.Range(0, plateGuns.Count);
            platesElapsed = platesPerCabin;
        }

        plateGuns[currentGun].Shoot();
        platesElapsed--;
    }
}
