using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immerse : MonoBehaviour
{
    public Material immersiveMat;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ImmersiveExperience(Collider coli)
    {
        Debug.Log(coli);
        Debug.Log("Switching Materials");
        RenderSettings.skybox = immersiveMat;   
    }
}
