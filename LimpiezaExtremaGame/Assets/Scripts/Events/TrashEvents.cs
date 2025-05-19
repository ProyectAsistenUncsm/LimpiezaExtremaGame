using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEvents
{
    //Evento que se dispara cuando se gana basura 
    public event Action<int> onTrashGained;
    /*
     Metodo que invoca el evento onBasuraRecogida si hay subscriptores
     y pasa la nueva cantidad de basura ganada
     */
    public void TrashGained(int trash)
    {
        if(onTrashGained != null)
        {
            onTrashGained(trash);
        }
    }

    //Evento que se dispara cuando hay un cambio en la cantidad de basura
    public event Action<int> onTrashChange;
    /*
     Metodo similar al primero que invoca el evento onBasuraCantCambio
     con la nueva cantidad de basura
     */
    public void TrashChange(int trash)
    {
        if(onTrashChange != null)
        {
            onTrashChange(trash);
        }
    }

    public event Action<string> onTrashCollected;
    public void TrashCollected(string trashId)
    {
        if(onTrashCollected != null)
        {
            onTrashCollected(trashId);
        }
    }
}
