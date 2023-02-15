using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoInteractionComponent : MonoBehaviour
{
    public void DoInteraction(GameObject gameObject)
    {
        InteractableComponent interactableComponent = gameObject.GetComponent<InteractableComponent>();
        if(interactableComponent != null) interactableComponent.Interact();
    }
}
