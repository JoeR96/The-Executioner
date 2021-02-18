using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformState : MonoBehaviour
{
    public int X;
    public int Z;
    private bool platformInUse;

    public void SetState()
    {
        platformInUse = !platformInUse;
    }

    public bool ReturnState()
    {
        return platformInUse;
    }

    public void Setint(int x, int z)
    {
        X = x;
        Z = z;
    }
}


