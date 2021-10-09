using System;
using UnityEngine;

namespace Amheklerior.Core.EventSystem {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameEventUtility.GAME_EVENT_MENU_ROOT + "ScriptableObject event", order = 11)]
    public class ScriptableObjectGameEvent : GameEvent<ScriptableObject> { }

}