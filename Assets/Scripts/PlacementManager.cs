using UnityEngine;
namespace GGJ2025
{
    public class PlacementManager : MonoBehaviour
    {
        private GridController<string> gridController;
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gridController = new GridController<string>(16, 16);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}