using System;
using R3;
using UnityEngine;

namespace Audio
{
    public enum SoundEffects
    {
        
    }

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private SoundEffectPlayer soundPlayerPrefab;

        public static AudioManager Instance;

        public static ReadOnlyReactiveProperty<float> MusicVolume => Instance.musicVolume;
        private readonly ReactiveProperty<float> musicVolume = new(1);
        public static ReadOnlyReactiveProperty<float> SFXVolume => Instance.sfxVolume;
        private readonly ReactiveProperty<float> sfxVolume = new(1);
        public static ReadOnlyReactiveProperty<float> GameVolume => Instance.gameVolume;
        private readonly ReactiveProperty<float> gameVolume = new(1);
        
        private void Awake() => Instance = this;

        public static void PlaySFX(AudioClip clip, float volumeMultiplier = 1) => Instance.PlaySoundEffect(clip, volumeMultiplier);

        private void PlaySoundEffect(AudioClip clip, float volumeMultiplier = 1)
        {
            var sfx = Instantiate(soundPlayerPrefab, transform);
            sfx.PlaySound(clip, volumeMultiplier );
        }

        public static void PlayMusic(AudioClip clip)
        {
            
        }
    }
}