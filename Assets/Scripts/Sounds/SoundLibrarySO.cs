using UnityEngine;

namespace Sounds
{
    /// <summary>
    ///  This class is responsible for storing the sounds used in the game
    /// </summary>
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "ScriptableObjects/SoundLibrary")]
    public class SoundLibrarySO : ScriptableObject
    {
        public  AudioClip buttonClickedSound;
        public  AudioClip cardFlipSound;
        public  AudioClip winSound;
        
        public AudioClip menuMusic;
        public AudioClip gameMusic;
    }
}