using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private List<GameObject> objectsToSpawn;
    private List<GameObject> objectsWithProbability;
    [SerializeField] private float[] yPosArr = { 0.5f, 3.0f };
    [SerializeField] private float zPos = 25;
    [SerializeField] private List<float> xPosList;
    private int lastXIndex = 1;
    public bool isGameOver = false;
    public float currentSpeed;
    [SerializeField] private float initialSpeed;
    private float initialTime;
    private PlayerController playerController;
    private int roads;
    // Start is called before the first frame update
    void Start()
    {
        initialTime = Time.time;
        FillXPosList();
        FillObjects();
        StartCoroutine("SpawnObjects");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            float currentTime = Time.time - initialTime;
            currentSpeed = initialSpeed + 7 * (1 - 1 / Mathf.Pow(currentTime / 1000 + 1, 5));
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (!isGameOver)
        {
            GameObject objectToSpawn = objectsWithProbability[GetRandomIndex()];
            int numberToSpawn = 1;
            List<float> spawnPositions = new List<float>();
            if (objectToSpawn.CompareTag("Obstacle"))
            {
                numberToSpawn = Random.Range(1, roads);
                for (int i = 0; i < numberToSpawn; i++)
                {
                    spawnPositions.Add(xPosList[Random.Range(0, xPosList.Count)]);
                }
            }
            else
            {
                spawnPositions.Add(xPosList[GetRandomXPos()]);
            }

            foreach (float xPos in spawnPositions)
            {
                GameObject spawnedObject = Instantiate(objectToSpawn);
                spawnedObject.transform.position = new Vector3(xPos, yPosArr[GetRandomYPos()], zPos);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private int GetRandomIndex()
    {
        return Random.Range(0, objectsWithProbability.Count);
    }
    private int GetRandomXPos()
    {
        lastXIndex = Random.Range(Mathf.Max(0, lastXIndex - 1), Mathf.Min(2, lastXIndex + 1) + 1);
        return lastXIndex;
    }
    private int GetRandomYPos()
    {
        return Random.Range(0, 2);
    }

    private void FillObjects()
    {
        objectsWithProbability = new List<GameObject>();
        foreach (GameObject obj in objectsToSpawn)
        {
            if (obj.CompareTag("Obstacle"))
            {
                for (int i = 0; i < 2; i++)
                {
                    objectsWithProbability.Add(obj);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    objectsWithProbability.Add(obj);
                }
            }
        }
    }

    private void FillXPosList()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        xPosList = playerController.xPosList;
        roads = playerController.roads;
    }
}
