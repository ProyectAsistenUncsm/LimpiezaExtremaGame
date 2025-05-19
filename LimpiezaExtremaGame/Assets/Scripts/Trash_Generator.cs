using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash_Generator : MonoBehaviour
{

    public GameObject trashPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTrash(Vector3 position)
    {
        GameObject trash = Instantiate(trashPrefab, position, Quaternion.identity);
        trash.transform.SetParent(transform);
    }
}
