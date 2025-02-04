using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    enum GameState { Start, Playing, Building, Score, EndScreen, StartScreen }

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
    private float Timer = 15000;
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        ChangeState();
        StartClock();
    }

    private void StartClock()
    {
        currentTime = Timer;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0 || Input.GetKeyDown("space"))
        {
            ChangeState();
        }

        timerText.text = currentTime.ToString("00s");
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

        currentTime = Timer;

        switch (gameState)
        {
            case GameState.Start:
            {
                StartPlaying();
            }
            break;

            case GameState.Playing when GameData.Score_01 < targetScore && GameData.Score_02 < targetScore:
            {
                scorePanel.Show(GameData.Score_01, GameData.Score_02, targetScore, () => {});
                currentTime = 2f;
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
        gameState = GameState.Building;

        player_01 = Instantiate(players[2]);
        player_02 = Instantiate(players[3]);
        cameraController.player_01 = player_01;
        cameraController.player_02 = player_02;
        cells.SetActive(true);

        Actions.PlayerBuilded += OnPlayerBuilded;
    }

    public void OnPlayerDead(PlayerController player)
    {
        if (player.playerNumber == 0)
        {
            player_01 = null;
        }

        if (player.playerNumber == 1)
        {
            player_02 = null;
        }

        if (player_01 == null && player_02 == null)
        {
            Actions.PlayerDeath -= OnPlayerDead;
            Actions.PlayerDeath -= OnPlayerScored;

            //targetScore--;
            currentTime = 1;
        }
    }

    private void OnPlayerScored(PlayerController player)
    {
        if (player.playerNumber == 0)
        {
            player_01 = null;
            GameData.Score_01 += 1;
        }

        if (player.playerNumber == 1)
        {
            player_02 = null;
            GameData.Score_02 += 1;
        }

        if (player_01 == null && player_02 == null)
        {
            Actions.PlayerDeath -= OnPlayerDead;
            Actions.PlayerDeath -= OnPlayerScored;

            currentTime = 1;
        }
    }

    public void OnPlayerBuilded(PlayerBuilderController player)
    {
        if (player.playerNumber == 0)
        {
            player_01 = null;
        }

        if (player.playerNumber == 1)
        {
            player_02 = null;
        }

        if (player_01 == null && player_02 == null)
        {
            Actions.PlayerBuilded -= OnPlayerBuilded;

            currentTime = 1;
        }
    }
}