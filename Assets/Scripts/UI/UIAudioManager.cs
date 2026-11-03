using UnityEngine;

namespace UI
{
    public class UIAudioManager : MonoBehaviour
    {
        public static UIAudioManager Instance;

        [SerializeField] private AudioClip menuMusic;
        [SerializeField] private AudioClip navSound;
        [SerializeField] private AudioClip activateSound;
        [SerializeField] private AudioClip startGameSound;
        
        private SFXPlayer _sfxPlayer;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _sfxPlayer = GetComponent<SFXPlayer>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayNavSound()
        {
            _sfxPlayer.PlaySoundFX(navSound);
        }
        
        public void PlayActivateSound()
        {
            _sfxPlayer.PlaySoundFX(activateSound);
        }

        public void PlayStartGameSound()
        {
            _sfxPlayer.PlaySoundFX(startGameSound);
        }
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    
    }
}
