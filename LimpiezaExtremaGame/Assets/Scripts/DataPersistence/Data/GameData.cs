using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public Vector3 playerPosition;
    public List<TrashSaveData> trashCollected;
    public List<QuestData> activeQuests;
    public int currentTrash;
    public int currentGold;
    public string currentMapBoundary;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to Load 

    public GameData()
    {
        playerPosition = Vector3.zero;
        trashCollected = new List<TrashSaveData>();
        activeQuests = new List<QuestData>();
        currentTrash = 0;
        currentGold = 0;
    }
}



public class TrashSaveData
{
    public string id;
    public bool collected;

    public TrashSaveData(string id, bool collected)
    {
        this.id = id;
        this.collected = collected;
    }
}
