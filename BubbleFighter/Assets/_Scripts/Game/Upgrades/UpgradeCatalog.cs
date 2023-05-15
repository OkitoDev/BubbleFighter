using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Game.Upgrades
{
    [Serializable]
    public class ValueUpgrades<T> where T : struct
    {
        public List<ValueUpgrade<T>> upgrades;
    }
    
    [Serializable]
    public class ReferenceUpgrades<T> where T : class
    {
        public List<ReferenceUpgrade<T>> upgrades;
    }
    
    public abstract class BaseUpgrade
    {
        public int cost;
    }

    [Serializable]
    public class ValueUpgrade<T> : BaseUpgrade where T : struct
    {
        [SerializeField] public T value;
    }
    
    [Serializable]
    public class ReferenceUpgrade<T> : BaseUpgrade where T : class
    {
        [SerializeReference, SubclassSelector] public T value;
    }
}