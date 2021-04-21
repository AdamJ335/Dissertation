using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public string doorType;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (doorType == "right")
            {
                //sends player to room on the right
                other.transform.position += new Vector3(3, 0, 0);
            }
            else if (doorType == "left")
            {
                //sends player to room on the left
                other.transform.position += new Vector3(-3, 0, 0);
            }
            else if (doorType == "up")
            {   //sends player to room above
                other.transform.position += new Vector3(0, 0, 3);
            }
            else if (doorType == "down")
            {   //sends player to room below
                other.transform.position += new Vector3(0, 0, -3);
            }
        }
    }
}
