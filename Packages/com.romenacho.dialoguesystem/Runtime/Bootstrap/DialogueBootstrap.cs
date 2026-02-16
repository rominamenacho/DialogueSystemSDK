using UnityEngine;
using DialogSystem.Core;
using DialogSystem.Data;
using DialogSystem.Interfaces;
using DialogSystem.Localization;
using DialogSystem.Voice;
using DialogSystem.Player;

namespace DialogSystem.Bootstrap
{
    public sealed class DialogueBootstrap : MonoBehaviour
    {
        [Header("JSON Files")]
        [SerializeField] private TextAsset structureJson;
        [SerializeField] private TextAsset localizationJson;
        [SerializeField] private TextAsset voiceJson;

        [Header("Audio")]
        [SerializeField] private AudioSource audioSource;

        [Header("References")]
        [SerializeField] private DialogueRunner runner;
        [SerializeField] private Presentation.TypewriterController typewriter;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (structureJson == null || localizationJson == null)
            {
                Debug.LogError("DialogueBootstrap: Missing required JSON files.");
                return;
            }

            // ---- LOAD STRUCTURE ----
            var structureLoader = new JsonStructureLoader();
            DialogueChapter chapter = structureLoader.Load(structureJson);

            // ---- LOAD LOCALIZATION ----
            var localizationLoader = new JsonLocalizationLoader();
            var localizationDict = localizationLoader.Load(localizationJson);

            ILocalizationProvider localizationProvider =
                new JsonLocalizationProvider(localizationDict);

            // ---- LOAD VOICE (optional) ----
            IVoiceProvider voiceProvider = null;

            if (voiceJson != null && audioSource != null)
            {
                var voiceLoader = new JsonVoiceLoader();
                var voiceDict = voiceLoader.Load(voiceJson);

                voiceProvider = new JsonVoiceProvider(voiceDict, audioSource);
            }

            // ---- INIT RUNNER ----
            runner.Initialize(chapter, localizationProvider, voiceProvider);

            runner.OnLineStarted += typewriter.Play;
        }

        public void StartDialogue()
        {
            runner.StartDialogue();
        }

        public void Next()
        {
            runner.Next();
        }
    }
}