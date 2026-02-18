using System.Collections.Generic;
using UnityEngine;
using DialogSystem.Core;
using DialogSystem.Data.DTO;

namespace DialogSystem.Data
{
    public class JsonStructureLoader
    {
        public DialogueChapter Load(
    TextAsset jsonFile,
    Dictionary<string, string> localizationDict)
        {
            var dto = JsonUtility.FromJson<StructureDTO>(jsonFile.text);

            if (dto.chapters == null || dto.chapters.Count == 0)
                throw new System.Exception("Structure contains no chapters.");

            var chapterDto = dto.chapters[0];

            var blocks = new Dictionary<string, DialogueBlock>();

            foreach (var blockDto in chapterDto.blocks)
            {
                if (blockDto.lines == null || blockDto.lines.Count == 0)
                    Debug.LogError($"Block {blockDto.blockId} has no lines.");

                var lines = new List<DialogueLine>();

                foreach (var lineDto in blockDto.lines)
                {
                    if (!localizationDict.ContainsKey(lineDto.lineId))
                    {
                        Debug.LogError(
                            $"Missing localization key: {lineDto.lineId} in block {blockDto.blockId}");
                    }

                    lines.Add(new DialogueLine(lineDto.lineId));
                }

                if (blocks.ContainsKey(blockDto.blockId))
                {
                    Debug.LogError($"Duplicate blockId detected: {blockDto.blockId}");
                }

                var block = new DialogueBlock(blockDto.blockId, lines);
                blocks.Add(block.BlockId, block);
            }

            if (!blocks.ContainsKey(chapterDto.startBlockId))
            {
                Debug.LogError(
                    $"StartBlockId '{chapterDto.startBlockId}' does not exist in chapter {chapterDto.chapterId}");
            }

            return new DialogueChapter(
                chapterDto.chapterId,
                chapterDto.startBlockId,
                blocks);
        }

    }
}
