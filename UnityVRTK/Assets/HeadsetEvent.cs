using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetEvent : MonoBehaviour {
    public Material matImmersion;
    public List<GameObject> dissappearObjects;
    public GameObject immersiveEnvrionment;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SomethingHappened()
    {
        RenderSettings.skybox = matImmersion;
        immersiveEnvrionment.SetActive(true);
        for(int i = 0; i < dissappearObjects.Count; i++)
        {
            dissappearObjects[i].SetActive(false);

        }


    }
}
