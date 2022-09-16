using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int isMoving = 0;
    private bool isOnGround;
    private Rigidbody playerRb;
    private SpawnManager spawnManager;
    private MainManager mainManager;
    [SerializeField] private float speed = 0.25f;
    [SerializeField] private float jumpForce = 1;
    public List<float> xPosList;
    [SerializeField] private float roadWidth = 2.5f;
    public int roads = 3; //roads deve essere dispari
    private float currentXPos;
    private int currentXPosIndex;
    private int endXPosIndex;
    private float epsilon = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        xPosList = new List<float>();
        float leftmostX = -roadWidth * (roads - 1) / 2;
        for (int i = 0; i < roads; i++)
        {
            xPosList.Add(leftmostX + (i * roadWidth));
        }
        currentXPos = 0;
        currentXPosIndex = (roads - 1) / 2;


        spawnManager = GameObject.Find("Spawner").GetComponent<SpawnManager>();
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
        isMoving = 0;
        isOnGround = true;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnManager.isGameOver)
        {
            ManageInput();
            if (isMoving != 0)
            {
                ManageMovement();
            }

        }
    }

    private void ManageInput()
    {
        if (isMoving == 0 && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && transform.position.x < 2)
        {
            isMoving = 1;
        }
        else if (isMoving == 0 && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && transform.position.x > -2)
        {
            isMoving = -1;
        }
        else if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        endXPosIndex = xPosList.IndexOf(currentXPos) + isMoving;
        if (endXPosIndex < 0)
        {
            endXPosIndex = 0;
        }
        else if (endXPosIndex >= xPosList.Count)
        {
            endXPosIndex = xPosList.Count - 1;
        }
    }
    private void ManageMovement()
    {
        if (Mathf.Abs(transform.position.x - xPosList[endXPosIndex]) <= epsilon)
        {
            currentXPos = xPosList[endXPosIndex];
            transform.position = new Vector3(xPosList[endXPosIndex], transform.position.y, transform.position.z);
            isMoving = 0;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(xPosList[endXPosIndex], transform.position.y, transform.position.z), speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            spawnManager.isGameOver = true;
            mainManager.GameOver();
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            int scoreToAdd = other.gameObject.GetComponent<ScrollObject>().score;
            Destroy(other.gameObject);
            AddScore(scoreToAdd);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }


    private void AddScore(int score)
    {
        mainManager.AddScore(score);
    }
}