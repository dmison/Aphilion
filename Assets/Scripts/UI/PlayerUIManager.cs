using UI.HUD;
using UnityEngine;

namespace UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Instance;
    
        public StatusBar statsBar;
        public DeathMessageManager deathMessageManager;
        public LockOnHUD lockOnHUD;
    
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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
