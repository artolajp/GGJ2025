using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025
{
    public class GameManager : MonoBehaviour
    {
        enum GameState {Init ,Playing, Building, Score, EndScreen, StartScreen}
        
        [SerializeField] GameState gameState = GameState.Init;
        [SerializeField] private List<Building> initialBuildings = new List<Building>();
        [SerializeField] private PlacementManager placementManager = null;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (gameState == GameState.Init)
            {
                InitBuildings();

                gameState = GameState.Building;
            }
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
        
    }

}
