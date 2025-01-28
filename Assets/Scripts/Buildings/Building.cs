using System;
using System.Collections.Generic;
using UnityEngine;
using GGJ2025;

public class Building : MonoBehaviour
{
    private bool canBePlaced = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox")
        {
            canBePlaced = true;
        }
        else
        {
            canBePlaced = false;
        }
    }

    public bool CanBePlaced()
    {
        return canBePlaced;
    }
}