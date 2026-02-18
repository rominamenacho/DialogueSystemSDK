using System.Collections.Generic;

namespace DialogSystem.Core
{
    public sealed class DialogueBlock
    {
        public string BlockId { get; }
        public IReadOnlyList<DialogueLine> Lines => _lines;

        private readonly List<DialogueLine> _lines;

        public DialogueBlock(string blockId, List<DialogueLine> lines)
        {
            if (string.IsNullOrWhiteSpace(blockId))
                throw new System.ArgumentException("BlockId cannot be null or empty.");

            if (lines == null || lines.Count == 0)
                throw new System.ArgumentException("Block must contain at least one line.");

            BlockId = blockId;
            _lines = lines;
        }
    }
}
