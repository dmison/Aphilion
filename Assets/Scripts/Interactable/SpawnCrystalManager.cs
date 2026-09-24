using UnityEngine;
using World;

namespace Interactable
{
    public class SpawnCrystalManager : MonoBehaviour
    {
        public int crystalId;
        public string crystalName;
        private bool _playerPresent;
        [SerializeField] public bool activated;
        [SerializeField] public Transform spawnPoint; 
        
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
