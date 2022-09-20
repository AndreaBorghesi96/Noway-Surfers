using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRoad : ScrollDown // INHERITANCE
{
    // Start is called before the first frame update
    protected override void Start() // POLYMORPHISM
    {
        base.Start();
        minZ = 0;
    }

    protected override void Update() // POLYMORPHISM
    {
        scrollSpeed = spawnManager.currentSpeed;
        base.Update();
    }

    public override void ManageOutOfBounds() // POLYMORPHISM
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 40);
    }
}
