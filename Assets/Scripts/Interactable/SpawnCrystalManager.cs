using UnityEngine;
using World;

namespace Interactable
{
    public class SpawnCrystalManager : MonoBehaviour
    {
        public int crystalId;
        public string crystalName;
        private bool _playerPresent;
        [SerializeField] private bool _active;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            WorldCrystalSpawnerManager.Instance.ReportIn(this);
        }

        private void OnDestroy()
        {
            WorldCrystalSpawnerManager.Instance.RemoveMe(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerPresent = true;       
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
