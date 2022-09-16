using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : ScrollDown
{
    public int score = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        minZ = -20;
    }

    public override void ManageOutOfBounds()
    {
        Destroy(gameObject);
    }
}
