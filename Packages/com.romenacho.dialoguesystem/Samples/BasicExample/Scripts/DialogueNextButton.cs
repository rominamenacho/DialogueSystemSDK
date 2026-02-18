using DialogSystem.Player;
using DialogSystem.Presentation;
using UnityEngine;

public class DialogueNextButton : MonoBehaviour
{
    public DialogueRunner runner;
    public TypewriterController typewriter;

    public void OnClick()
    {
        if (typewriter.IsTyping)
        {
            typewriter.Skip();
            return; // importante: no avanzar en el mismo click
        }

        runner.Next();
    }

}

