using System.Collections.Generic;
using DialogSystem.Interfaces;

namespace DialogSystem.Localization
{
    public sealed class JsonLocalizationProvider : ILocalizationProvider
    {
        private readonly Dictionary<string, string> _texts;

        public JsonLocalizationProvider(Dictionary<string, string> texts)
        {
            _texts = texts ?? new Dictionary<string, string>();
        }

        public string GetText(string lineId)
        {
            if (string.IsNullOrEmpty(lineId))
                return string.Empty;

            if (_texts.TryGetValue(lineId, out var text))
                return text;

            return $"[MISSING:{lineId}]";
        }

        public bool Contains(string lineId)
        {
            return _texts.ContainsKey(lineId);
        }
    }
}
