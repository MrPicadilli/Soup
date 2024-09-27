using System.Collections.Generic;
using UnityEngine;

public class Soup : MonoBehaviour
{
    [SerializeField]
    public List<Ingredient> ingredients;
    public int nbLegumes;
    public List<Color> colors;

    private void Awake()
    {
        Debug.Log("Soupe créée");
        ingredients = new List<Ingredient>();
        nbLegumes = 0;
        colors = new List<Color>();
    }

    public Soup()
    {
        ingredients = new List<Ingredient>();
        nbLegumes = 0;
        colors = new List<Color>();
    }

    public Soup(List<Ingredient> ingredients, int nbLegumes, List<Color> colors) 
    {
        this.ingredients = new List<Ingredient>();
        this.nbLegumes = nbLegumes;
        this.colors = new List<Color>();

        foreach(var ing in ingredients)
        {
            this.ingredients.Add(ing.Clone());
        }

        foreach(var col in colors)
        {
            this.colors.Add(new Color(col.r, col.g, col.b));
        }
    }

    public Color computeColor()
    {
        float r = 0, g = 0 , b = 0;
        int sizeIngredients = colors.Count;

        foreach(Color color in colors)
        {
            r += color.r;
            g += color.g;
            b += color.b;
        }

        return new Color(r / sizeIngredients, g / sizeIngredients, b / sizeIngredients);

    }

    public void AddLegume(Legume legume)
    {
        nbLegumes++;
        if (legume.isMixed)
        {
            Color color = legume.couleur;
            colors.Add(color);
        }
        ingredients.Add(legume);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);        
    }

    public List<Ingredient> GetIngredients()
    {
        return ingredients;
    }

    public bool containsIngredient(Ingredient i)
    {
        return ingredients.Contains(i);
    }

    public Soup Clone()
    {
        Debug.Log(ingredients.Count);
        return new Soup(ingredients, nbLegumes, colors);
    }
}
