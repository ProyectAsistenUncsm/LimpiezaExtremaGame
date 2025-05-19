using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action onCoinCollected;
    public void CoinCollected()
    {
        if(onCoinCollected != null)
        {
            onCoinCollected();
        }
    }

    //Evento que se dispara el jugador recolecta basura
    public event Action onTrashCollected;
    //Metodo que verifica si hay subscriptores y luego dispara el evento
    public void TrashCollected()
    {
        if (onTrashCollected != null)
        {
            onTrashCollected();
        }
    }
}
