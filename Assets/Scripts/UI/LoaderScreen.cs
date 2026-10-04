using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoaderScreen: MonoBehaviour
    {
        public static LoaderScreen Instance;
        
        [SerializeField] private Slider _slider;
        [SerializeField] private Animator _animator;
        
        private float minValue = 0.0f;
        private float maxValue = 1.0f;
        private float currentValue = 0.0f;

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

        public void ToggleLoader(bool state)
        {
            _animator.SetBool("Active", state);    
        }
        
        public void SetProgress(float progress)
        {
            currentValue = progress;
            _slider.value = currentValue;
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            SetProgress(0);
        }
    }
    
    
}


