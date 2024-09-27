using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legume : Ingredient
{
    public bool isMixed;
    public Color couleur;
    public GameObject mixedObject;

    public Legume(string nom, Mesh objet, bool isMixed, Color couleur) : base(nom, objet)
    {
        this.isMixed = isMixed;
        this.couleur = couleur;
    }

    public Legume(string nom, Mesh objet, bool isMixed, Color couleur, GameObject mixedObject) : base(nom, objet)
    {
        this.isMixed = isMixed;
        this.couleur = couleur;
        this.mixedObject = mixedObject;
    }

    // Deep copy
    public new Legume Clone()
    {
        return new Legume(nom, objet, isMixed, couleur, mixedObject);
    }
}
