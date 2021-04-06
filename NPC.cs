using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class NPC : MonoBehaviour
{
    
    private Collider collider;
    [SerializeField] private GameObject text;

    private DialogueBox dialogueScript; // canvas that has dialogue box as children

    private bool interactable = false;
    public bool inDialogue = false;
    private bool pressed = false;

    private Queue<string> sentences;
    public Dialogue dialogue;

    void Awake()
    {
        collider = GetComponent<Collider>();
        dialogueScript = FindObjectOfType<DialogueBox>();
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (interactable)
        {
            text.SetActive(true);
        }
        else text.SetActive(false);

        if (pressed)
        {
            if (inDialogue && dialogueScript.endOfSentence)
            {
                ContinueDialogue();
                dialogueScript.endOfSentence = false;
            }
            else if (!inDialogue) StartDialogue();

            pressed = false;
        }

    }

    public void Trigger()
    {
        pressed = true;
    }
    public void StartDialogue()
    {
        dialogueScript.EnterDialogue();
        interactable = false;
        PlayerInput.LockPos();
        inDialogue = true;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        dialogueScript.nameText.text = dialogue.name;

        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StartCoroutine(dialogueScript.DisplaySentence(sentence));
    }

    public void EndDialogue()
    {
        dialogueScript.ExitDialogue();
        interactable = true;
        PlayerInput.LockPos();
        inDialogue = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            interactable = false;
        }
    }

}
