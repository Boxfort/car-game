using UnityEngine;
using System.Collections;

public class RoadTextureScript : MonoBehaviour {

    private const float ROAD_SPEED = 0.5f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 offset = new Vector2(Time.time * ROAD_SPEED, 0);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
