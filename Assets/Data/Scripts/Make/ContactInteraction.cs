using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ContactInteraction : MonoBehaviour
{
    [SerializeField] private List<Interaction> interactions;
    private bool isGrab = true;

    private void OnTriggerEnter(Collider other)
    {
        var temp = from interaction in interactions
                   where interaction.tag == other.tag && interaction.isGrab == isGrab
                   select interaction;
        foreach(var i in temp)
        {
            i.Action();
        }
    }
}

[Serializable]
public class Interaction
{
    public string tag;
    public bool isGrab;
    public UnityEvent actions;

    public void Action()
    {
        actions?.Invoke();
    }
}