using System;

namespace DialogSystem.Interfaces
{
    public interface IDialogueRunner
    {
        event Action<string> OnLineStarted;
        event Action OnDialogueFinished;

        void Initialize(
            Core.DialogueChapter chapter,
            ILocalizationProvider localizationProvider,
            IVoiceProvider voiceProvider);

        void StartDialogue();
        void Next();

        bool IsRunning { get; }
    }
}
