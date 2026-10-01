using System.Data;
using UnityEngine;
using World;

namespace Interactable
{
    public class SpawnCrystalManager : MonoBehaviour
    {
        public int crystalId;
        public string crystalName;
        private bool _playerPresent;
        private Animator _animator; 
        [SerializeField] GameObject _crystalObject;
        [SerializeField] MeshRenderer _crystalRenderer;
        [SerializeField] public bool activated;
        [SerializeField] public Transform spawnPoint; 
        
        
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (activated)
            {
                _animator.SetTrigger("Activate");
            }
            WorldCrystalSpawnerManager.Instance.ReportIn(this);
            
        }

        public void ActivateMe()
        {
            activated = true;
            _animator.SetTrigger("Activate");
            
        }
        
        private void OnDestroy()
        {
            WorldCrystalSpawnerManager.Instance.RemoveMe(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if(!activated)ActivateMe();
                _playerPresent = true;
                WorldSaveGameManager.instance.SaveGame();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerPresent = false;
            }
        }
    }
}
