using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {

    public Animator am;
    public Mesh newMesh;

    // Use this for initialization
    void Start ()
    {
        am = GetComponent<Animator>();
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<MeshFilter>().mesh = newMesh;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "BadCar")
        {
            StartCoroutine(DestroyCar());
            StartCoroutine(MoveBack(3.0f));
        }
        else if(coll.gameObject.tag == "Collectable")
        {
            EventManagerScript.IncrementCollectable();
            Destroy(coll.gameObject);
        }
    }

    IEnumerator DestroyCar()
    {
        GameManagerScript.PauseGame();

        GetComponent<BoxCollider>().enabled = false;
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mr in children)
        {
            mr.enabled = false;
        }

        GameObject explosion = transform.FindChild("Explosion").gameObject;
        explosion.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(2.0f);

        gameObject.SetActive(false);
        EventManagerScript.EndGame();
    }

    IEnumerator MoveBack(float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            transform.Translate(new Vector3(-20.0f * Time.deltaTime, 0, 0));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
