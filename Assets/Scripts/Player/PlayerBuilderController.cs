using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuilderController : MonoBehaviour
{
    [SerializeField] public int playerNumber = 0;

    private Vector3 targetPosition;
    
    [SerializeField] public GameObject[] buildings;
    private GameObject buildingInstance;

    private int maximumGridSteps = 10;

    private void Awake()
    {
        buildingInstance = Instantiate(buildings[UnityEngine.Random.Range(0, buildings.Length)], this.transform);
    }

    private void OnMovement(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        targetPosition = transform.position + new Vector3(movementVector.x, 0, movementVector.y);
        targetPosition = new Vector3(Mathf.Round(movementVector.x), 0, Mathf.Round(movementVector.y));
        transform.position += targetPosition;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -maximumGridSteps, maximumGridSteps), transform.position.y, Mathf.Clamp(transform.position.z, -maximumGridSteps, maximumGridSteps));
    }

    private void OnRotate()
    {
        FindObjectOfType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_RotateBuilding");

        transform.Rotate(0, 90f, 0);
    }

    private void OnConfirm()
    {
        FindObjectOfType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_RotateBuilding");

        Actions.PlayerBuilded?.Invoke(this);

        transform.DetachChildren();
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Colliding!");
    }
}
