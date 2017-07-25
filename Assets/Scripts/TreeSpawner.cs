using UnityEngine;
using System.Collections;

public class TreeSpawner : MonoBehaviour {

    public GameObject tree;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnTree", 0.0f, 2.5f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnTree()
    {
        GameObject clone = (GameObject)Instantiate(tree, transform.position, Quaternion.identity);
        Destroy(clone, 10.0f);
    }
}
