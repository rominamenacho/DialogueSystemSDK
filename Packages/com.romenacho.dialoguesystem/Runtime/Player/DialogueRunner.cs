using DialogSystem.Core;
using DialogSystem.Interfaces;
using System;
using UnityEngine;

namespace DialogSystem.Player
{
    public sealed class DialogueRunner : MonoBehaviour, IDialogueRunner
    {
        private DialogueGraph _graph;
        private DialogueChapter _chapter;
        private ILocalizationProvider _localization;
        private IVoiceProvider _voice;

        public event Action<string> OnLineStarted;
        public event Action OnDialogueFinished;

        public bool IsRunning { get; private set; }

        public void Initialize(
            DialogueChapter chapter,
            ILocalizationProvider localizationProvider,
            IVoiceProvider voiceProvider)
        {
            // Defer graph creation so StartDialogue overloads can pick start block
            _chapter = chapter ?? throw new ArgumentNullException(nameof(chapter));
            _localization = localizationProvider;
            _voice = voiceProvider;
        }

        public void StartDialogue()
        {
            if (_chapter == null || _localization == null)
                return;

            _graph = new DialogueGraph(_chapter);
            IsRunning = true;
            PlayCurrentLine();
        }

        // New overload: start at a specific block id
        public void StartDialogue(string startBlockId)
        {
            if (_chapter == null || _localization == null)
                return;

            _graph = new DialogueGraph(_chapter, startBlockId);
            IsRunning = true;
            PlayCurrentLine();
        }

        public void Next()
        {
            if (!IsRunning)
                return;

            if (_graph.HasNextLine)
            {
                _voice?.Stop();
                _graph.MoveNext();
                PlayCurrentLine();
            }
            else
            {
                FinishDialogue();
            }
        }

        private void PlayCurrentLine()
        {
            var lineId = _graph.CurrentLine.Id;

            string text = _localization.GetText(lineId);

            _voice?.Play(lineId);

            OnLineStarted?.Invoke(text);
        }

        private void FinishDialogue()
        {
            IsRunning = false;
            _voice?.Stop();
            OnDialogueFinished?.Invoke();
        }
    }
}
