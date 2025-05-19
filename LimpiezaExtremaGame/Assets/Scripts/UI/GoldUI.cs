using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private TextMeshProUGUI goldText;

    private void OnEnable()
    {
        GameEventsManager.instance.goldEvents.onGoldChange += GoldChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.goldEvents.onGoldChange -= GoldChange;
    }

    private void GoldChange(int gold)
    {
        goldText.text = gold.ToString();
    }
}
