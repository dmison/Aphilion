using UnityEngine;

namespace World
{
    public class WorldUtilityManager : MonoBehaviour
    {
        public static WorldUtilityManager Instance;

        [SerializeField] private LayerMask environmentLayerMask;
        [SerializeField] private LayerMask characterLayerMask;
        [SerializeField] private LayerMask enemiesLayerMask;
    
    
        public LayerMask GetEnvironmentLayerMask() => environmentLayerMask;
        public LayerMask GetCharacterLayerMask() => characterLayerMask;
        public LayerMask GetEnemiesLayerMask() => enemiesLayerMask;
    
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

        // private void Start()
        // {
        //     DontDestroyOnLoad(gameObject);
        // }

        public float GetAngleToTarget(Transform from, Vector3 direction)
        {
            Debug.DrawLine(from.position, from.position + direction, Color.red);
            
            direction.y = 0;
            float angle = Vector3.Angle(from.forward, direction);
            Vector3 cross = Vector3.Cross(from.forward, direction);
            if(cross.y<0) angle = -angle;
            return angle;
        }
    }
}
