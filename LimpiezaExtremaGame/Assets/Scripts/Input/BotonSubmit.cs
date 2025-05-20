using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonSubmit : MonoBehaviour
{
    //script generado por unity al crear los controles
    private Controls controls;
    public Button botonSubmit;

    //DIALOG
    //public DialogueManager dialogueManager;

    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Submit.performed += ctx => Aceptar();
        botonSubmit.onClick.AddListener(Aceptar);
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Aceptar()
    {
        Debug.Log("¡Botón presionado con Input System en móvil!");
        //dialog
        // Simula que se presionó el botón "Submit" estando en contexto DEFAULT
        GameEventsManager.instance.inputEvents.SubmitPressed();
    }



}

















































//public class BotonSubmit : MonoBehaviour
//{
//    //script generado por unity al crear los controles
//    private Controls controls;
//    public Button botonSubmit;

//    private void Awake()
//    {
//        controls = new Controls();
//    }
//    private void OnEnable()
//    {
//        controls.Gameplay.Enable();
//        controls.Gameplay.Submit.performed += ctx => Aceptar();
//        botonSubmit.onClick.AddListener(Aceptar);
//    }

//    private void OnDisable()
//    {
//        controls.Gameplay.Disable();
//    }

//    private void Aceptar()
//    {
//        Debug.Log("¡Botón presionado con Input System en móvil!");

//    }



//}
