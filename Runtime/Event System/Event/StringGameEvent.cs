using System;
using UnityEngine;

namespace Amheklerior.Core.EventSystem {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameEventUtility.GAME_EVENT_MENU_ROOT + "String event", order = 3)]
    public class StringGameEvent : GameEvent<string> { }

}