using UnityEngine;
using System.Collections.Generic;

public class TriggerBox : CollisionComponent
{
    [SerializeField] private BuildBox buildBox;
    [SerializeField] private bool isStatic = false;
    private bool isPlaced = false;

    public bool BuildBox
    {
        get { return buildBox; }
    }

    public bool IsPlaced
    {
        get { return isPlaced; }
        set { isPlaced = value; }
    }

    private void FixedUpdate()
    {
        if (isStatic == true)
        {
            return;
        }

        CleanUpColliders();

        if (colliders.Count != 0)
        {
            int bombsNumber = 0;

            foreach (Collider collider in colliders)
            {
                if (bombsNumber == 0)
                {
                    buildBox.BuildingStatus = 2;
                }

                if (isPlaced == true)
                {
                    if (collider.tag == "BombBox")
                    {
                        bombsNumber++;
                        buildBox.BuildingStatus = 3;
                    }
                }
            }

            colliders.Clear();
        }
        else
        {
            if (isPlaced == false)
            {
                buildBox.BuildingStatus = 1;
            }
            else
            {
                if (buildBox.BuildingStatus != 2)
                {
                    buildBox.BuildingStatus = 2;
                }
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (isStatic == true)
        {
            return;
        }

        AddCollider(collider);
    }

    public void Detonate()
    {
        if (isStatic == true)
        {
            return;
        }

        if (isPlaced == true)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}