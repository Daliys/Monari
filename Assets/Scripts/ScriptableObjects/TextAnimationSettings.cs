using UnityEngine;

namespace ScriptableObjects
{
    /// <summary>
    ///  This class is responsible for storing the settings for regular text animations
    /// </summary>
    [CreateAssetMenu(fileName = "TextAnimationSettings", menuName = "ScriptableObjects/TextAnimationSettings", order = 1)]
    public class TextAnimationSettings : ScriptableObject
    {
        public float regularTextAnimationDuration = 0.3f;
        public float regularTextScaleMultiplier = 1.2f;
    }
}