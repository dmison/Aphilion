using Character;
using Character.Player;
using Effects;
using UnityEngine;

public class PlayerEffectsManager : CharacterEffectsManager
{
    PlayerManager player; 
    protected override void Awake()
    {
        // base.Awake();
        player = GetComponent<PlayerManager>();
    }
    public override void ProcessInstantEffect(InstantCharacterEffect effect)
    {
        // base.ProcessInstantEffect(effect);
        effect.ProcessEffect(player);
        
    }
}
