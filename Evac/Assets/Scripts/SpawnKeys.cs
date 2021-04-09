using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKeys : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public GameObject keyPrefab;
    public static int maxKeys = 5;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < maxKeys; i++)
        {
            spawnKey();
        }

    }

    /*
    Debugging
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            spawnKey();
        }
    }
    */
    public void spawnKey()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));

        Instantiate(keyPrefab, pos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
