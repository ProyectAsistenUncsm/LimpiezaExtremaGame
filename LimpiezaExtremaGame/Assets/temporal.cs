using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasuraTest : MonoBehaviour
{
    public GameObject prefab;

    void Start()
    {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

