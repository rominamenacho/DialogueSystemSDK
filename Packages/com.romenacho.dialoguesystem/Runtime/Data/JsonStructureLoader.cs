using DialogSystem.Core;
using DialogSystem.Data.DTO;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem.Data
{
    public class JsonStructureLoader
    {
        public Dictionary<string, DialogueChapter> Load(
        TextAsset jsonFile,
        Dictionary<string, string> localizationDict)
        {
            var dto = JsonUtility.FromJson<StructureDTO>(jsonFile.text);

            if (dto.chapters == null || dto.chapters.Count == 0)
                throw new Exception("Structure contains no chapters.");

            var chapters = new Dictionary<string, DialogueChapter>();

            foreach (var chapterDto in dto.chapters)
            {
                if (chapters.ContainsKey(chapterDto.chapterId))
                    Debug.LogError($"Duplicate chapterId detected: {chapterDto.chapterId}");

                var blocks = new Dictionary<string, DialogueBlock>();

                foreach (var blockDto in chapterDto.blocks)
                {
                    if (blocks.ContainsKey(blockDto.blockId))
                        Debug.LogError(
                            $"Duplicate blockId '{blockDto.blockId}' in chapter '{chapterDto.chapterId}'");

                    if (blockDto.lines == null || blockDto.lines.Count == 0)
                        Debug.LogError(
                            $"Block '{blockDto.blockId}' in chapter '{chapterDto.chapterId}' has no lines.");

                    var lines = new List<DialogueLine>();

                    foreach (var lineDto in blockDto.lines)
                    {
                        if (!localizationDict.ContainsKey(lineDto.lineId))
                        {
                            Debug.LogError(
                                $"Missing localization key '{lineDto.lineId}' " +
                                $"in block '{blockDto.blockId}' chapter '{chapterDto.chapterId}'");
                        }

                        lines.Add(new DialogueLine(lineDto.lineId));
                    }

                    var block = new DialogueBlock(blockDto.blockId, lines);
                    blocks.Add(block.BlockId, block);
                }

                if (!blocks.ContainsKey(chapterDto.startBlockId))
                {
                    Debug.LogError(
                        $"StartBlockId '{chapterDto.startBlockId}' does not exist " +
                        $"in chapter '{chapterDto.chapterId}'");
                }

                var chapter = new DialogueChapter(
                    chapterDto.chapterId,
                    chapterDto.startBlockId,
                    blocks);

                chapters.Add(chapter.ChapterId, chapter);
            }

            return chapters;
        }

    }
}