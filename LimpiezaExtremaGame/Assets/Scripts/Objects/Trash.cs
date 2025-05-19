using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Trash : MonoBehaviour, IDataPersistence
{
    [Header("Configuracion")]
    //[SerializeField] private float respawnTimeSeconds = 8;
    [SerializeField] private int trashGained = 1;

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private CircleCollider2D circleCollider;
    private SpriteRenderer visual;
    private void Awake()
    {
        //Validar si el id de la basura esta vacio
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogWarning($"Trash object '{gameObject.name}' does not have a generated id!");
        }

        circleCollider = GetComponent<CircleCollider2D>();
        visual = GetComponentInChildren<SpriteRenderer>();

        // REGISTRO EN EL SISTEMA DE GUARDADO:
        DataPersistenceManager.instance?.AddDataPersistenceObject(this);
    }

    public void LoadData(GameData data)
    {
        foreach (var trashData in data.trashCollected)
        {
            if (trashData.id == id && trashData.collected)
            {
                circleCollider.enabled = false;
                visual.gameObject.SetActive(false);
                break;
            }
        }
    }


    public void SaveData(ref GameData data)
    {
        bool alreadyExists = false;

        for (int i = 0; i < data.trashCollected.Count;)
        {
            if (data.trashCollected[i].id == id)
            {
                data.trashCollected[i].collected = !circleCollider.enabled;
                alreadyExists = true;
                break;
            }
        }

        if (!alreadyExists)
        {
            data.trashCollected.Add(new TrashSaveData(id, !circleCollider.enabled));
        }
    }

    private void CollectTrash()
    {
        circleCollider.enabled = false;
        visual.gameObject.SetActive(false);
        GameEventsManager.instance.trashEvents.TrashGained(trashGained);
        GameEventsManager.instance.miscEvents.TrashCollected();

        circleCollider.enabled = false;
        visual.gameObject.SetActive(false);

        GameEventsManager.instance.trashEvents.TrashGained(trashGained);
        GameEventsManager.instance.trashEvents.TrashCollected(id);  // <- ESTE es clave


        StopAllCoroutines();
        //StartCoroutine(RespawnAfterTime());
    }



    // Desactivar para que la basura pueda reaparecer despues de un tiempo determinado 
    //private IEnumerator RespawnAfterTime()
    //{
    //    yield return new WaitForSeconds(respawnTimeSeconds);
    //    circleCollider.enabled = true;
    //    visual.gameObject.SetActive(true);
    //}

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            CollectTrash();
        }
    }
}
