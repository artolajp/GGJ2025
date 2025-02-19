using UnityEngine;
using System.Collections.Generic;

public class BombBox : CollisionComponent
{
    [SerializeField] private BuildBox buildBox;
    [SerializeField] private GameObject particleExplotion_01;
    [SerializeField] private GameObject particleExplotion_02;

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
                    else
                    {
                        if (triggerBox.GetBuildBox.BuildingStatus == 3)
                        {
                            buildBox.BuildingStatus = 3;
                        }
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
        FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(1f, 1f, "Sound_BombExplosion_1", "Sound_BombExplosion_2", "Sound_BombExplosion_3");

        Instantiate(particleExplotion_01, transform.position, Quaternion.identity);
        Instantiate(particleExplotion_02, transform.position, Quaternion.identity);

        TriggerBox triggerBox = null;

        foreach (Collider collision in colliders)
        {
            if (collision.tag == "TriggerBox")
            {
                triggerBox = collision.GetComponent<TriggerBox>();

                triggerBox.Detonate();
            }
        }

        Actions.portalBuilded?.Invoke();

        Destroy(transform.parent.gameObject);
    }
}