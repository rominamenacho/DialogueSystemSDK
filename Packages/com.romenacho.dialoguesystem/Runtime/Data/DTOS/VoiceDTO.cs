using System;
using System.Collections.Generic;

namespace DialogSystem.Data.DTO
{
    [Serializable]
    public class VoiceWrapper
    {
        public List<VoiceEntry> entries;
    }

    [Serializable]
    public class VoiceEntry
    {
        public string key;
        public string audioKey;
    }
}
/*{
  "entries": [
    { "key": "CH1_INTRO_001", "audioKey": "VO_CH1_001" },
    { "key": "CH1_INTRO_002", "audioKey": "VO_CH1_002" }
  ]
}
*/