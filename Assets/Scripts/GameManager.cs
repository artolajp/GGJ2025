using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GGJ2025
{
    public class GameManager : MonoBehaviour
    {
        enum GameState {Start ,Playing, Building, Score, EndScreen, StartScreen}
        
        [SerializeField] GameState gameState = GameState.Start;
        [SerializeField] private CameraController cameraController = null;
        
        [SerializeField] private int score_01 = 0;
        [SerializeField] private int score_02 = 0;
        [SerializeField] private int targetScore = 0;
        
        [SerializeField] private GameObject[] players;
        [SerializeField] private List<Transform> playerStartPositions = new();
        private GameObject player_01;
        private GameObject player_02;

        [SerializeField] private GameObject cells;


        [SerializeField] ScorePanel scorePanel = null;

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

                case GameState.Playing when score_01 < targetScore && score_02 < targetScore:
                {
                    scorePanel.Show(score_01, score_02, targetScore, () => {});
                    currentTime = 2f;
                    gameState = GameState.Score;
                }
                break;

                case GameState.Playing:
                {
                    scorePanel.Show(score_01, score_02, targetScore, () =>
                    {
                        if (score_01 > score_02)
                        {
                            SceneManager.LoadScene("WinGreen");
                        }
                        else if (score_02 > score_01)
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

                targetScore--;
                currentTime = 1;
            }
        }

        private void OnPlayerScored(PlayerController player)
        {
            if (player.playerNumber == 0)
            {
                player_01 = null;
                score_01++;
            }
            
            if (player.playerNumber == 1)
            {
                player_02 = null;
                score_02++;
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

                cells.SetActive(false);

                currentTime = 1;
            }
        }
    }
}
