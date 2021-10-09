using System;
using UnityEngine;

namespace Amheklerior.Core.EventSystem {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameEventUtility.GAME_EVENT_MENU_ROOT + "Float event", order = 5)]
    public class FloatGameEvent : GameEvent<float> { }

}