using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public AudioSource collectSound;
    //public bool isKeyCollected = false;
    private bool isKeyCollected { get; set; } = false;


    public bool getIsKeyCollected()
    {
        return this.isKeyCollected;
    }

    public void setIsKeyCollected(bool isKeyCollected)
    {
        this.isKeyCollected = isKeyCollected;
    }

    void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        ScoringSystem.score -= 1;
        isKeyCollected = true;
        Destroy(this.gameObject);


    }
}
