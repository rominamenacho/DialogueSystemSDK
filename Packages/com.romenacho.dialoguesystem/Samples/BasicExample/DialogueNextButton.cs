using UnityEngine;

public class DialogueNextButton : MonoBehaviour
{
   {
    [SerializeField] private DialogueRunner runner;
    [SerializeField] private TypewriterController typewriter;

    public void OnClick()
    {
        if (typewriter.IsTyping)
            typewriter.Skip();
        else
            runner.Next();
    }
}

