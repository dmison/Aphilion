using System;
using Interactable;
using UnityEngine;

namespace Character.Player
{
    public class PlayerInteractionManager: MonoBehaviour
    {
        [SerializeField] private BoxCollider _area;
        private string _labelText = "";
        private void Awake()
        {
                
        }

        private void OnTriggerEnter(Collider other)
        {
            IInteractable interaction = other.GetComponent<IInteractable>();
            if (interaction != null)
            {
                Debug.Log(interaction.GetLabel());
            };
        }

        private void OnTriggerExit(Collider other)
        {
            // Debug.Log(other);
        }

        private void DoInteraction()
        {
            
        }

        private void Update()
        {
            if (_labelText.Length > 0)
            {
                // send to UI
            }
        }
    }
}