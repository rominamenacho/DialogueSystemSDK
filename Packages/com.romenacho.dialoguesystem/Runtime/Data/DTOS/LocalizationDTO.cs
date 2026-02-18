using System;
using System.Collections.Generic;

namespace DialogSystem.Data.DTO
{
    [Serializable]
    public class LocalizationWrapper
    {
        public List<LocalizationEntry> entries;
    }

    [Serializable]
    public class LocalizationEntry
    {
        public string key;
        public string value;
    }
}
/*
 {
  "entries": [
    { "key": "CH1_INTRO_001", "value": "Example English" },
    { "key": "CH1_INTRO_002", "value": "Example English 2" }
  ]
}
 
 */