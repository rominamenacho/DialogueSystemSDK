using System.Collections.Generic;

namespace DialogSystem.Core
{
    public sealed class DialogueChapter
    {
        public string ChapterId { get; }
        public string StartBlockId { get; }

        private readonly Dictionary<string, DialogueBlock> _blocks;

        public DialogueChapter(
            string chapterId,
            string startBlockId,
            Dictionary<string, DialogueBlock> blocks)
        {
            if (string.IsNullOrWhiteSpace(chapterId))
                throw new System.ArgumentException("ChapterId cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(startBlockId))
                throw new System.ArgumentException("StartBlockId cannot be null or empty.");

            if (blocks == null || blocks.Count == 0)
                throw new System.ArgumentException("Chapter must contain blocks.");

            if (!blocks.ContainsKey(startBlockId))
                throw new System.ArgumentException("StartBlockId must exist in blocks.");

            ChapterId = chapterId;
            StartBlockId = startBlockId;
            _blocks = blocks;
        }

        public DialogueBlock GetBlock(string blockId)
        {
            if (!_blocks.TryGetValue(blockId, out var block))
                throw new KeyNotFoundException($"Block '{blockId}' not found in chapter '{ChapterId}'.");

            return block;
        }

        public DialogueBlock GetStartBlock()
        {
            return GetBlock(StartBlockId);
        }
    }
}
