using UnityEngine;
using DialogSystem.Core;
using DialogSystem.Localization;
using DialogSystem.Voice;
using DialogSystem.Presentation;
using DialogSystem.Data;

public class DialogueBootstrap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueRunner runner;
    [SerializeField] private TypewriterController typewriter;
    [SerializeField] private JsonStructureLoader structureLoader;
    [SerializeField] private JsonLocalizationProvider localizationProvider;
    [SerializeField] private JsonVoiceProvider voiceProvider;

    [Header("JSON Files")]
    [SerializeField] private TextAsset structureJson;
    [SerializeField] private TextAsset localizationJson;
    [SerializeField] private TextAsset voiceJson;

    private void Start()
    {
        var structure = structureLoader.Load(structureJson);
        localizationProvider.Load(localizationJson);
        voiceProvider.Load(voiceJson);

        runner.Initialize(structure, localizationProvider, voiceProvider);

        runner.OnLineStarted += typewriter.Play;

        runner.StartChapter("CH1");
    }
}

