using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class BuildableItem : MonoBehaviour, IBuildableItem
    {
        public Vector3 ItemPosition;
        public Vector3 ItemRotation;

        public string ItemName;
        public int Health = 0;
        public Animator animator;
        public Player player;

        public string Name
        {
            get
            {
                return ItemName;
            }
            set
            {
                ItemName = value;
            }
        }

        public void OnDamaged(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                OnDestroy();
            }
        }

        public void OnDestroy()
        {
            // Assuming first 5 objects are are planks, disable them, not the NavMeshLink (object 6)
            for (int i=0; i<transform.childCount-1; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            // gameObject.SetActive(false);
        }

        public void OnRebuild()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            animator.SetInteger("Rebuild", 1);

            Health = 100;
            player.IncreaseScore(HitEnemy: false, BuiltBarrier: true);
        }
    }
}