using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private float musicVolume = 1;
        private float sfxVolume = 1;
        private float gameVolume = 1;
        
        private void Awake()
        {
            Instance = this;
        }
    }
}