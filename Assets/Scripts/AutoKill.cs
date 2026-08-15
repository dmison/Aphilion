using System.Collections;
using UnityEngine;

public class AutoKill : MonoBehaviour
{
    [SerializeField] private float secondsToLive;

    private void Awake()
    {
        Destroy(gameObject, secondsToLive);
    }

    
}
