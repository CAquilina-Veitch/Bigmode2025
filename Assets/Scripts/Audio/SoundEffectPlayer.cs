using System;
using Extensions;
using R3;
using UnityEngine;

namespace Audio
{
    public class SoundEffectPlayer : DisposableBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private AudioClip audioClip;
        private float volumeMultiplier;
        
        public void PlaySound(AudioClip sound, float newVolumeMultiplier = 1, float pitch = 1)
        {
            volumeMultiplier = newVolumeMultiplier;
            AudioManager.GameVolume.Subscribe(SetSourceVolume).AddTo(this);
            AudioManager.SFXVolume.Subscribe(SetSourceVolume).AddTo(this);
            SetSourceVolume();
            audioSource.clip = sound;
            audioSource.pitch = pitch;
            audioSource.Play();
            Observable.Timer(TimeSpan.FromSeconds(sound.length + 0.1f)).Subscribe(_ => Destroy(gameObject)).AddTo(this);
        }

        private void SetSourceVolume() => audioSource.volume = AudioManager.GameVolume.CurrentValue * 
                                                               AudioManager.SFXVolume.CurrentValue *
                                                               volumeMultiplier;
    }
}