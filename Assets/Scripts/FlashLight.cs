using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {

    public float offset = 0.0f;

    private Light light;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        InvokeRepeating("ToggleLight", offset, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ToggleLight()
    {
        if (light.enabled)
            light.enabled = false;
        else
            light.enabled = true;
    }
}
