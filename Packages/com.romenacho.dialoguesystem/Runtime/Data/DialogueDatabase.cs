using UnityEngine;


namespace DialogSystem.Data
{
    [CreateAssetMenu(menuName = "DialogSystem/DialogueDatabase")]
    public class DialogueDatabase : ScriptableObject
    {
        public TextAsset structureJson;
        public TextAsset localizationJson;
        public TextAsset voiceJson;
    }
}
