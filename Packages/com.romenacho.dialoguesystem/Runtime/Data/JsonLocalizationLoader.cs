using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem.Data
{
    public class JsonLocalizationLoader
    {

        public Dictionary<string, string> Load(TextAsset jsonFile)
        {
            var raw = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonFile.text);

            var dict = new Dictionary<string, string>();

            foreach (var pair in raw)
            {
                if (dict.ContainsKey(pair.Key))
                    Debug.LogError($"Duplicate localization key: {pair.Key}");

                dict[pair.Key] = pair.Value.ToString();
            }
            return dict;
        }
    }
}


