using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Inventaire inventory;
    public Character[] characters;

    public GameData()
    {
        this.inventory = new Inventaire();  
        this.characters = new Character[0];
    }
}
