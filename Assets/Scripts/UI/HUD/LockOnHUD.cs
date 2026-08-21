using System;
using Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class LockOnHUD : MonoBehaviour
    {
        public CharacterManager target;
        private Image _image;

        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            _image.gameObject.SetActive(target);
            
        }
    }
}
