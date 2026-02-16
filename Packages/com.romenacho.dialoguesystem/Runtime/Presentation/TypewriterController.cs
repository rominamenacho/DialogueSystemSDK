using System.Collections;
using UnityEngine;
using TMPro;

namespace DialogSystem.Presentation
{
    using System.Collections;
    using UnityEngine;

    public sealed class TypewriterController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_Text textComponent;

        [Header("Settings")]
        [SerializeField] private float charactersPerSecond = 30f;

        private Coroutine _typingCoroutine;
        private string _currentText;
        private bool _isTyping;

        public bool IsTyping => _isTyping;

        public void Play(string text)
        {
            if (textComponent == null)
                return;

            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);

            _currentText = text ?? string.Empty;
            _typingCoroutine = StartCoroutine(TypeRoutine());
        }

        public void Skip()
        {
            if (!_isTyping)
                return;

            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);

            textComponent.text = _currentText;
            _isTyping = false;
        }

        private IEnumerator TypeRoutine()
        {
            _isTyping = true;
            textComponent.text = string.Empty;

            float delay = 1f / Mathf.Max(1f, charactersPerSecond);

            foreach (char c in _currentText)
            {
                textComponent.text += c;
                yield return new WaitForSeconds(delay);
            }

            _isTyping = false;
        }
    }
}
