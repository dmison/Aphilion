using System;
using Interactable;
using UnityEngine;

namespace World
{
    public class AiSpawnPoint: MonoBehaviour
    {
        [SerializeField] public GameObject AiPrefab;
        [SerializeField] public SpawnCrystalManager SpawnCrystalManager;
        private Mesh _editorMesh;

                
        
        private void OnDrawGizmos()
        {
            SkinnedMeshRenderer smr = AiPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            _editorMesh = AiPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
            Material m = smr.sharedMaterial;

            // RenderParams rp = new RenderParams(m);
            // Graphics.RenderMesh(rp, _editorMesh, 0, Matrix4x4.identity);
            // Graphics.DrawMesh(_editorMesh, transform.position, transform.rotation, m, 0);
            // Graphics.DrawMeshNow(_editorMesh, Matrix4x4.identity, 0);                        

            Gizmos.color = Color.magenta;
            Gizmos.DrawMesh(_editorMesh, transform.position, transform.rotation);
            

        }

        private void OnDrawGizmosSelected()
        {

        }
    }
}