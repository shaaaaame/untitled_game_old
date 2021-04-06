using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private new Collider collider;
    private Animator anim;

    [SerializeField] private GameObject floatText;

    public UnityEvent OnInteract;

    private void Awake()
    {
        collider = this.GetComponent<Collider>();
        anim = this.GetComponent<Animator>();
    }

    //called from PlayerInput script whenever key is pressed
    public void Interact()
    {
        if (OnInteract != null)
        {
            OnInteract.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            anim.SetBool("Interactable", true);
        }
        floatText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            anim.SetBool("Interactable", false);
        }
        floatText.SetActive(false);
    }
}
