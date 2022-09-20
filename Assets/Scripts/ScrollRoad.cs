using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRoad : ScrollDown
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        minZ = 0;
    }

    protected override void Update() {
        scrollSpeed = spawnManager.currentSpeed;
        base.Update();
    }

    public override void ManageOutOfBounds()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 40);
    }
}
