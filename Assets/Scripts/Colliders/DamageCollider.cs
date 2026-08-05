using System;
using Character.Player;
using Effects;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Damage")] 
    [SerializeField] private int physicalDamage = 0;
    [SerializeField] private int fireDamage = 0;
    [SerializeField] private int poisonDamage = 0;
    [SerializeField] private int lightningDamage = 0;
    [SerializeField] private int poiseDamage = 0;

    [SerializeField] private LayerMask canDamageLayers;

    private Vector3 _contactPoint;

    private bool LayerInLayerMask(int layer, LayerMask mask)
    {
        return ((mask & (1 << layer)) != 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (!LayerInLayerMask(other.gameObject.layer, canDamageLayers)) return;
        _contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        if (other.gameObject.CompareTag("Player"))
        {
            CalculateDamage(other.gameObject.GetComponent<PlayerManager>());
        }
        // TODO: Damageable interface???
    }
    
    protected virtual void CalculateDamage(PlayerManager player)
    {
        int  damage = physicalDamage + fireDamage + poisonDamage + lightningDamage;
        TakeDamageEffect effect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
        effect.physicalDamage = physicalDamage;
        effect.fireDamage = fireDamage;
        effect.poisonDamage = poisonDamage;
        effect.lightningDamage = lightningDamage;
        effect.impactPoint = _contactPoint;
        player.playerEffectsManager.ProcessInstantEffect(effect);
    }
}
