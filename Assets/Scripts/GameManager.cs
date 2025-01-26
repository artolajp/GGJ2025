using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GGJ2025
{
    public class GameManager : MonoBehaviour
    {
        enum GameState {Init ,Playing, Building, Score, EndScreen, StartScreen}
        
        [SerializeField] GameState gameState = GameState.Init;
        [SerializeField] private List<Building> initialBuildings = new List<Building>();
        [SerializeField] private List<Building> buildings = new List<Building>();
        [SerializeField] private PlacementManager placementManager = null;
        [SerializeField] private CameraController cameraController = null;
        
        [SerializeField] private int currentScoreP1 = 0;
        [SerializeField] private int currentScoreP2 = 0;
        [SerializeField] private int targetScore = 0;
        
        [FormerlySerializedAs("player1Controller")]
        [SerializeField] private PlayerController player1ControllerPrefab = null;
        [FormerlySerializedAs("player2Controller")]
        [SerializeField] private PlayerController player2ControllerPrefab = null;
        [SerializeField] private PlayerBuilderController player1BuilderController = null;
        [SerializeField] private PlayerBuilderController player2BuilderController = null;
        [SerializeField] private List<Transform> playerStartPositions = new();
        [SerializeField] ScorePanel scorePanel = null;  
        
        GameObject p1;
        GameObject p2;
        
        PlayerController playerController1;
        PlayerController playerController2;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ChangeState();
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     ChangeState();
            // }
        }

        void InitBuildings()
        {
            foreach (var building in initialBuildings)
            {
                building.Initialize(placementManager.Controller, 0);
                building.ConfirmPlacement();
            }

            placementManager.RefreshGrid();
        }

        private void ChangeState()
        {
            if(p1) Destroy(p1);
            if(p2) Destroy(p2);
            placementManager.PauseGrid();
            
            switch (gameState)
            {
                case GameState.Init:
                    InitBuildings();
                    StartPlaying();
                    break;
                case GameState.Playing when currentScoreP1 < targetScore && currentScoreP2 < targetScore:
                    scorePanel.Show(currentScoreP1,currentScoreP2, targetScore, () => {ChangeState();});
                    gameState = GameState.Score;
                    break;
                case GameState.Playing:
                    scorePanel.Show(currentScoreP1, currentScoreP2, targetScore, () => { Application.Quit(); });
                    gameState = GameState.EndScreen;
                    break;
                case GameState.Score:
                    StartBuilding();
                    break;
                case GameState.Building:
                    StartPlaying();
                    break;
                default:
                    gameState = GameState.EndScreen;
                    break;
            }
        }
        private void StartBuilding()
        {
            gameState = GameState.Building;
            var playerBuilderController1 = Instantiate(player1BuilderController);
            var playerBuilderController2 = Instantiate(player2BuilderController);
            p1 = playerBuilderController1.gameObject;
            p2 = playerBuilderController2.gameObject;
            cameraController.player1 = p1;
            cameraController.player2 = p2;
            placementManager.SetPlayers(playerBuilderController1, playerBuilderController2, buildings);
            placementManager.OnFinishPlacement = ChangeState;
            placementManager.ActiveGrid();
        }
        
        private void StartPlaying()
        {
            playerController1 = Instantiate(player1ControllerPrefab, playerStartPositions[0]);
            playerController2 = Instantiate(player2ControllerPrefab, playerStartPositions[1]);
            cameraController.player1 = p1 = playerController1.gameObject;
            cameraController.player2 = p2 = playerController2.gameObject;
            gameState = GameState.Playing;

            playerController1.OnDead = OnPlayerDead;
            playerController2.OnDead = OnPlayerDead;
            
            playerController1.OnScore = OnPlayerScored;
            playerController2.OnScore = OnPlayerScored;
        }

        private void OnPlayerDead(PlayerController player)
        {
            Debug.Log($"OnPlayerDead: {player.name}");
            if (player == playerController1)
            {
                playerController1.OnDead = null;
                playerController1.OnScore = null;
                playerController1 = null;
            }
            
            if (player == playerController2)
            {
                playerController2.OnDead = null;
                playerController2.OnScore = null;
                playerController2 = null;
            }

            
            if (playerController1 == null && playerController2 == null)
            {
                targetScore--;
                ChangeState();
            }
        }
        
        private void OnPlayerScored(PlayerController player)
        {
            Debug.Log($"OnPlayerScored: {player.name}");
            if (player == playerController1)
            {
                playerController1.OnDead = null;
                playerController1.OnScore = null;
                playerController1 = null;
                currentScoreP1 ++;
            }
            
            if (player == playerController2)
            {
                playerController2.OnDead = null;
                playerController2.OnScore = null;
                playerController2 = null;
                currentScoreP2 ++;
            }
            
            if (playerController1 == null && playerController2 == null)
            {
                ChangeState();
            }
        }
    }

}
