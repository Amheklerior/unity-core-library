using System;
using UnityEngine;

namespace Amheklerior.Core.EventSystem {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameEventUtility.GAME_EVENT_MENU_ROOT + "GameObject event", order = 10)]
    public class GameObjectGameEvent : GameEvent<GameObject> { }

}