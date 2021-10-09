using System;
using UnityEngine;

namespace Amheklerior.Core.EventSystem {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameEventUtility.GAME_EVENT_MENU_ROOT + "Int 3D vector event", order = 9)]
    public class Vector3IntGameEvent : GameEvent<Vector3Int> { }

}