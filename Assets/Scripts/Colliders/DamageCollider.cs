using System;
using System.Collections.Generic;
using Character;
using Character.Player;
using Effects;
using UnityEngine;
using World;

public class DamageCollider : MonoBehaviour
{
    protected Collider damageCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Damage")] 
    public int physicalDamage = 0;
    public int fireDamage = 0;
    public int poisonDamage = 0;
    public int lightningDamage = 0;
    public int poiseDamage = 0;

    public LayerMask canDamageLayers;

    protected Vector3 _contactPoint;

    protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();

    protected virtual void Awake()
    {
        damageCollider = GetComponent<Collider>();
    }

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
    
    public virtual void EnableDamageCollider()
    {
        damageCollider.enabled = true; 
    }

    public virtual void DisableDamageCollider()
    {
        damageCollider.enabled = false;
        charactersDamaged.Clear();
    }
}
