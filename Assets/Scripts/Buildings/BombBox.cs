using UnityEngine;
using System.Collections.Generic;

public class BombBox : CollisionComponent
{
    [SerializeField] private BuildBox buildBox;

    private void FixedUpdate()
    {
        CleanUpColliders();

        if (colliders.Count != 0)
        {
            TriggerBox triggerBox = null;

            foreach (Collider collider in colliders)
            {
                if (collider.tag == "TriggerBox")
                {
                    triggerBox = collider.GetComponent<TriggerBox>();
                }

                if (triggerBox != null)
                {
                    if (buildBox.BuildingStatus < 2)
                    {
                        buildBox.BuildingStatus = 3;
                    }

                    if (triggerBox.IsPlaced == false)
                    {
                        buildBox.BuildingStatus = 2;
                    }
                }
            }

            colliders.Clear();
        }
        else
        {
            buildBox.BuildingStatus = 3;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        AddCollider(collider);
    }

    public void Detonate()
    {
        FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(1f, 1f, "Sound_BombExplosion_1", "Sound_BombExplosion_2", "Sound_BombExplosion_3");

        //Particles.

        TriggerBox triggerBox = null;

        foreach (Collider collision in colliders)
        {
            if (collision.tag == "TriggerBox")
            {
                triggerBox = collision.GetComponent<TriggerBox>();

                triggerBox.Detonate();
            }
        }

        Destroy(transform.parent.gameObject);
    }
}