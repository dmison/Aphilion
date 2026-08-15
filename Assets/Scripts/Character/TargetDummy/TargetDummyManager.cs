using System.Collections;
using UnityEngine;

namespace Character.TargetDummy
{
    public class TargetDummyManager : CharacterManager
    {
        protected override void Awake()
        {
            base.Awake();
            characterEffectsManager = GetComponent<TargetDummyEffectsManager>();

        }
    }
}
