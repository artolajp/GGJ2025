using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerBuilderController : MonoBehaviour
{
    [SerializeField] private int playerNumber = 0;

    private Vector3 targetPosition;
    private bool inputDelay = false;
    
    [SerializeField] public GameObject[] buildings;
    private int buildingNumber = 0;
    private BuildBox buildBox;
    private BombBox bombBox;
    private TriggerBox triggerBox;

    private bool bombMode = false;

    private int maximumGridSteps = 10;

    private static bool bombExtraChance = false;

    public int PlayerNumber
    {
        get { return playerNumber; }
        set { playerNumber = value; }
    }

    public static bool BombExtraChance
    {
        get { return bombExtraChance; }
        set { bombExtraChance = value; }
    }

    private void Awake()
    {
        buildingNumber = GetRandomBuilding();

        GameObject getBuilding = Instantiate(buildings[buildingNumber], this.transform);
        buildBox = getBuilding.transform.Find("BuildBox").GetComponent<BuildBox>();

        if (buildingNumber == 0)
        {
            bombMode = true;
            bombBox = getBuilding.transform.Find("BombBox").GetComponent<BombBox>();
        }
        else
        {
            triggerBox = getBuilding.transform.Find("TriggerBox").GetComponent<TriggerBox>();
        }
    }

    private int GetRandomBuilding()
    {
        int getScore = playerNumber == 0 ? GameData.Score_01 : GameData.Score_02;
        int getScoreFraction = (int) UnityEngine.Mathf.Round((GameData.TargetScore - 1) / 5);
        int getBuilding = -1;

        getScore--;

        if (getScore <= getScoreFraction)
        {
            getBuilding = UnityEngine.Random.Range(1, 5);

            if (UnityEngine.Random.Range(0, 8) == 0)
            {
                getBuilding = 5;
            }
        }

        if (getScore > getScoreFraction && getScore <= getScoreFraction * 2)
        {
            getBuilding = UnityEngine.Random.Range(6, 14);

            if (UnityEngine.Random.Range(0, 8) == 0)
            {
                getBuilding = 14;
            }
        }

        if (getScore > getScoreFraction * 2 && getScore <= getScoreFraction * 3)
        {
            getBuilding = UnityEngine.Random.Range(1, 15);

            if (UnityEngine.Random.Range(0, 4) == 0)
            {
                getBuilding = 15;
            }

            if (getBuilding < 5)
            {
                getBuilding += UnityEngine.Random.Range(0, 8);
            }
        }

        if (getScore > getScoreFraction * 3 && getScore <= getScoreFraction * 4)
        {
            getBuilding = UnityEngine.Random.Range(1, buildings.Length);

            if (UnityEngine.Random.Range(0, 2) < 2)
            {
                getBuilding = UnityEngine.Random.Range(15, 18);

                if (UnityEngine.Random.Range(0, 4) == 0)
                {
                    getBuilding = 7;

                    if (UnityEngine.Random.Range(0, 5) == 0)
                    {
                        getBuilding = 15;
                    }
                }
            }

            if (UnityEngine.Random.Range(0, 5) == 0)
            {
                getBuilding = 6;
            }

            if (getBuilding < 5)
            {
                getBuilding += UnityEngine.Random.Range(0, 8);
            }
        }

        if (getScore > getScoreFraction * 4 && getScore <= getScoreFraction * 5)
        {
            getBuilding = UnityEngine.Random.Range(6, buildings.Length);

            if (UnityEngine.Random.Range(0, 2) < 2)
            {
                getBuilding = UnityEngine.Random.Range(15, 17);

                if (UnityEngine.Random.Range(0, 3) == 0)
                {
                    getBuilding = 18;
                }
            }

            if (getBuilding < 5)
            {
                getBuilding += UnityEngine.Random.Range(4, 10);
            }
        }

        if (bombExtraChance == true)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                getBuilding = 0;
            }
        }

        getBuilding = Mathf.Clamp(getBuilding, 0, buildings.Length - 1);

        //[DEBUG]:
        //getBuilding = playerNumber == 0 ? 15 : 14;

        return getBuilding;
    }

    private void OnMovement(InputValue movementValue)
    {
        if (inputDelay == true)
        {
            return;
        }

        StartCoroutine("InputDelay");

        FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(1f, 1f, "Sound_DeskClick_1", "Sound_DeskClick_2", "Sound_DeskClick_3");

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

        FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(0.2f, 1.2f, "Sound_RotateBuilding_1", "Sound_RotateBuilding_2", "Sound_RotateBuilding_3");

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
                Actions.playerBuilded?.Invoke(this);

                FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(0.2f, 1.2f, "Sound_BuildPlaced_1", "Sound_BuildPlaced_2", "Sound_BuildPlaced_3");

                buildBox.BuildingStatus = 2;
                triggerBox.IsPlaced = true;

                Instantiate(triggerBox.ParticleBuildPlaced, transform.position, transform.rotation);

                // New portal builded.
                if (buildingNumber == 15)
                {
                    Actions.portalBuilded?.Invoke();
                }

                transform.DetachChildren();
                Destroy(gameObject);
            }
            else
            {
                FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(0.5f, 1.5f, "Sound_Can'tPlaceBuilding");
            }
        }
        else
        {
            if (buildBox.BuildingStatus == 3)
            {
                Actions.playerBuilded?.Invoke(this);

                bombBox.Detonate();
                Destroy(gameObject);
            }
            else
            {
                FindAnyObjectByType<AudioManager>().AudioPlaySoundWithSource(0.5f, 1.5f, "Sound_Can'tPlaceBuilding");
            }
        }
    }

    private IEnumerator InputDelay()
    {
        inputDelay = true;

        yield return new WaitForSeconds(0.02f);

        inputDelay = false;
    }
}