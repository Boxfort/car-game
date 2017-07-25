using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour {

    public GameObject[] vehicles;
    public GameObject[] obstacles;

    public GameObject collectable;

    private float[,] positions;
    private const float LANE_SIZE = 2.0f;
    private bool canSpawn = true;

	// Use this for initialization
	void Start ()
    {
        positions = new float[,] { { 0, -1 },
                                   { 1, -1 },
                                   { 2, -1 },
                                   { 3, -1 },
                                   { 4, -1 },
                                   { 0,  1 },
                                   { 0,  4 },
                                   { 0,  4 },
                                   { 1,  4 },
                                   { 2,  4 },
                                   { 3,  4 }};

        InvokeRepeating("SpawnCollectable", 1.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (canSpawn)
            SelectObstacle();
	}

    void SpawnCollectable()
    {
        int lane = Random.Range(0, 5);

        Instantiate(collectable, new Vector3(lane * LANE_SIZE, .5f , transform.position.z) , Quaternion.AngleAxis(90, Vector3.up));
    }

    void SelectObstacle()
    {
        canSpawn = false;

        int rand = Random.Range(0, 2);

        switch(rand)
        {
            case 0:
                StartCoroutine(SpawnCars(2.0f));
                break;
            case 1:
                StartCoroutine(SpawnObstacle(3.0f));
                break;
        }     
    }

    IEnumerator SpawnCars(float time)
    {
        int layoutNum = Random.Range(0, positions.Length / 2);
        int carNum = Random.Range(0, vehicles.Length);

        Vector3 position = new Vector3(positions[layoutNum, 0] * LANE_SIZE, 0.16f, transform.position.z);
        Instantiate(vehicles[carNum], position, Quaternion.AngleAxis(90, Vector3.up));

        if (positions[layoutNum, 1] != -1)
        {
            carNum = Random.Range(0, vehicles.Length);
            Vector3 position2 = new Vector3(positions[layoutNum, 1] * LANE_SIZE, 0.16f, transform.position.z);
            Instantiate(vehicles[carNum], position2, Quaternion.AngleAxis(90, Vector3.up));
        }

        yield return new WaitForSeconds(time);
        canSpawn = true;
    }

    IEnumerator SpawnObstacle(float time)
    {
        int obstacleNum = Random.Range(0, obstacles.Length);

        Vector3 position = new Vector3(4, 0.16f, transform.position.z);
        Instantiate(obstacles[obstacleNum], position, Quaternion.AngleAxis(90, Vector3.up));

        yield return new WaitForSeconds(time);
        canSpawn = true;
    }
}
