using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour, IDataPersistence
{
    [Header("Configuracion")]
    [SerializeField] private int startingGold = 0;

    public int currentGold { get; private set; }

    private void Awake()
    {
        currentGold = startingGold;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.goldEvents.onGoldGained += GoldGained;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.goldEvents.onGoldGained -= GoldGained;
    }
    private void Start()
    {
        GameEventsManager.instance.goldEvents.GoldChange(currentGold);
    }

    private void GoldGained(int gold)
    {
        currentGold += gold;
        GameEventsManager.instance.goldEvents.GoldChange(currentGold);
    }

    public void LoadData(GameData data)
    {
        currentGold = data.currentGold;
        GameEventsManager.instance.goldEvents.GoldChange(currentGold);
    }

    public void SaveData(ref GameData data)
    {
        data.currentGold = currentGold;
    }
}
