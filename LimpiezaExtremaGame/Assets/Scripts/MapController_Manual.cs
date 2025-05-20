using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapController_Manual : MonoBehaviour, IDataPersistence
{
    public static MapController_Manual Instance { get; set; }

    [Header("Minimapa UI")]
    public RectTransform mapContainer;
    public GameObject mapParent;
    List<Image> mapImages;

    [Header("Icono y colores")]
    public Color highlightColour = Color.yellow;
    public Color dimmedColour = new Color(1f, 1f, 1f, 0.5f);

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        mapImages = mapParent.GetComponentsInChildren<Image>().ToList();
    }

    public void HighlightArea(string areaName)
    {
        foreach(Image area in mapImages)
        {
            area.color = dimmedColour;
        }

        Image currentArea = mapImages.Find(x => x.name == areaName);
        if (currentArea == null)
        {
            Debug.LogWarning("Area no ha sido encontrada: " + areaName);
            return;
        }

        currentArea.color = highlightColour;
    }

    public void LoadData(GameData data)
    {
        if (!string.IsNullOrEmpty(data.currentMapBoundary))
        {
            HighlightArea(data.currentMapBoundary);
        }
    }

    public void SaveData(ref GameData data)
    {
        Image current = mapImages.Find(x => x.color == highlightColour);
        if (current != null)
            data.currentMapBoundary = current.name;
    }
}
