using Interactable;
using UnityEngine;

public class Gate : MonoBehaviour,  IInteractable
{
    private static readonly int Open = Animator.StringToHash("Open");
    private bool isOpen = false;
    private Animator _animator;
    private SFXPlayer _sfxPlayer;
    [SerializeField] private AudioClip openingSFX;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sfxPlayer = GetComponent<SFXPlayer>();
    }

    public string GetLabel()
    {
        return InterationActive() ? "Open" : "";
    }

    public bool InterationActive()
    {
        return !isOpen;
    }

    public void Interact()
    {
        if(!InterationActive()) return;
        
        isOpen = true;
        _animator.SetTrigger(Open);
        
        _sfxPlayer.PlaySoundFX(openingSFX, 1F, false);
    }
}
