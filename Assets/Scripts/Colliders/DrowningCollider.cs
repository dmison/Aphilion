using System;
using System.Collections.Generic;
using Character;
using Character.Player;
using UnityEngine;
using World;

namespace Colliders
{
    public class DrowningCollider: MonoBehaviour
    {
        protected Collider damageCollider;
        protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();
        public LayerMask canDamageLayers;

        protected Vector3 _contactPoint;
        
        protected virtual void Awake()
        {
            damageCollider = GetComponent<Collider>();
        }

        protected bool LayerInLayerMask(int layer, LayerMask mask)
        {
            return ((mask & (1 << layer)) != 0);
        }

        public void Reset()
        {
            charactersDamaged.Clear();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);    
            // get root player object
            PlayerManager player = other.gameObject.GetComponentInParent<PlayerManager>();
            if (player == null) return;
            if (charactersDamaged.Contains(player)) return;
            charactersDamaged.Add(player);
            GameObject characterGO = player.gameObject;
            player.characterSoundEffectsManager.PlaySoundFX(WorldSoundFxManager.instance.splashSFX);
            //
            if (
                !LayerInLayerMask(player.gameObject.layer, WorldUtilityManager.Instance.GetCharacterLayerMask())
                &&
                !LayerInLayerMask(player.gameObject.layer, WorldUtilityManager.Instance.GetEnemiesLayerMask())
            ) return;
            
            _contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            StartCoroutine(player.playerDeathManager.Drown(this));
            
            
        }
    }
}