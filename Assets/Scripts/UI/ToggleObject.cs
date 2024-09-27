using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        GameObject[] livre = null;
        if (gameObject.tag == "RecipeBook")
            livre = GameObject.FindGameObjectsWithTag("CharactersBook");

        if (gameObject.tag == "CharactersBook")
            livre= GameObject.FindGameObjectsWithTag("RecipeBook");

        if (livre.Length != 0)
            livre[0].SetActive(false);

    }
}
