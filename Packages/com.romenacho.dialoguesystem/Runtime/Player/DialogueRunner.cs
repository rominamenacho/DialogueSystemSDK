using DialogSystem.Core;
using DialogSystem.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem.Player
{
    public sealed class DialogueRunner : MonoBehaviour, IDialogueRunner
    {
        private DialogueGraph _graph;
        private Dictionary<string, DialogueChapter> _chapters;
        private ILocalizationProvider _localization;
        private IVoiceProvider _voice;

        public event Action<string> OnLineStarted;
        public event Action OnDialogueFinished;

        public bool IsRunning { get; private set; }

        public void Initialize(
    Dictionary<string, DialogueChapter> chapters,
    ILocalizationProvider localizationProvider,
    IVoiceProvider voiceProvider)
        {
            _chapters = chapters ?? throw new ArgumentNullException(nameof(chapters));
            _localization = localizationProvider;
            _voice = voiceProvider;
        }

        public void StartDialogue(string chapterId, string startBlockId = null)
        {
            if (!_chapters.ContainsKey(chapterId))
            {
                Debug.LogError($"Chapter {chapterId} not found.");
                return;
            }

            var chapter = _chapters[chapterId];

            _graph = startBlockId == null
                ? new DialogueGraph(chapter)
                : new DialogueGraph(chapter, startBlockId);

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
