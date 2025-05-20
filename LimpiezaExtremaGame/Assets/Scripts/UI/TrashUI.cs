using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashUI : MonoBehaviour, IDataPersistence
{
    [Header("Componentes")]
    [SerializeField] private TextMeshProUGUI trashText;

    private int trashCollected = 0;

    private void OnEnable()
    {
        GameEventsManager.instance.trashEvents.onTrashChange += TrashChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.trashEvents.onTrashChange -= TrashChange;
    }

    public void LoadData(GameData data)
    {
        trashCollected = 0;
        foreach (KeyValuePair<string, bool> pair in data.trashCollected)
        {
            if (pair.Value)
            {
                trashCollected++;
            }
        }
        UpdateTrashText();
    }

    public void SaveData(ref GameData data)
    {
        // no data needs to be saved for this script
    }

    private void TrashChange(int trash)
    {
        trashCollected = trash;
        UpdateTrashText();
    }

    private void UpdateTrashText()
    {
        trashText.text = trashCollected.ToString();
    }

}
