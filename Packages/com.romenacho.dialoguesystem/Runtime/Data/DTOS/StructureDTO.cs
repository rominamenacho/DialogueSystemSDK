using System;
using System.Collections.Generic;

namespace DialogSystem.Data.DTO
{
    [Serializable]
    public class StructureDTO
    {
        public List<ChapterDTO> chapters;
    }

    [Serializable]
    public class ChapterDTO
    {
        public string chapterId;
        public string startBlockId;
        public List<BlockDTO> blocks;
    }

    [Serializable]
    public class BlockDTO
    {
        public string blockId;
        public List<LineDTO> lines;
    }

    [Serializable]
    public class LineDTO
    {
        public string lineId;
    }
}
