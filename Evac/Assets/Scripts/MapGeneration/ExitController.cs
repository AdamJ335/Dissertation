using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Save();
        }
    }

    private void Save()
    {
        //Save
        File.WriteAllText("C:/Users/Adam Jennings/Desktop/Projects/Dissertation/Evac/save.txt", "test");
    }
}
