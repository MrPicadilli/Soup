using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ErrorNotificationController : MonoBehaviour
{
    [SerializeField] Text ErrorTextLabel;
    private Animator m_animator;
    private void Awake() {
        m_animator = GetComponent<Animator>();
    }
    public void showNotification(string error){
        ErrorTextLabel.text = error;
        m_animator.SetTrigger("Appear");
    }

}
