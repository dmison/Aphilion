using System;
using Character;
using Character.AICharacters;
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

    public float GetAngleToTarget(Transform from, Vector3 direction)
    {
        direction.y = 0;
        float angle = Vector3.Angle(from.forward, direction);
        Vector3 cross = Vector3.Cross(from.forward, direction);
        if(cross.y<0) angle = -angle;
        return angle;
    }
}
