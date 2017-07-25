using UnityEngine;
using System.Collections;

public class SceneryScript : MonoBehaviour {

    private const float MOVEMENT_SPEED = 20.0f;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 8.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(new Vector3(0, 0, -MOVEMENT_SPEED * Time.deltaTime));
    }
}
