using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HudIconManager : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] public Image icon;
        [SerializeField] public Image background;
        [SerializeField] public Image sigil;
        [SerializeField] public Image frame;
        [SerializeField] public TextMeshProUGUI label;
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        
        }
    }
}
