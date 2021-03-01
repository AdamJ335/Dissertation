using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;
    private int direction;
    public float moveAmount;
    private float timeBtwRoom;
    private float startTimeBtwRoom = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);

    }
    private void Move()
    {
        if (direction == 1 || direction == 2)
        { //Move RIGHT
            Vector3 newPos = new Vector3(transform.position.x + moveAmount, 0, transform.position.z);
            transform.position = newPos;
        }
        else if (direction == 3 || direction == 4)
        { //Move LEFT
            Vector3 newPos = new Vector3(transform.position.x - moveAmount, 0, transform.position.z);
            transform.position = newPos;
        }
        else if (direction == 5)
        { //Move DOWN
            Vector3 newPos = new Vector3(transform.position.x, 0, transform.position.z - moveAmount);
            transform.position = newPos;
        }

        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }

    }
}
