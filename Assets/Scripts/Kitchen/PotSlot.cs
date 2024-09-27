using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotSlot : MonoBehaviour, IDropHandler 
{

    private void Awake() {
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
        if (eventData.pointerDrag.GetComponent<Legume>() == null)
        {
            if (eventData.pointerDrag != null)
            {
                Debug.Log("Soupise " + eventData.pointerDrag.GetComponent<Ingredient>().nom);
                Ingredient incomingIng = eventData.pointerDrag.GetComponent<Ingredient>();

                SoupUIController soupUI = FindObjectOfType<SoupUIController>();
                soupUI.AddIngToSoup(incomingIng);
                soupUI.AddMixedBitsToSoup(incomingIng);
                Inventaire inventaire = Inventaire.instance;

                foreach (KeyValuePair<Ingredient, int> kvp in inventaire.inventaireIngredients)
                {
                    //Debug.Log(kvp.Key.nom);
                    if (kvp.Key.nom == incomingIng.nom)
                    {
                        soupUI.RemoveIngFromInv(kvp.Key);
                        DragDrop.updateLayer("UIIngObject", 0);
                    }
                }
            }
        } 
    }
}
