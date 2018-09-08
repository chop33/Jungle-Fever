using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour {
   
    public float thrust;
    public Transform[] spawnpoint = new Transform[3];
    public Rigidbody prefab;

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < spawnpoint.Length; ++ i)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Rigidbody rigidPrefab;
                rigidPrefab = Instantiate(prefab, spawnpoint[i].position, spawnpoint[i].rotation) as Rigidbody;
                rigidPrefab.AddForce(thrust, 0, 0, ForceMode.Impulse);
            }
        }
    }
}
