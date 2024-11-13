using UnityEngine;

namespace ScriptableObjects
{
    /// <summary>
    /// This class is responsible for storing the settings for gwin panel animations
    /// </summary>
    [CreateAssetMenu(fileName = "PanelAnimationSettings", menuName = "ScriptableObjects/PanelAnimationSettings")]
    public class PanelAnimationSettings : ScriptableObject
    {
        public float fadeDuration = 0.5f;
        public float scaleDuration = 0.5f;
        public float pulseScale = 1.1f;
        public float textPulseDuration = 0.3f;
    }
}