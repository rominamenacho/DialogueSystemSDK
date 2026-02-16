using System.Collections.Generic;
using UnityEngine;
using DialogSystem.Core;
using DialogSystem.Data.DTO;

namespace DialogSystem.Data
{
    public sealed class JsonStructureLoader
    {
        public DialogueChapter Load(TextAsset jsonFile)
        {
            var dto = JsonUtility.FromJson<StructureDTO>(jsonFile.text);

            if (dto.chapters == null || dto.chapters.Count == 0)
                throw new System.Exception("Structure contains no chapters.");

            // Para versión simple: cargamos solo el primero
            // TODO Para versión más avanzada: cargar todos los capítulos y guardarlos en un diccionario
            var chapterDto = dto.chapters[0];

            var blocks = new Dictionary<string, DialogueBlock>();

            foreach (var blockDto in chapterDto.blocks)
            {
                var lines = new List<DialogueLine>();

                foreach (var lineDto in blockDto.lines)
                {
                    lines.Add(new DialogueLine(lineDto.lineId));
                }

                var block = new DialogueBlock(blockDto.blockId, lines);
                blocks.Add(block.BlockId, block);
            }

            return new DialogueChapter(
                chapterDto.chapterId,
                chapterDto.startBlockId,
                blocks);
        }
    }
}
