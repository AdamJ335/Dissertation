using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public AudioSource collectSound;
    public static bool isKeyCollected = false;

    void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        ScoringSystem.score -= 1;
        isKeyCollected = true;
        Destroy(gameObject);

    }
}
