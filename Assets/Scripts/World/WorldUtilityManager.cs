using System;
using Character;
using UnityEngine;

public class WorldUtilityManager : MonoBehaviour
{
    public static WorldUtilityManager Instance;

    [SerializeField] private LayerMask _environmentLayerMask;
    [SerializeField] private LayerMask _characterLayerMask;
    [SerializeField] private LayerMask _enemiesLayerMask;
    
    
    public LayerMask GetEnvironmentLayerMask() => _environmentLayerMask;
    public LayerMask GetCharacterLayerMask() => _characterLayerMask;
    public LayerMask GetEnemiesLayerMask() => _enemiesLayerMask;
    
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
