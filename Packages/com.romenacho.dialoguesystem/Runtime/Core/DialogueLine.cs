using UnityEngine;

namespace DialogSystem.Core
{
    [Serializable]
    public sealed class DialogueLine
    {
        public string Id { get; }
        public string CharacterId { get; }
        public string TextKey { get; }
        public string VoiceKey { get; }

        public DialogueLine(
            string id,
            string characterId,
            string textKey,
            string voiceKey = null)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            CharacterId = characterId;
            TextKey = textKey ?? throw new ArgumentNullException(nameof(textKey));
            VoiceKey = voiceKey;
        }
    }

}


