using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuilderController : MonoBehaviour
{
    [SerializeField] public int playerNumber = 0;

    private Vector3 targetPosition;
    
    [SerializeField] public GameObject[] buildings;
    private BuildBox buildBox;
    private BombBox bombBox;
    private TriggerBox triggerBox;

    private bool bombMode = false;

    private int maximumGridSteps = 10;

    [SerializeField] public int DeleteObject = 0;//DELETE

    private void Awake()
    {
        int getNumber = DeleteObject;// UnityEngine.Random.Range(0, buildings.Length);

        GameObject getBuilding = Instantiate(buildings[getNumber], this.transform);

        if (getNumber == 0)
        {
            bombMode = true;
            bombBox = getBuilding.transform.Find("BombBox").GetComponent<BombBox>();
        }
        else
        {
            buildBox = getBuilding.transform.Find("BuildBox").GetComponent<BuildBox>();
            triggerBox = getBuilding.transform.Find("TriggerBox").GetComponent<TriggerBox>();
        }
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
        Actions.PlayerBuilded?.Invoke(this);

        if (bombMode == false)
        {
            if (buildBox.BuildingStatus == 1)
            {
                FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_RotateBuilding");

                buildBox.BuildingStatus = 2;
                triggerBox.IsPlaced = true;
                transform.DetachChildren();
                Destroy(gameObject);
            }
            else
            {
                FindAnyObjectByType<AudioController>().AudioPlaySoundVariation(0.5f, 1.2f, "Sound_DoorHandle_2");
            }
        }
        else
        {
            bombBox.Detonate(); // Only if its placed dude!
            Destroy(gameObject);
        }
    }
}