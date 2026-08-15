using System;
using UnityEngine;

public class WorldUtilityManager : MonoBehaviour
{
    public static WorldUtilityManager Instance;

    [SerializeField] private LayerMask _environmentLayerMask;
    [SerializeField] private LayerMask _characterLayerMask;
    
    
    public LayerMask GetEnvironmentLayerMask() => _environmentLayerMask;
    public LayerMask GetCharacterLayerMask() => _characterLayerMask;
    
    
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
}
