using System;
using UnityEngine;

namespace Amheklerior.Core.EventSystem {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameEventUtility.GAME_EVENT_MENU_ROOT + "Float 3D vector event", order = 10)]
    public class Vector3GameEvent : GameEvent<Vector3> { }

}