using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    enum GameState { Start, Playing, Building, Score, EndScreen, StartScreen }
    enum TimerMode { Playing, Building, Ruler, Overtime, TimeTransition, Transition }

    [SerializeField] GameState gameState = GameState.Start;
    [SerializeField] private CameraController cameraController = null;

    /*
    [SerializeField] private int score_01 = 2;
    [SerializeField] private int score_02 = 2;
    */

    [SerializeField] private int targetScore = 40;
    [SerializeField] ScorePanel scorePanel = null;
    [SerializeField] private GameObject cells;

    [SerializeField] private GameObject[] players;
    [SerializeField] private List<Transform> playerStartPositions = new();
    private GameObject player_01;
    private GameObject player_02;

    private float currentTime = 0;
    private float timer = 30f;
    private TimerMode timerMode = TimerMode.Playing;
    [SerializeField] private TMP_Text timerNumbersText;
    [SerializeField] private TMP_Text timerText;
    private string[] victoryTexts = new string[3] { "Good!", "Nice!", "Great!" };
    private string[] defeatTexts = new string[3] { "Oh!", "Pop!", "Auch!" };

    private void OnEnable()
    {
        Actions.rulerIsDone += Overtime;
    }

    private void OnDisable()
    {
        Actions.rulerIsDone -= Overtime;
    }

    private void Start()
    {
        ChangeState();
        StartClock();
    }

    private void StartClock()
    {
        currentTime = timer;
    }

    private void Update()
    {
        CalculateTimer();

        if (Input.GetKeyDown("space"))
        {
            ChangeState();
        }
    }

    private void CalculateTimer()
    {
        switch (timerMode)
        {
            case TimerMode.Playing:
            {
                currentTime -= Time.deltaTime;
                timerNumbersText.text = currentTime.ToString("00s");

                if (currentTime < 0)
                {
                    timerMode = TimerMode.Ruler;
                }
            }
            break;

            case TimerMode.Building:
            {
                currentTime -= Time.deltaTime;
                timerNumbersText.text = currentTime.ToString("00s");

                if (currentTime < 0)
                {
                    ChangeState();
                }
            }
            break;

        case TimerMode.Ruler:
            {
                timerNumbersText.text = "Run!";

                Actions.spelunkyTime?.Invoke(true);
            }
            break;

            case TimerMode.Overtime:
            {
                currentTime -= Time.deltaTime;
                timerNumbersText.text = currentTime.ToString("00s");

                if (currentTime < 0)
                {
                    ChangeState();
                }
            }
            break;

            case TimerMode.Transition:
            {
                currentTime -= Time.deltaTime;

                if (currentTime < 0)
                {
                    ChangeState();
                }
            }
            break;

            case TimerMode.TimeTransition:
            {
                currentTime -= Time.deltaTime;
                timerNumbersText.text = currentTime.ToString("00s");

                if (currentTime < 0)
                {
                    ChangeState();
                }
            }
            break;
        }
    }

    private void ChangeState()
    {
        if (player_01 != null)
        {
            Destroy(player_01);
        }

        if (player_02 != null)
        {
            Destroy(player_02);
        }

        currentTime = timer;

        switch (gameState)
        {
            case GameState.Start:
            {
                StartPlaying();
            }
            break;

            case GameState.Playing when GameData.Score_01 < targetScore && GameData.Score_02 < targetScore:
            {
                Actions.canShoot?.Invoke(false);

                scorePanel.Show(GameData.Score_01, GameData.Score_02, targetScore, () => {});

                timerText.text = "Scores:";

                currentTime = 2f;
                timerMode = TimerMode.TimeTransition;
                gameState = GameState.Score;
            }
            break;

            case GameState.Playing:
            {
                scorePanel.Show(GameData.Score_01, GameData.Score_02, targetScore, () =>
                {
                    if (GameData.Score_01 > GameData.Score_02)
                    {
                        SceneManager.LoadScene("WinGreen");
                    }
                    else if (GameData.Score_02 > GameData.Score_01)
                    {
                        SceneManager.LoadScene("WinRed");
                    }
                    else
                    {
                        SceneManager.LoadScene("Menu");
                    }
                });

                gameState = GameState.EndScreen;
            }
            break;

            case GameState.Score:
            {
                scorePanel.Hide();
                StartBuilding();
            }
            break;

            case GameState.Building:
            {
                StartPlaying();
            }
            break;

            default:
            {
                gameState = GameState.EndScreen;
            }
            break;
        }
    }

    private void StartPlaying()
    {
        timerText.text = "Time:";
        timerMode = TimerMode.Playing;

        Actions.canShoot?.Invoke(true);

        if (cells.activeSelf == true)
        {
            cells.SetActive(false);
        }

        gameState = GameState.Playing;

        player_01 = Instantiate(players[0], playerStartPositions[0].transform.position, Quaternion.identity);
        player_02 = Instantiate(players[1], playerStartPositions[1].transform.position, Quaternion.identity);
        cameraController.player_01 = player_01;
        cameraController.player_02 = player_02;

        Actions.PlayerDeath += OnPlayerDead;
        Actions.PlayerScored += OnPlayerScored;
    }

    private void StartBuilding()
    {
        timerText.text = "Building:";
        timerMode = TimerMode.Building;

        gameState = GameState.Building;

        player_01 = Instantiate(players[2]);
        player_02 = Instantiate(players[3]);
        cameraController.player_01 = player_01;
        cameraController.player_02 = player_02;
        cells.SetActive(true);

        Actions.PlayerBuilded += OnPlayerBuilded;
    }

    private void OnPlayerDead(PlayerController player)
    {
        if (player.PlayerNumber == 0)
        {
            player_01 = null;
        }

        if (player.PlayerNumber == 1)
        {
            player_02 = null;
        }

        if (player_01 == null && player_02 == null)
        {
            Actions.PlayerDeath -= OnPlayerDead;
            Actions.PlayerDeath -= OnPlayerScored;

            //targetScore--;
            Actions.spelunkyTime?.Invoke(false);

            timerNumbersText.text = defeatTexts[UnityEngine.Random.Range(0, defeatTexts.Length)];
            currentTime = 1f;
            timerMode = TimerMode.Transition;
        }
    }

    private void OnPlayerScored(PlayerController player)
    {
        if (player.PlayerNumber == 0)
        {
            player_01 = null;
            GameData.Score_01 += 1;
        }

        if (player.PlayerNumber == 1)
        {
            player_02 = null;
            GameData.Score_02 += 1;
        }

        if (player_01 == null && player_02 == null)
        {
            Actions.PlayerDeath -= OnPlayerDead;
            Actions.PlayerDeath -= OnPlayerScored;

            Actions.spelunkyTime?.Invoke(false);

            timerNumbersText.text = victoryTexts[UnityEngine.Random.Range(0, victoryTexts.Length)];
            currentTime = 1f;
            timerMode = TimerMode.Transition;
        }
    }

    private void OnPlayerBuilded(PlayerBuilderController player)
    {
        if (player.PlayerNumber == 0)
        {
            player_01 = null;
        }

        if (player.PlayerNumber == 1)
        {
            player_02 = null;
        }

        if (player_01 == null && player_02 == null)
        {
            Actions.PlayerBuilded -= OnPlayerBuilded;

            timerNumbersText.text = "Ready?";
            currentTime = 1f;
            timerMode = TimerMode.Transition;
        }
    }

    private void Overtime()
    {
        if (timerMode == TimerMode.Overtime || timerMode == TimerMode.TimeTransition || timerMode == TimerMode.Transition)
        {
            return;
        }

        timerText.text = "Overtime:";
        currentTime = 20f;
        timerMode = TimerMode.Overtime;
    }
}