using Character.Player;
using UnityEngine;

namespace Audio
{
    public class PlayerTrack : MonoBehaviour
    {
        [SerializeField] private bool lockX = false;
        [SerializeField] private bool lockY = false;
        [SerializeField] private bool lockZ = false;
        private Transform target;

        void Update()
        {
            if (target)
            {
                float x = transform.position.x;
                float y = transform.position.y;
                float z = transform.position.z;

                if (!lockX) x = target.position.x;
                if (!lockY) y = target.position.y;
                if (!lockZ) z = target.position.z;

                transform.position = new Vector3(x, y, z);
            }
            else
            {
                target = PlayerManager.Instance.transform;
            }
        }
    }
}
