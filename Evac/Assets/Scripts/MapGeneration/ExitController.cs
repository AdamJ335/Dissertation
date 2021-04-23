using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    //generates Timer instance to record time then save it.
    Timer instance = new Timer();

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            instance.StopTime();
            string time = instance.getTime();
            Save(time);
            Destroy(other);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        }
    }

    private void Save(string time)
    {
        //Save to a txt file
        File.WriteAllText(Application.persistentDataPath + "/save.txt", "Time Taken: " + time);
    }
}
