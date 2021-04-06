using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEditor.Networking.PlayerConnection;

public class SpecialNPC : MonoBehaviour
{
    Collider collider;

    public UnityEvent OnInteract;
    [SerializeField] private GameObject floatText;

    private bool interactable;
    

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (interactable)
        {
            floatText.SetActive(true);
        }
        else floatText.SetActive(false);

    }

    public void Trigger()
    {
        OnInteract.Invoke();
        PlayerInput.LockPos();
        interactable = false;
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
