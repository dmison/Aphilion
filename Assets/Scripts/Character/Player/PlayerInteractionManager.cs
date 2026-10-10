using System;
using Interactable;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerInteractionManager: MonoBehaviour
    {
        [SerializeField] private BoxCollider _area;
        IInteractable _interactable;
        InputAction _useAction;
        
        private void Awake()
        {
            _useAction = InputSystem.actions.FindAction("Actions/Interact");
            _useAction.performed += DoInteraction;
        }

        private void OnDestroy()
        {
            _useAction.performed -= DoInteraction;
        }

        private void OnTriggerEnter(Collider other)
        {
            IInteractable interaction = other.GetComponent<IInteractable>();
            if (interaction != null)
            {
                _interactable = interaction;
                PlayerUIManager.Instance.interactPrompt.SetLabelText(interaction.GetLabel());
            };
        }

        private void OnTriggerExit(Collider other)
        {
            IInteractable interaction = other.GetComponent<IInteractable>();
            if (interaction != null)
            {
                _interactable = null;
                PlayerUIManager.Instance.interactPrompt.SetLabelText("");
            };
        }

        private void DoInteraction(InputAction.CallbackContext context)
        {
            if (_interactable != null)
            {
                _interactable.Interact();
            }
        }
    }
}