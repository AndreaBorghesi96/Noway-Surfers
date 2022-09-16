using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrollDown : MonoBehaviour
{
    private float initialTime = 0;
    public float scrollSpeed;
    private SpawnManager spawnManager;
    protected float minZ;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        spawnManager = GameObject.Find("Spawner").GetComponent<SpawnManager>();
        scrollSpeed = spawnManager.currentSpeed;
        initialTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnManager.isGameOver)
        {
            transform.Translate(Vector3.back * Time.deltaTime * scrollSpeed);
            if (transform.position.z < minZ)
            {
                ManageOutOfBounds();
            }
        }
    }

    public abstract void ManageOutOfBounds();
}
