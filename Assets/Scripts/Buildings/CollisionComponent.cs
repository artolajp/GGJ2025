using UnityEngine;
using System.Collections.Generic;

public class CollisionComponent : MonoBehaviour
{
    protected List<Collider> colliders = new List<Collider>();

    protected void AddCollider(Collider collider)
    {
        if (colliders.Contains(collider) == false)
        {
            colliders.Add(collider);
        }
    }

    protected void CleanUpColliders()
    {
        for (int iterator = colliders.Count - 1; iterator >= 0; iterator--)
        {
            if (colliders[iterator] == null)
            {
                colliders.RemoveAt(iterator);
            }
        }
    }
}
