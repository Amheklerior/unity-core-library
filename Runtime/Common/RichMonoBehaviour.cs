using UnityEngine;

namespace Amheklerior.Core.Common {

    public abstract class RichMonoBehaviour : MonoBehaviour {

#if UNITY_EDITOR

        [Header("Dev Settings:")]

        [Tooltip(tooltip:"Activate/deactivate logs.")]
        [SerializeField] protected bool _debugMode = false;

        [SerializeField, Multiline] protected string _description;

#endif

    }
}
