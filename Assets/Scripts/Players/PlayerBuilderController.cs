using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerBuilderController : MonoBehaviour
{
    [SerializeField] public int playerNumber = 0;

    private Vector3 targetPosition;
    private bool inputDelay = false;
    
    [SerializeField] public GameObject[] buildings;
    private BuildBox buildBox;
    private BombBox bombBox;
    private TriggerBox triggerBox;

    private bool bombMode = false;

    private int maximumGridSteps = 10;

    private void Awake()
    {
        int getNumber = GetRandomBuilding();

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

    private int GetRandomBuilding()
    {
        int getScore = playerNumber == 0 ? GameData.Score_01 : GameData.Score_02;
        int getBuilding = UnityEngine.Random.Range(0, buildings.Length);

        if (getScore < 4)
        {
            if (getBuilding == 0)
            {
                getBuilding += UnityEngine.Random.Range(1, 4);
            }
        }

        if (getScore > 6 && getScore < 8)
        {
            if (getBuilding != 0 && getBuilding < 5)
            {
                getBuilding += UnityEngine.Random.Range(4, 6);
            }
        }

        getBuilding = Mathf.Clamp(getBuilding, 0, buildings.Length - 1);

        return getBuilding;
    }

    private void OnMovement(InputValue movementValue)
    {
        if (inputDelay == true)
        {
            return;
        }

        StartCoroutine("InputDelay");

        FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(1f, 1f, "Sound_DeskClick_1", "Sound_DeskClick_2", "Sound_DeskClick_3");

        Vector2 movementVector = movementValue.Get<Vector2>();

        targetPosition = transform.position + new Vector3(movementVector.x, 0, movementVector.y);
        targetPosition = new Vector3(Mathf.Round(movementVector.x), 0, Mathf.Round(movementVector.y));
        transform.position += targetPosition;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -maximumGridSteps, maximumGridSteps), transform.position.y, Mathf.Clamp(transform.position.z, -maximumGridSteps, maximumGridSteps));
    }

    private void OnRotate()
    {
        if (inputDelay == true)
        {
            return;
        }

        StartCoroutine("InputDelay");

        FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.2f, 1.2f, "Sound_RotateBuilding_1", "Sound_RotateBuilding_2", "Sound_RotateBuilding_3");

        transform.Rotate(0, 90f, 0);
    }

    private void OnConfirm()
    {
        if (inputDelay == true)
        {
            return;
        }

        StartCoroutine("InputDelay");

        if (bombMode == false)
        {
            if (buildBox.BuildingStatus == 1)
            {
                Actions.PlayerBuilded?.Invoke(this);

                FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.2f, 1.2f, "Sound_BuildPlaced_1", "Sound_BuildPlaced_2", "Sound_BuildPlaced_3");

                buildBox.BuildingStatus = 2;
                triggerBox.IsPlaced = true;
                transform.DetachChildren();
                Destroy(gameObject);
            }
            else
            {
                FindAnyObjectByType<AudioManager>().AudioPlaySoundVariation(0.5f, 1.5f, "Sound_Can'tPlaceBuilding");
            }
        }
        else
        {
            Actions.PlayerBuilded?.Invoke(this);

            bombBox.Detonate();
            Destroy(gameObject);
        }
    }

    IEnumerator InputDelay()
    {
        inputDelay = true;

        yield return new WaitForSeconds(0.02f);

        inputDelay = false;
    }
}