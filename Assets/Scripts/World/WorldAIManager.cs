using System;
using UnityEngine;

namespace World
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        
    }
}
