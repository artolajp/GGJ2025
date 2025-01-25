using UnityEngine;
namespace GGJ2025
{
    public class PlacementManager : MonoBehaviour
    {
        private GridController<GridObject> gridController;
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gridController = new GridController<GridObject>(16, 16);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}