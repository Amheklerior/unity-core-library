using System;
using UnityEngine;

namespace Amheklerior.Core.EventSystem {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameEventUtility.GAME_EVENT_MENU_ROOT + "Int 2D vector event", order = 7)]
    public class Vector2DIntGameEventInt : GameEvent<Vector2Int> { }

}