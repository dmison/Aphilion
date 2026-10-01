using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemManager: MonoBehaviour
{
    public static InputSystemManager Instance;

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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void ToggleMenuInputs(bool enable)
    {
        if (enable)
        {
            InputSystem.actions.FindActionMap("Menus").Enable();    
        }
        else
        {
            InputSystem.actions.FindActionMap("Menus").Disable();
        }
            
            
    }

    public void TogglePlayerControls(bool enable)
    {
        if (enable)
        {
            InputSystem.actions.FindActionMap("Movement").Enable();
            InputSystem.actions.FindActionMap("Actions").Enable();
            InputSystem.actions.FindActionMap("EquipmentManager").Enable();
        }
        else
        {
            InputSystem.actions.FindActionMap("Movement").Disable();
            InputSystem.actions.FindActionMap("Actions").Disable();
            InputSystem.actions.FindActionMap("EquipmentManager").Disable();
        }
    }
    
}