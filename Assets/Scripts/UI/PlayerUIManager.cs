using Character.Player;
using UI.HUD;
using UnityEngine;

namespace UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Instance;
        
        [SerializeField] public StatusBar statsBar;
        [SerializeField] public DeathMessageManager deathMessageManager;
        [SerializeField] public InteractPrompt interactPrompt;
        [SerializeField] public FadeToBlack fadeToBlack;
        
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
