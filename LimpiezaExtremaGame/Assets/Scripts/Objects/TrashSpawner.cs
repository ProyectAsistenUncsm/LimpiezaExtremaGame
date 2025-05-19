using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZonaSpawnConfiguracion
{
    public Transform zona;
    public GameObject[] prefabsPermitidos;
    public int cantidadBasura = 5;
}

public class TrashSpawner : MonoBehaviour
{
    [Header("Configuración por zona")]
    public List<ZonaSpawnConfiguracion> configuracionesDeZonas;

    [Header("Chequeo de colisiones")]
    public LayerMask obstaculos;
    public float radioChequeo = 0.5f;
    public int intentosMaximos = 20;

    void Start()
    {
        Debug.Log("👉 Iniciando generación de basura personalizada");

        foreach (var config in configuracionesDeZonas)
        {
            if (config.zona == null)
                Debug.LogError("❌ Una de las zonas de spawn está vacía.");

            if (config.prefabsPermitidos == null || config.prefabsPermitidos.Length == 0)
                Debug.LogError($"❌ La zona {config.zona?.name} no tiene prefabs asignados.");
        }

        GenerarBasura();
    }

    void GenerarBasura()
    {
        foreach (var config in configuracionesDeZonas)
        {
            Transform zona = config.zona;
            GameObject[] prefabs = config.prefabsPermitidos;
            int cantidad = config.cantidadBasura;

            BoxCollider2D area = zona.GetComponent<BoxCollider2D>();
            if (area == null)
            {
                Debug.LogWarning("Zona sin BoxCollider2D: " + zona.name);
                continue;
            }

            for (int i = 0; i < cantidad; i++)
            {
                Vector2 posicionValida = BuscarPosicionValida(area);

                if (!float.IsFinite(posicionValida.x) || !float.IsFinite(posicionValida.y))
                {
                    Debug.LogWarning($"❌ Posición inválida en zona: {zona.name}");
                    continue;
                }

                GameObject prefabElegido = prefabs[Random.Range(0, prefabs.Length)];
                GameObject basuraInstanciada = Instantiate(prefabElegido, new Vector3(posicionValida.x, posicionValida.y, 0), Quaternion.identity);

                var trashScript = basuraInstanciada.GetComponent<Trash>();
                if (trashScript != null)
                {
                    trashScript.SendMessage("GenerateGuid");
                }
            }
        }
    }

    Vector2 BuscarPosicionValida(BoxCollider2D area)
    {
        for (int intento = 0; intento < intentosMaximos; intento++)
        {
            Bounds bounds = area.bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 posicion = new Vector2(x, y);

            Collider2D colision = Physics2D.OverlapCircle(posicion, radioChequeo, obstaculos);
            if (colision == null)
            {
                return posicion;
            }
        }

        return Vector2.positiveInfinity;
    }

    private void OnDrawGizmos()
    {
        if (configuracionesDeZonas == null) return;

        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);

        foreach (var config in configuracionesDeZonas)
        {
            if (config.zona == null) continue;

            BoxCollider2D col = config.zona.GetComponent<BoxCollider2D>();
            if (col != null)
            {
                Bounds b = col.bounds;
                Gizmos.DrawCube(b.center, b.size);
            }
        }
    }
}


