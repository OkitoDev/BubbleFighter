using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "GlobalFloatValue", menuName = "Global Values/Float")]
    public class GlobalFloatValue : ScriptableObject, ISerializationCallbackReceiver
    {
        public float initialValue;

        [NonSerialized] public float RuntimeValue;

        public void OnAfterDeserialize()
        {
            RuntimeValue = initialValue;
        }

        public void OnBeforeSerialize() { }
    }
}