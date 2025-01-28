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
        
        [SerializeField] private PlayerController player_01 = null;
        [SerializeField] private PlayerController player_02 = null;
        [SerializeField] private PlayerBuilderController playerBuilder_01 = null;
        [SerializeField] private PlayerBuilderController playerBuilder_02 = null;
        [SerializeField] private List<Transform> playerStartPositions = new();
        private GameObject getPlayer_01;
        private GameObject getPlayer_02;


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
            if (getPlayer_01 != null)
            {
                Destroy(getPlayer_01);
            }

            if (getPlayer_02 != null)
            {
                Destroy(getPlayer_02);
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

        private void StartBuilding()
        {
            gameState = GameState.Building;

            getPlayer_01 = Instantiate(playerBuilder_01).gameObject;
            getPlayer_02 = Instantiate(playerBuilder_02).gameObject;
            cameraController.player_01 = getPlayer_01;
            cameraController.player_02 = getPlayer_02;
        }

        private void StartPlaying()
        {
            gameState = GameState.Playing;

            getPlayer_01 = Instantiate(player_01, playerStartPositions[0]).gameObject;
            getPlayer_02 = Instantiate(player_02, playerStartPositions[1]).gameObject;
            cameraController.player_01 = getPlayer_01;
            cameraController.player_02 = getPlayer_02;

            player_01.OnDead = OnPlayerDead;
            player_01.OnScore = OnPlayerScored;
            player_02.OnDead = OnPlayerDead;
            player_02.OnScore = OnPlayerScored;
        }

        private void OnPlayerDead(PlayerController player)
        {
            if (player == player_01)
            {
                player_01.OnDead = null;
                player_01.OnScore = null;
                player_01 = null;
            }
            
            if (player == player_02)
            {
                player_02.OnDead = null;
                player_02.OnScore = null;
                player_02 = null;
            }

            if (player_01 == null && player_02 == null)
            {
                targetScore--;
                currentTime = 1;
            }
        }

        private void OnPlayerScored(PlayerController player)
        {
            if (player == player_01)
            {
                player_01.OnDead = null;
                player_01.OnScore = null;
                player_01 = null;
                score_01++;
            }
            
            if (player == player_02)
            {
                player_02.OnDead = null;
                player_02.OnScore = null;
                player_02 = null;
                score_02++;
            }
            
            if (player_01 == null && player_02 == null)
            {
                currentTime = 1;
            }
        }
    }
}
