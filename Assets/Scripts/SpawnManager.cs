using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private float zPos = 25;
    [SerializeField] private List<float> xPosList;
    [SerializeField] private List<float> yPosList;
    public bool isGameOver = false;
    public float currentSpeed;
    [SerializeField] private float initialSpeed;
    private float initialTime;
    private PlayerController playerController;
    private int roads;
    [SerializeField] private List<GameObject> coins;
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private List<GameObject> powerups;
    private int[] coinPos;
    // 0,1  1,1  2,1
    // 0,0  1,0  2,0

    // Start is called before the first frame update
    void Start()
    {
        InitializePosList();
        InitializeCoinPos();

        initialTime = Time.time;
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
            SpawnCoin(); // ABSTRACTION
            SpawnPowerup(); // ABSTRACTION
            SpawnObstacles(); // ABSTRACTION

            yield return new WaitForSeconds(spawnRate);
        }
    }
    private void SpawnCoin()
    {
        int lastX = coinPos[0];
        int lastY = coinPos[1];
        int changingAxis = Random.Range(0, 3); // 0 x-axis, 1 y-axis, 2 no axis
        switch(changingAxis){
            case 0:
                if(lastX > 0) {
                    if(lastX < roads - 1) {
                        if(Random.Range(0, 2) == 0)
                        {
                            lastX += 1;
                        } else {
                            lastX -= 1;
                        }
                    } else {
                        lastX -= 1;
                    }
                } else {
                    lastX += 1;
                }
                break;
            case 1:
                lastY = (lastY + 1) % 2;
                break;
            case 2:
                // do nothing
                break;
        }
        coinPos[0] = lastX;
        coinPos[1] = lastY;
        int coinToSpawn = Random.Range(0, coins.Count);
        GameObject spawnedObject = Instantiate(coins[coinToSpawn]);
        spawnedObject.transform.position = PositionFromCoords(lastX, lastY);
        if(lastY > 0)
        {
            spawnedObject.GetComponent<ScrollObject>().Score *= 2;
        }
    }
    private void SpawnPowerup()
    {

    }
    private void SpawnObstacles()
    {
        bool isObstacleSpawning = Random.Range(0, 100) < 25;
        if(isObstacleSpawning) {
            int obstaclesToSpawn = Random.Range(0, roads);
            List<int> possibleXs = Enumerable.Range(0, roads).ToList();
            Shuffle<int>(possibleXs);
            List<int> chosenXs = possibleXs.GetRange(0, obstaclesToSpawn);

            for(int i = 0; i < obstaclesToSpawn; i++)
            {
                int y = Random.Range(0, 2);
                int obstacleToSpawn = Random.Range(0, obstacles.Count);
                GameObject spawnedObject = Instantiate(obstacles[obstacleToSpawn]);
                if(OverlapsCoin(chosenXs[i], y))
                {
                    y = (y + 1) % 2;
                }
                spawnedObject.transform.position = PositionFromCoords(chosenXs[i], y);
            }

        }
    }

    private bool OverlapsCoin(int x, int y)
    {
        return (x == coinPos[0] && y == coinPos[1]);
    }

    public static void Shuffle<T>(IList<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    private Vector3 PositionFromCoords(int x, int y)
    {
        Vector3 pos = new Vector3(xPosList[x], yPosList[y], zPos);
        return pos;
    }
    private void InitializeCoinPos()
    {
        coinPos = new int[2];
        coinPos[0] = 1;
        coinPos[1] = 0;
    }

    private void InitializePosList()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        xPosList = playerController.XPosList;
        yPosList = new List<float>();
        yPosList.Add(0.5f);
        yPosList.Add(2.5f);
        roads = playerController.Roads;
    }
}
