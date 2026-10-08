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
            _animator.SetBool("Active", true);
            InputSystem.actions.FindActionMap("DeathMenu").Enable();
        }

        public void HideDeathMessage()
        {
            _animator.SetBool("Active", false);
            InputSystem.actions.FindActionMap("DeathMenu").Disable();
        }
 
        public void RespawnCharacter()
        {
            HideDeathMessage();
            PlayerManager.Instance.RespawnAtCrystal();
        }

        public void LeaveGame()
        {
            WorldSceneManager.Instance.GoToStartScene();
            //use scenemanager to go back to start screen
        }
        
    }
}
