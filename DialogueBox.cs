using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI fillText;

    public bool endOfSentence;
    public float speed = 0.02f;

    public void EnterDialogue()
    {
        dialogueBox.SetActive(true);
    }

    public void ExitDialogue()
    {
        dialogueBox.SetActive(false);
    }

    public IEnumerator DisplaySentence(string sentence)
    {
        fillText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            fillText.text += letter;
            Debug.Log(letter);
            yield return new WaitForSeconds(speed);
        }

        endOfSentence = true;

    }

}
