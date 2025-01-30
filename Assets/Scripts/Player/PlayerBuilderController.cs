using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuilderController : MonoBehaviour
{
    [SerializeField] public int playerNumber = 0;

    private Vector3 targetPosition;
    
    [SerializeField] public GameObject[] buildings;
    [SerializeField] private BuildingBuildBox buildingBuildBox;

    private int maximumGridSteps = 10;

    private void Awake()
    {
        GameObject getBuilding = Instantiate(buildings[UnityEngine.Random.Range(0, buildings.Length)], this.transform);
        buildingBuildBox = getBuilding.transform.Find("BuildBox").GetComponent<BuildingBuildBox>();
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
        FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_RotateBuilding");

        transform.Rotate(0, 90f, 0);
    }

    private void OnConfirm()
    {
        if (buildingBuildBox.GetCanBePlaced() == 1)
        {
            FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_RotateBuilding");

            Actions.PlayerBuilded?.Invoke(this);

            buildingBuildBox.SetCanBePlaced(2);
            buildingBuildBox.SetDontChange(true);
            transform.DetachChildren();
            Destroy(gameObject);
        }
        else
        {
            FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_DoorHandle_2");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Colliding!");
    }
}
