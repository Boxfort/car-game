using UnityEngine;
using System.Collections;

public class FenceSpawner : MonoBehaviour {

    public GameObject fence;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnFence", 0.0f, 3.75f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnFence()
    {
        GameObject clone = (GameObject)Instantiate(fence, transform.position, Quaternion.identity);
    }
}
