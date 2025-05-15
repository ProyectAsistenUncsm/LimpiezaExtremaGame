using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float respawnTimeSeconds = 8;
    [SerializeField] private int golGained = 1;

    private CircleCollider2D circleCollider;
    private SpriteRenderer visual;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        visual = GetComponentInChildren<SpriteRenderer>();
    }

    private void CollectCoin()
    {
        circleCollider.enabled = false;
        visual.gameObject.SetActive(false);
        GameEventsManager.instance.goldEvents.GoldGained(golGained);
        GameEventsManager.instance.miscEvents.CoinCollected();
        StopAllCoroutines();
        StartCoroutine(RespawnAfterTime());
    }

    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTimeSeconds);
        circleCollider.enabled=true;
        visual.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            CollectCoin();
        }
    }
}
