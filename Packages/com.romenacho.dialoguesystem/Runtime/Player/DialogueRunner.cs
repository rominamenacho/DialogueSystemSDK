using DialogSystem.Core;
using DialogSystem.Interfaces;
using System;
using UnityEngine;

namespace DialogSystem.Player
{
    public sealed class DialogueRunner : MonoBehaviour, IDialogueRunner
    {
        private DialogueGraph _graph;
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
            _graph = new DialogueGraph(chapter);
            _localization = localizationProvider;
            _voice = voiceProvider;
        }

        public void StartDialogue()
        {
            if (_graph == null || _localization == null)
                return;

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
