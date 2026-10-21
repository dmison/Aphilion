using UnityEngine;

namespace UI
{
    public class ControlHelpView : MonoBehaviour
    {
        private static readonly int Active = Animator.StringToHash("Active");
        private Animator _animator;
        public bool visible = false;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    
        public void ToggleView()
        {
            visible = !visible;
            _animator.SetBool(Active, visible);
        }
    
    }
}
