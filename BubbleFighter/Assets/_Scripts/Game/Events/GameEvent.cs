using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    [CreateAssetMenu(fileName = "Event", menuName = "Events/Base Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new List<GameEventListener>();
        
        public void Raise() => _listeners.ForEach(gameEventListener => gameEventListener.OnEventRaised());
        public void RegisterListener(GameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(GameEventListener listener) => _listeners.Remove(listener);
    }
}