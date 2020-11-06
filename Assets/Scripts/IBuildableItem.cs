using UnityEngine;
using UnityEditor;
using System;

namespace Assets.Scripts
{
    public interface IBuildableItem
    {
        string Name { get; set; }
        void OnRebuild();
        void OnDamaged(int damage);
        void OnDestroy();
    }
}