using System;

namespace DialogSystem.Core
{
    [Serializable]
    public sealed class DialogueLine
    {
        public string Id { get; }
      

        public DialogueLine( string lineId)
        {
            if (string.IsNullOrWhiteSpace(lineId))
                throw new System.ArgumentException("LineId cannot be null or empty.");

            Id = lineId;
        }
    }

}


