using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SoupUIController : MonoBehaviour
{
    private Soup soup;
    private Inventaire inventory;
    private Renderer soupRenderer;

    public GameObject soupSurface;
    public ParticleSystem soupBubbles;
    public GameObject inventoryItem;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        inventory = new Inventaire();
        //Create soup
        soup = new Soup();
        soupRenderer = soupSurface.GetComponent<Renderer>();
        //Debug.Log("New Controller");
        //Generate Inventory
        generateInventoryUI();
    }

    public void AddLegToSoup(Legume legume)
    {
        soup.AddLegume(legume);
        soupRenderer.material.SetColor("_Color", soup.computeColor());

        soupBubbles.GetComponent<Renderer>().material.SetColor("_Color", soup.computeColor());
        soupBubbles.startColor = soup.computeColor();
    }

    public void AddIngToSoup(Ingredient ingredient)
    {
        soup.AddIngredient(ingredient);
    }

    public void AddMixedBitsToSoup(Legume veg)
    {
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            Vector3 spawnPos = new Vector3 (
                Random.Range(-.8f, .8f),
                soupRenderer.transform.localPosition.y + .5f,
                Random.Range(-.8f, .8f)
            );
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(0f, 90f), Vector3.left);
            Instantiate(veg.mixedObject, Vector3.back, randomRotation, soupRenderer.transform).transform.localPosition = spawnPos; 
            
        }
    }

    public void AddMixedBitsToSoup(Ingredient ing)
    {
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(-.8f, .8f),
                soupRenderer.transform.localPosition.y + .5f,
                Random.Range(-.8f, .8f)
            );
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(0f, 90f), Vector3.left);

            Instantiate(ing, Vector3.back, randomRotation, soupRenderer.transform).transform.localPosition = spawnPos;

        }
    }


    public void RemoveVegFromInv(Legume leg)
    {
        Inventaire inventaire = Inventaire.instance;

        int currentValue = inventaire.inventaireLegumes[leg];
        inventaire.inventaireLegumes[leg] = currentValue - 1;
        UpdateInventoryUI();

    }

    public void RemoveIngFromInv(Ingredient ing)
    {
        Inventaire inventaire = Inventaire.instance;

        int currentValue = inventaire.inventaireIngredients[ing];
        inventaire.inventaireIngredients[ing] = currentValue - 1;
        UpdateInventoryUI();

    }

    private void UpdateInventoryUI()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("inventoryItem");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
        generateInventoryUI();
    }


    public void AddLegToInv()
    {
        Debug.Log("feur");
    }

    public void generateInventoryUI()
    {
        //Debug.Log("Generate Inventory");
        
        Inventaire inventaire = Inventaire.instance;
        GameObject itemSpawner = GameObject.FindGameObjectWithTag("ItemSpawner");
        foreach (KeyValuePair<Legume, int> kvp in inventaire.inventaireLegumes)
        {
            //Debug.Log(kvp.Key.nom);
            if (kvp.Value > 0)
            {
                GameObject newItem = createLegumeInventoryItem(itemSpawner.transform, kvp.Value, kvp.Key);
                newItem.transform.parent = itemSpawner.transform;
            }
        }
        foreach (KeyValuePair<Ingredient, int> kvp in inventaire.inventaireIngredients)
        {
            //Debug.Log(kvp.Key.nom);
            if (kvp.Value > 0)
            {
                GameObject newItem = createIngInventoryItem(itemSpawner.transform, kvp.Value, kvp.Key);
                newItem.transform.parent = itemSpawner.transform;
            }
        }
    }
    public GameObject createLegumeInventoryItem(Transform itemSpawner, int numberOfIngredient, Legume legume)
    {
        //Create one Inventory Item by giving everything to a prefab of a UI element
        GameObject newItem;
        newItem = Instantiate(inventoryItem, itemSpawner);
        newItem.AddComponent<Legume>();
        newItem.GetComponent<Legume>().couleur = legume.couleur;
        newItem.GetComponent<Legume>().name = legume.name;
        newItem.GetComponent<Legume>().nom = legume.nom;
        newItem.GetComponent<Legume>().objet = legume.objet;
        newItem.GetComponent<Legume>().isMixed = legume.isMixed;
        newItem.GetComponent<Legume>().mixedObject = legume.mixedObject;
        newItem.GetComponent<DragDrop>().canvas = canvas;
        newItem.transform.GetChild(1).GetComponent<MeshFilter>().mesh = legume.objet;
        newItem.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfIngredient;
        return newItem;
    }

    public GameObject createIngInventoryItem(Transform itemSpawner, int numberOfIngredient, Ingredient ing)
    {
        GameObject newItem;
        newItem = Instantiate(inventoryItem, itemSpawner);
        newItem.AddComponent<Ingredient>();
        newItem.GetComponent<Ingredient>().nom = ing.nom;
        newItem.GetComponent<Ingredient>().objet = ing.objet;
        newItem.GetComponent<DragDrop>().canvas = canvas;
        newItem.transform.GetChild(1).GetComponent<MeshFilter>().mesh = ing.objet;
        newItem.transform.GetChild(1).GetComponent<Renderer>().sharedMaterial = ing.GetComponent<Renderer>().sharedMaterial;
        newItem.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfIngredient;

        return newItem;
    }

    public void EndSoup() 
    {
        foreach(var ing in soup.GetIngredients())
        {
            GameManager.instance.ingredientsSoup.Add(ing.Clone());
        }

        //GameManager.instance.soup = soup.Clone();
        
        GameManager.instance.loadDinnerScene(soup);
    }

}
