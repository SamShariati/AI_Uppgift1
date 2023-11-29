using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;

    protected override Node SetupTree()
    {
        throw new System.NotImplementedException();
    }
}
