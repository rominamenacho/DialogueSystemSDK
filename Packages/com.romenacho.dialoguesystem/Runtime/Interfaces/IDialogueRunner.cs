using DialogSystem.Core;
using System;
using System.Collections.Generic;

namespace DialogSystem.Interfaces
{
    public interface IDialogueRunner
    {
        event Action<string> OnLineStarted;
        event Action OnDialogueFinished;

        void Initialize(
            Dictionary<string, DialogueChapter> chapters,
            ILocalizationProvider localizationProvider,
            IVoiceProvider voiceProvider);

        void StartDialogue(string chapterId, string startBlockId = null);
        void Next();
        void PlayImmediate(string text);

        bool IsRunning { get; }
    }
}
