using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactable
{
    public class Switch : MonoBehaviour, IInteractable
    {
        private static readonly int On = Animator.StringToHash("On");
        private Animator _animator;
        private bool _isActive;

        [SerializeField] private IActionTarget _target;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public string GetLabel()
        {
            return "Switch";
        }

        public bool InterationActive()
        {
            return true;
        }

        public void Interact()
        {
            _isActive = !_isActive;
            _animator.SetBool(On, _isActive);
            if (_isActive)
            {
                _target.On();
            }
            else
            {
                _target.Off();
            }
        }
    }
}
