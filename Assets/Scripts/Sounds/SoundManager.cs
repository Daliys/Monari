using UnityEngine;

namespace Sounds
{
    /// <summary>
    ///  This class is responsible for managing the sounds in the game
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundLibrarySO soundLibrary;
        [SerializeField] private  AudioSource uiAudioSource;
        [SerializeField] private  AudioSource effectAudioSource;
        [SerializeField] private  AudioSource musicAudioSource;
        
        [Range(0f, 1f)]  [SerializeField] private  float effectsVolume = 1f;
        [Range(0f, 1f)]  [SerializeField] private  float musicVolume = 1f;
        [Range(0f, 1f)]  [SerializeField] private  float uiVolume = 1f;
    
        public static SoundManager Instance { get; private set; }
    
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            uiAudioSource.volume = uiVolume;    
            effectAudioSource.volume = effectsVolume;
            musicAudioSource.volume = musicVolume;
        }

    
        public void PlayButtonClickedSound()
        {
            uiAudioSource.PlayOneShot(soundLibrary.buttonClickedSound);
            uiAudioSource.Play();
        }
    
        public void PlayCardFlipSound()
        {
            effectAudioSource.PlayOneShot(soundLibrary.cardFlipSound);
            effectAudioSource.Play();
        }
    
        public void PlayWinSound()
        {
            effectAudioSource.PlayOneShot(soundLibrary.winSound);
            effectAudioSource.Play();
        }
        
        public void PlayMenuMusic()
        {
            musicAudioSource.clip = soundLibrary.menuMusic;
            musicAudioSource.Play();
        }
        
        public void PlayGameMusic()
        {
            musicAudioSource.clip = soundLibrary.gameMusic;
            musicAudioSource.Play();
        }
    
    
    
    }
}
