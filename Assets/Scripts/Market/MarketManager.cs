using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ItemPrefab
{
    public GameObject PosObject;
    public GameObject ObjectPrefab;
}


public class MarketManager : MonoBehaviour, IDataPersistence
{
    Dictionary<Ingredient, int> ingredient_to_put;
    public List<ItemPrefab> list_prefab;
    public int minimumNumberVegetablesToBuy;
    public List<Sprite> listGuest;
    public Image currentGuest;
    private ErrorNotificationController errorNotificationController;


    private void Start() {
        errorNotificationController = GameObject.FindObjectOfType<ErrorNotificationController>();
        float instancy = 0;
        ingredient_to_put = GameManager.instance.InitMarket();
        GameObject PosObject = GameObject.FindWithTag("PosCarrots");
        GameObject ObjectPrefab = Resources.Load<GameObject>("carrotPrefab");

        foreach (KeyValuePair<Ingredient, int> ingredient in ingredient_to_put)  
        {  
            foreach (ItemPrefab item in list_prefab)
            { 
                if(ingredient.Key.nom == item.ObjectPrefab.GetComponent<Legume>().nom){
                    PosObject = item.PosObject;
                    ObjectPrefab = item.ObjectPrefab;
                    break;
                }
            }
            instancy = 0;
            while(instancy < ingredient.Value){
                Instantiate(ObjectPrefab, PosObject.transform.position + new Vector3(0, instancy * 3, 0) , Quaternion.identity);
                instancy++;
            }
        } 
    }

    public void LoadData(GameData data)
    {
        Inventaire.instance = data.inventory;
    }

    public void SaveData(ref GameData data)
    {
        data.inventory = Inventaire.instance;
    }

    private void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, Mathf.Infinity)){
            //if the cursor hits an ingredient, add it to the inventory
            if(hit.collider.gameObject.GetComponent<Legume>() != null){
                hit.collider.gameObject.layer = 3;
                minimumNumberVegetablesToBuy--;

                Destroy(hit.collider.gameObject.GetComponent<Legume>());
                Inventaire.instance.AddLegume(hit.collider.gameObject.GetComponent<Legume>());
                hit.collider.gameObject.transform.position = GameObject.FindWithTag("PosBasket").transform.position;
                hit.collider.gameObject.transform.SetParent(GameObject.FindWithTag("PosBasket").transform);
            }
            // if the cursor hits a character, set the guest 
            if(hit.collider.gameObject.GetComponent<Character>() != null)
            {
                if (GameManager.instance.guest == null) 
                {
                    onChangeGuest(hit.collider.gameObject.GetComponent<Character>().name);
                }
                else if (!GameManager.instance.guest.Equals(hit.collider.gameObject.name))
                {
                    onChangeGuest(hit.collider.gameObject.GetComponent<Character>().name);
                }
            }
        }
    }

    // change the current guest
    private void onChangeGuest(string guestName)
    {
        GameManager.instance.guest = guestName;
        changeImage(guestName);
    }

    // change the current selected guest visual
    private void changeImage(string guestName)
    {
        foreach(Character ch in GameManager.instance.characterList)
        {
            if(ch.name.Equals(guestName))
            {
                currentGuest.sprite = ch.selectedSprite;
            }
        }
    }

    public void loadKitchen(){
        Inventaire.instance.nbLegumeInInventory();
        if (Inventaire.instance.nbLegumeInInventory() >= 3 && !GameManager.instance.guest.Equals("")) {
            GameManager.instance.loadKitchenScene();
        }
        else{
            Debug.Log("show minimumNumberVegetablesToBuy : " + this.minimumNumberVegetablesToBuy + "nb leg invent" + Inventaire.instance.nbLegumeInInventory() );
            if (this.minimumNumberVegetablesToBuy > 0)
                errorNotificationController.showNotification("you must have at least " + minimumNumberVegetablesToBuy + " vegetables in your inventory to leave the market");
            if (GameManager.instance.guest.Equals(""))
                errorNotificationController.showNotification("Pick a guest please");
        }
    }


}

