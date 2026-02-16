namespace DialogSystem.Core
{
    public sealed class DialogueGraph
    {
        private readonly DialogueChapter _chapter;

        private DialogueBlock _currentBlock;
        private int _currentLineIndex;

        public DialogueGraph(DialogueChapter chapter)
        {
            _chapter = chapter ?? throw new System.ArgumentNullException(nameof(chapter));
            _currentBlock = chapter.GetStartBlock();
            _currentLineIndex = 0;
        }

        public DialogueLine CurrentLine =>
            _currentBlock.Lines[_currentLineIndex];

        public bool HasNextLine =>
            _currentLineIndex < _currentBlock.Lines.Count - 1;

        public void MoveNext()
        {
            if (!HasNextLine)
                return;

            _currentLineIndex++;
        }

        public bool IsBlockFinished =>
            _currentLineIndex >= _currentBlock.Lines.Count - 1;
    }
}
