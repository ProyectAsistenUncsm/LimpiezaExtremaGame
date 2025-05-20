using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> trashCollected;
    public List<QuestData> activeQuests;
    public int currentTrash;
    public int currentGold;
    public string currentMapBoundary;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to Load 

    public GameData()
    {
        playerPosition = Vector3.zero;
        trashCollected = new SerializableDictionary<string, bool>();
        activeQuests = new List<QuestData>();
        currentTrash = 0;
        currentGold = 0;
    }
}
