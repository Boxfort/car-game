using UnityEngine;
using System.Collections;

public class BadCarScript : MonoBehaviour {

    public float MOVEMENT_SPEED = 30.0f;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, 6.0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(MOVEMENT_SPEED * Time.deltaTime,0,0));
    }
}
