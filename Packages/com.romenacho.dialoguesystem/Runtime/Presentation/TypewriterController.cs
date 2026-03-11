using DialogSystem.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DialogSystem.Presentation
{
    public class TypewriterController : MonoBehaviour
    {
        [SerializeField] private TMP_Text textUI;
        [SerializeField] private float typingSpeed = 0.03f;
        [SerializeField] private int maxVisibleLines = 2;
        [SerializeField] private DialogueRunner dialogueRunner;

        [SerializeField] private CanvasGroup continueIcon;
        [SerializeField] private float fadeDuration = 0.25f;

        private readonly Queue<string> _visibleLines = new Queue<string>();
        private Coroutine _typingRoutine;
        private string _currentLine;
        private bool _isTyping;

        private Coroutine _fadeRoutine;
        public bool IsTyping => _isTyping;

        public event Action OnDialogueFinished;
        public event Action OnLineTypingCompleted;



        public void Play(string newLine)
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);

            AddLineToBuffer(newLine);

            _typingRoutine = StartCoroutine(TypeRoutine(newLine));
        }

        private void AddLineToBuffer(string line)
        {
            _visibleLines.Enqueue(line);

            if (_visibleLines.Count > maxVisibleLines)
                _visibleLines.Dequeue();
        }

        private IEnumerator TypeRoutine(string line)
        {
            _isTyping = true;
            _currentLine = line;

            string previousText = GetPreviousLinesText();

            for (int i = 0; i <= line.Length; i++)
            {
                textUI.text = previousText + line.Substring(0, i);
                yield return new WaitForSeconds(typingSpeed);
            }

            textUI.text = previousText + line;
            _isTyping = false;
            _typingRoutine = null;

            OnLineTypingCompleted?.Invoke();

        }

        private string GetPreviousLinesText()
        {
            if (_visibleLines.Count <= 1)
                return "";

            string result = "";
            int index = 0;

            foreach (var line in _visibleLines)
            {
                index++;

                // omitimos la última porque es la actual
                if (index == _visibleLines.Count)
                    break;

                result += line + "\n";
            }

            return result;
        }

        public void Skip()
        {
            if (!_isTyping)
                return;

            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);

            string previousText = GetPreviousLinesText();
            textUI.text = previousText + _currentLine;

            _isTyping = false;
            _typingRoutine = null;
            OnLineTypingCompleted?.Invoke();
        }


        private void ClearTextInternal()
        {
            if (textUI != null)
                textUI.text = "";

            _visibleLines.Clear();
            _currentLine = null;
            _isTyping = false;

            if (_typingRoutine != null)
            {
                StopCoroutine(_typingRoutine);
                _typingRoutine = null;
            }
        }

        private void Awake()
        {
            if (continueIcon != null)
            {
                continueIcon.alpha = 0f;
                continueIcon.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            dialogueRunner.OnDialogueFinished += ClearTextInternal;

            OnLineTypingCompleted += ShowContinueIcon;
            dialogueRunner.OnLineStarted += HideContinueIconFromLine;
            dialogueRunner.OnDialogueFinished += HideContinueIcon;
        }



        private void OnDisable()
        {
            dialogueRunner.OnDialogueFinished -= ClearTextInternal;

            OnLineTypingCompleted -= ShowContinueIcon;
            dialogueRunner.OnLineStarted -= HideContinueIconFromLine;
            dialogueRunner.OnDialogueFinished -= HideContinueIcon;
        }

        private void ShowContinueIcon()
        {
            //note: To show icon only if has next line, then add to if>  || !dialogueRunner.HasNextLine
            if (!dialogueRunner.IsRunning || continueIcon == null)
                return;

            FadeIcon(1f);
        }

        private void HideContinueIcon()
        {
            FadeIcon(0f);
        }

        private void HideContinueIconFromLine(string _)
        {
            FadeIcon(0f);
        }


        private void FadeIcon(float targetAlpha)
        {
            if (continueIcon == null)
                return;

            if (_fadeRoutine != null)
                StopCoroutine(_fadeRoutine);

            _fadeRoutine = StartCoroutine(FadeRoutine(targetAlpha));
        }

        private IEnumerator FadeRoutine(float targetAlpha)
        {
            continueIcon.gameObject.SetActive(true);

            float startAlpha = continueIcon.alpha;
            float time = 0f;

            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                float t = time / fadeDuration;
                continueIcon.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                yield return null;
            }

            continueIcon.alpha = targetAlpha;

            if (Mathf.Approximately(targetAlpha, 0f))
                continueIcon.gameObject.SetActive(false);

            _fadeRoutine = null;
        }
    }
}