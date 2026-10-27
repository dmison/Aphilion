using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using World;

namespace UI
{
    public class SystemMenu : MonoBehaviour
    {
        private static readonly int Active = Animator.StringToHash("Active");
        private Animator _animator;
        private InputAction _showMe;
        [SerializeField] private Button defaultButton;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private ControlHelpView controlHelpView;
        [SerializeField] private TextMeshProUGUI text;
        private bool active = false;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _showMe = InputSystem.actions.FindAction("SystemMenu/Show");
            _showMe.performed += ShowMe;
            text.text = controlHelpView.visible ? "Hide Controls" : "Show Controls";
            InputSystem.actions.FindActionMap("GameMenu").Disable();
        }

        private void OnDestroy()
        {
            _showMe.performed -= ShowMe;
            Time.timeScale = 1.0f;
        }

        public void ShowHelp()
        {
            controlHelpView.ToggleView();
            ResumeGame();
        }
        
        public void ResumeGame()
        {
            if (active)
            {
                active = false;
                Time.timeScale = 1.0f;
                _animator.SetBool(Active, false);
                InputSystem.actions.FindActionMap("Movement").Enable();
                InputSystem.actions.FindActionMap("Actions").Enable();
                InputSystem.actions.FindActionMap("GameMenu").Disable();
                InputSystem.actions.FindActionMap("EquipmentManager").Enable();
            }
        }

        public void LeaveGame()
        {
            if (active)
            {
                active = false;
                Time.timeScale = 1.0f;
                // should have a prompt here
                WorldSceneManager.Instance.GoToStartScene();
                
            }
        }
        
        public void ShowMe (InputAction.CallbackContext ctx)
        {
            if(active)
            {
                active = false;
                Time.timeScale = 1.0f;
                _animator.SetBool(Active, false);
                InputSystem.actions.FindActionMap("Movement").Enable();
                InputSystem.actions.FindActionMap("Actions").Enable();
                InputSystem.actions.FindActionMap("GameMenu").Disable();
                InputSystem.actions.FindActionMap("EquipmentManager").Enable();
                
            }
            else
            {
                text.text = controlHelpView.visible ? "Hide Controls" : "Show Controls";
                active = true;
                InputSystem.actions.FindActionMap("Movement").Disable();
                InputSystem.actions.FindActionMap("Actions").Disable();
                InputSystem.actions.FindActionMap("EquipmentManager").Disable();
                InputSystem.actions.FindActionMap("GameMenu").Enable();
                eventSystem.SetSelectedGameObject(defaultButton.gameObject);
                Time.timeScale = 0.0f;
                _animator.SetBool(Active, true);
                
            }
            
            
        }

    }
}
