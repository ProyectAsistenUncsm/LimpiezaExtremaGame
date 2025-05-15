using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public GoldEvents goldEvents;
    public MiscEvents miscEvents;
    public TrashEvents trashEvents;
    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    public QuestEvents questEvents;
    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Se encontro mas de un Game Event Manager en la escena.");
        }
        instance = this;

        //Se inician todos los eventos
        goldEvents = new GoldEvents();
        miscEvents = new MiscEvents();
        trashEvents = new TrashEvents();
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        questEvents = new QuestEvents(); 
        dialogueEvents = new DialogueEvents();
    }

}
