using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : ScrollDown // INHERITANCE
{
    [SerializeField] private int score = 0;
    public int Score // ENCAPSULATION
    {
        get => score; // get => score;
        set {
            score = value;
        }
    }
    // Start is called before the first frame update
    protected override void Start() // POLYMORPHISM
    {
        base.Start();
        if(scrollSpeed == 0) {
            Destroy(gameObject);
        }
        minZ = -20;
    }

    public override void ManageOutOfBounds() // POLYMORPHISM
    {
        Destroy(gameObject);
    }
}
