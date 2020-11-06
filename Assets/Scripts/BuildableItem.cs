using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class BuildableItem : MonoBehaviour, IBuildableItem
    {
        public Vector3 ItemPosition;
        public Vector3 ItemRotation;

        public string ItemName;

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

        public virtual void OnDamaged(int damage)
        {
            return;
        }

        public virtual void OnDestroy()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnRebuild()
        {
            gameObject.SetActive(true);
        }

    }
}