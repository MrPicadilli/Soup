using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;


public class LongClickLeftMarket : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler
{

    bool pressing = false;  
    private float pointerDownTimer;
    private int scaleFactor = 5;
    
    public GameObject rightArrow;

    private void Start() {
        if(Camera.main.transform.position.z < 0.5 * scaleFactor)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void Update() {
        if(pressing && Camera.main.transform.position.z > -0.5 * scaleFactor)
        {
            Camera.main.transform.Translate(Vector3.left * (Time.deltaTime*2) * scaleFactor);
        }
        if(Camera.main.transform.position.z < 10 * scaleFactor)
        {
            rightArrow.SetActive(true);
        }
    } 
 
    public void OnPointerDown (PointerEventData eventData)
    {
        if(Camera.main.transform.position.z < 0 * scaleFactor)
        {
            this.gameObject.SetActive(false);
        }
        if(Camera.main.transform.position.z < 10 * scaleFactor)
        {
            rightArrow.SetActive(true);
        }
        pressing = true;
    }
   
   
    public void OnPointerUp (PointerEventData eventData)
    {
        if(Camera.main.transform.position.z < 0 * scaleFactor)
        {
            this.gameObject.SetActive(false);
        }
        if(Camera.main.transform.position.z < 10 * scaleFactor)
        {
            rightArrow.SetActive(true);
        }
        pressing = false;
    }


}
