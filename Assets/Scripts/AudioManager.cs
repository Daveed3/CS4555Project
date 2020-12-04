using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        static GameObject playerAudioFolder;
        
        private void Start() {
            playerAudioFolder = GameObject.Find("/Player/Audio");
        }

        // plays an audio source if an audio with the tag "PlayerVoiceAudio" isn't already playing
        public static void CheckAndPlayAudio(AudioSource audioToPlay)
        {
            AudioSource[] audioSources = playerAudioFolder.GetComponentsInChildren<AudioSource>();
            bool playerAudioPlaying = false;
            foreach(AudioSource source in audioSources)
            {
                if(source.tag.Equals("PlayerVoiceAudio"))
                {
                    if(source.isPlaying)
                    {
                        playerAudioPlaying = true;
                        break;
                    }
                }
            }

            if(!playerAudioPlaying)
            {
                audioToPlay.Play();
            }
        }
    }
}