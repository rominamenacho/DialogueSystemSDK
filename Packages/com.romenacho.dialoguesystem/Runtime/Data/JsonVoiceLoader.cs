using System.Collections.Generic;
using UnityEngine;
using DialogSystem.Data.DTO;

namespace DialogSystem.Data
{
    public sealed class JsonVoiceLoader
    {
        public Dictionary<string, string> Load(TextAsset jsonFile)
        {
            var wrapper = JsonUtility.FromJson<VoiceWrapper>(jsonFile.text);

            var dict = new Dictionary<string, string>();

            foreach (var entry in wrapper.entries)
            {
                dict[entry.key] = entry.audioKey;
            }

            return dict;
        }
    }
}
