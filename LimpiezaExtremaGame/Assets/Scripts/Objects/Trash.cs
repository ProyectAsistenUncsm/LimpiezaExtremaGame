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
    }

    public void LoadData(GameData data)
    {
        if(data.trashCollected.TryGetValue(id, out bool alreadyCollected) && alreadyCollected)
        {
            circleCollider.enabled = false;
            visual.gameObject.SetActive(false);
            //GameEventsManager.instance.trashEvents.TrashGained(trashGained);
        }
    }

    public void SaveData(ref GameData data)
    {
        //if (!data.trashCollected.ContainsKey(id))
        //{
        //    data.trashCollected.Add(id, !circleCollider.enabled);
        //}

        if (data.trashCollected.ContainsKey(id))
        {
            // Se actualiza si el valor ya existe
            data.trashCollected[id] = !circleCollider.enabled;
        }
        else
        {
            // Si no existe se agrega
            data.trashCollected.Add(id, !circleCollider.enabled);
        }
    }

    private void CollectTrash()
    {
        circleCollider.enabled = false;
        visual.gameObject.SetActive(false);
        GameEventsManager.instance.trashEvents.TrashGained(trashGained);
        GameEventsManager.instance.miscEvents.TrashCollected();
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
