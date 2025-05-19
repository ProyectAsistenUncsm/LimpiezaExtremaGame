using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour, IDataPersistence
{
    [Header("Configuracion")]
    [SerializeField] private int startingTrash = 0;

    public int currentTrash { get; private set; }

    // Registrar que basura fue recogida 
    private Dictionary<string, bool> trashCollected = new Dictionary<string, bool>();

    private void Awake()
    {
        currentTrash = startingTrash;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.trashEvents.onTrashGained += TrashGained;
        GameEventsManager.instance.trashEvents.onTrashCollected += TrashCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.trashEvents.onTrashGained -= TrashGained;
        GameEventsManager.instance.trashEvents.onTrashCollected -= TrashCollected;
    }
    private void Start()
    {
        GameEventsManager.instance.trashEvents.TrashChange(currentTrash);
    }

    private void TrashGained(int trash)
    {
        currentTrash += trash;
        GameEventsManager.instance.trashEvents.TrashChange(currentTrash);
    }

    private void TrashCollected(string trashId)
    {
        if (!trashCollected.ContainsKey(trashId))
        {
            trashCollected[trashId] = true;
        }
    }

    public void LoadData(GameData data)
    {
        this.currentTrash = data.currentTrash;

        this.trashCollected = new Dictionary<string, bool>();

        if (data.trashCollected != null)
        {
            foreach (TrashSaveData entry in data.trashCollected)
            {
                this.trashCollected[entry.id] = entry.collected;
            }
        }

        // Actualizar UI despues de cargar
        GameEventsManager.instance.trashEvents.TrashChange(currentTrash);
    }

    public void SaveData(ref GameData data)
    {
        data.currentTrash = this.currentTrash;

        data.trashCollected = new List<TrashSaveData>();
        foreach (TrashSaveData trashData in data.trashCollected)
        {
            string id = trashData.id;
            bool collected = trashData.collected;
        }



        //data.trashCollected = new SerializableDictionary<string, bool>();
        //foreach (var kvp in this.trashCollected)
        //{
        //    this.trashCollected.Add(kvp.Key, kvp.Value);
        //}
    }
}
