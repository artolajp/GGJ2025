using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuilderController : MonoBehaviour
{
    private int positionX = 0;
    private int positionY = 0;
    private Vector3 targetPosition;
    
    [SerializeField] public Building building;
    
    public Action<PlayerBuilderController> OnConfirmed;
    public Building BuildingInstance
    {
        get;
        set;
    }

    private void OnMovement(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        targetPosition = transform.position + new Vector3(movementVector.x, 0, movementVector.y);
        targetPosition = new Vector3(Mathf.Round(movementVector.x), 0, Mathf.Round(movementVector.y));
        transform.position += targetPosition;
    }

    private void OnRotate()
    {
        transform.Rotate(0, 90f, 0);
    }

    private void OnConfirm()
    {
        OnConfirmed?.Invoke(this);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Colliding!");
    }
}
