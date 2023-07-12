using Item;
using UnityEngine;
using UnityEngine.Events;

public class DalmoreDropInteraction : MonoBehaviour
{
    public UnityEvent dropEvent;
    public Ingredients Ingredient;

    private void OnTriggerEnter(Collider other)
    {
        var interaction = other.gameObject.GetComponent<IItemDataInteractable>();
        if(interaction != null)
        {
            Drop(interaction);
        }
    }

    public void Drop(IItemDataInteractable interaction)
    {
        interaction.GetInteractionData().Add(Ingredient);
        dropEvent?.Invoke();
    }
}
