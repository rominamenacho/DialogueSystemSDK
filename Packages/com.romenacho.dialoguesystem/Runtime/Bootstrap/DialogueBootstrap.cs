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
        [SerializeField] private DialogueDatabase database;


        [Header("Audio")]
        [SerializeField] private AudioSource audioSource;

        [Header("References")]
        [SerializeField] private DialogueRunner runner;
        [SerializeField] private Presentation.TypewriterController typewriter;


        private void Awake()
        {

            if (database == null)
                throw new System.Exception("DialogueDatabase not found in Resources.");

            Initialize();
        }


        private void Initialize()
        {
            if (database.structureJson == null || database.localizationJson == null)
            {
                Debug.LogError("DialogueBootstrap: Missing required JSON files in DialogueDatabase.");
                return;
            }


            // ---- LOAD LOCALIZATION ----
            var localizationLoader = new JsonLocalizationLoader();
            var localizationDict = localizationLoader.Load(database.localizationJson);

            // ---- LOAD STRUCTURE ----
            var structureLoader = new JsonStructureLoader();

            var chapters = structureLoader.Load(database.structureJson, localizationDict);

            ILocalizationProvider localizationProvider =
                new JsonLocalizationProvider(localizationDict);

            // ---- LOAD VOICE (optional) ----
            IVoiceProvider voiceProvider = null;

            if (database.voiceJson != null && audioSource != null)
            {
                var voiceLoader = new JsonVoiceLoader();
                var voiceDict = voiceLoader.Load(database.voiceJson);

                voiceProvider = new JsonVoiceProvider(voiceDict, audioSource);
            }

            // ---- INIT RUNNER ----
            runner.Initialize(chapters, localizationProvider, voiceProvider);

            runner.OnLineStarted += typewriter.Play;

            // Note: StartDialogue is called here for demonstration purposes only.
            // For production use, remove this call and invoke it externally via button or event.
            //  runner.StartDialogue("CH1", "INTRO");

            //to show dialogs wich is not on the json file, then use this:
            //   runner.PlayImmediate("HELLO WORLD");
        }

        public void Next()
        {
            runner.Next();
        }
    }
}