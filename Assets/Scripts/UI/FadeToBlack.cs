using System;
using UnityEngine;

namespace UI
{
    public class FadeToBlack : MonoBehaviour
    {
        private Animator _animator;
        private CanvasGroup _canvasGroup;
        
        public bool isBlack()
        {
            return _canvasGroup.alpha >= 0.999f;
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeToBack()
        {
            Debug.Log("toblack");
            _animator.SetBool("Active", true);
        }
        
        public void FadeFromBack()
        {
            Debug.Log("fromblack");
            _animator.SetBool("Active", false);
        }
    }
}
