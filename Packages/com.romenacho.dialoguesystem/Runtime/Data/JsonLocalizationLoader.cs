using System.Collections.Generic;
using UnityEngine;
using DialogSystem.Data.DTO;

namespace DialogSystem.Data
{
    public sealed class JsonLocalizationLoader
    {
        public Dictionary<string, string> Load(TextAsset jsonFile)
        {
            var wrapper = JsonUtility.FromJson<LocalizationWrapper>(jsonFile.text);

            var dict = new Dictionary<string, string>();

            foreach (var entry in wrapper.entries)
            {
                dict[entry.key] = entry.value;
            }

            return dict;
        }
    }
}


/*{
"entries": [
    { "key": "CH1_INTRO_001", "value": "Example English" },
    { "key": "CH1_INTRO_002", "value": "Example English 2" }
  ]
}
*/