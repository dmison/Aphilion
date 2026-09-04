using System;
using System.Collections.Generic;
using Character.Player;
using Unity.VisualScripting;
using UnityEngine;
using World;

namespace Character
{
    public class CharacterSenses : MonoBehaviour
    {
        private bool _isPlayer;
        [Header("Vision")]
        [SerializeField] private float visionDistance=2;
        [SerializeField] private float visionFOV=90;
        private void Awake()
        {
            PlayerManager pm = GetComponent<PlayerManager>();
            _isPlayer = pm != null;
        }

        private Vector3 ForwardDirection()
        {
            Vector3 forwardDirection = _isPlayer
                ? PlayerCameraManager.instance.playerCamera.transform.forward
                : transform.forward;
            forwardDirection.Normalize();
            return forwardDirection;
        }
        public List<CharacterManager> CharactersInVision()
        {
            List<CharacterManager> charactersInVision = new List<CharacterManager>();
            
            if (!WorldUtilityManager.Instance)
                return charactersInVision;
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, visionDistance, WorldUtilityManager.Instance.GetCharacterLayerMask());

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager cm = colliders[i].GetComponent<CharacterManager>();

                if (!cm) continue; // null for some reason
                if (cm.IsDead) continue; // target is dead
                if (cm.transform.root == transform.root) continue; // target is self

                Vector3 targetDirection = cm.transform.position - transform.position;
                float targetDistance = Vector3.Distance(transform.position, cm.transform.position);

                
                float targetViewAngle = Vector3.Angle(targetDirection, ForwardDirection());

                // distance check?  no need as it will need to be in the radius

                if ((targetViewAngle > -(visionFOV / 2)) && targetViewAngle < visionFOV / 2)
                {
                    charactersInVision.Add(cm);
                }
            }
            return charactersInVision;
        }

        
        // private void OnDrawGizmosSelected()
        // {
        //     if(GetComponent<PlayerManager>() != null) return;
        //     //draw vision arc
        //     Vector3[] points = VectorDrawing.GenerateArcLinePoints(
        //         transform.position,
        //         transform.TransformDirection(transform.forward), 
        //         visionFOV, 
        //         visionDistance,
        //         Mathf.CeilToInt(visionFOV/20));
        //
        //     for (int i = 0; i < points.Length - 1; i++)
        //     {
        //          points[i] = transform.TransformPoint(points[i]);
        //     }
        //         
        //     
        //     Gizmos.color = Color.darkMagenta;
        //     Gizmos.DrawLineStrip(points, true);
        //
        // }
    }
}