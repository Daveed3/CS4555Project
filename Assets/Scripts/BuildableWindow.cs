using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class BuildableWindow : BuildableItem
    {
        int Health = 100;

        public override void OnDamaged(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                base.OnDestroy();
            }
        }

        public override void OnRebuild()
        {
            Health = 100;

            base.OnRebuild();
        }
    }
}