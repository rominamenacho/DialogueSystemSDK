using DialogSystem.Data;
using DialogSystem.Interfaces;
using DialogSystem.Localization;
using DialogSystem.Player;
using DialogSystem.Voice;
using UnityEngine;

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

        [Header("Params ")]
        [SerializeField] private string chapterCode;
        [SerializeField] private string startBlockId;


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

            // ---- LOAD LOCALIZATION ----
            var localizationLoader = new JsonLocalizationLoader();
            var localizationDict = localizationLoader.Load(localizationJson);

            // ---- LOAD STRUCTURE ----
            var structureLoader = new JsonStructureLoader();

            var chapters = structureLoader.Load(structureJson, localizationDict);

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
            runner.Initialize(chapters, localizationProvider, voiceProvider);

            runner.OnLineStarted += typewriter.Play;
            // Note: StartDialogue is called here for demonstration purposes only.
            // For production use, remove this call and invoke it externally via button or script event.
            StartDialogue(chapterCode, startBlockId);

        }

        public void StartDialogue(string chapter, string blockId = null)
        {
            if (string.IsNullOrEmpty(chapter))
            {
                Debug.LogError("DialogueBootstrap: Chapter code is null or empty.");
                return;
            }
            runner.StartDialogue(chapter, blockId);
        }

         public void Next()
        {
            runner.Next();
        }
    }
}