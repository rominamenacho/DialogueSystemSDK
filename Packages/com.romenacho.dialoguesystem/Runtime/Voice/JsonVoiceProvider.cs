using System.Collections.Generic;
using UnityEngine;
using DialogSystem.Interfaces;

namespace DialogSystem.Voice
{
    public sealed class JsonVoiceProvider : IVoiceProvider
    {
        private readonly Dictionary<string, string> _voiceMap;
        private readonly AudioSource _audioSource;

        public JsonVoiceProvider(
            Dictionary<string, string> voiceMap,
            AudioSource audioSource)
        {
            _voiceMap = voiceMap ?? new Dictionary<string, string>();
            _audioSource = audioSource;
        }

        public void Play(string lineId)
        {
            if (_audioSource == null)
                return;

            if (!_voiceMap.TryGetValue(lineId, out var audioKey))
                return;

            var clip = Resources.Load<AudioClip>(audioKey);

            if (clip != null)
                _audioSource.PlayOneShot(clip);
        }

        public bool HasVoice(string lineId)
        {
            return _voiceMap.ContainsKey(lineId);
        }

        public void Stop()
        {
            if (_audioSource != null && _audioSource.isPlaying)
                _audioSource.Stop();
        }
    }
}
