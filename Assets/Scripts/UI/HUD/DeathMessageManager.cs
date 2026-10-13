using System.Collections;
using System.Linq;
using Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using World;

namespace UI.HUD
{
    public class DeathMessageManager : MonoBehaviour
    {

        private Animator _animator;
        private InputAction _continue;
        private InputAction _goToMenu;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            InputSystem.actions.FindActionMap("DeathMenu").Disable();
            
            _continue = InputSystem.actions.FindAction("continue");
            _continue.performed += _ => { RespawnCharacter(); };
            
            _goToMenu = InputSystem.actions.FindAction("exit");
            _goToMenu.performed += _ => { LeaveGame(); };
        }

        public void ShowDeathMessage()
        {
            if (_animator)
            {
                _animator.SetBool("Active", true);
            }
            InputSystem.actions.FindActionMap("DeathMenu").Enable();
        }

        private void HideDeathMessage()
        {
            if (_animator)
            {
                _animator.SetBool("Active", false);
            }
        }
 
        public void RespawnCharacter()
        {
            Debug.Log("lets resspawn");
            InputSystem.actions.FindActionMap("DeathMenu").Disable();
            HideDeathMessage();
            StartCoroutine(PlayerManager.Instance.RespawnAtCrystal(true));
            // WorldAIManager.Instance.ResetAllCharacters();
            
            
        }

        public void LeaveGame()
        {
            InputSystem.actions.FindActionMap("DeathMenu").Disable();
            WorldAIManager.Instance.RemoveAllSpawnPoints();
            WorldSceneManager.Instance.GoToStartScene();
            
            
        }
        
    }
}
