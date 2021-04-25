using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; //index 0 -> LR, 1->LRB, 2->LRT, 3->ALL
    private int direction;
    public float moveAmount;
    private float timeBtwRoom;
    private float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minZ;
    public bool stopGeneration;

    private int downCounter;

    public LayerMask room;
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
            if (transform.position.x < maxX)
            {
                downCounter = 0;
                Vector3 newPos = new Vector3(transform.position.x + moveAmount, 0, transform.position.z);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                //stops rooms spawning over each other
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5; //forces room to move down if theres no more space
            }

        }
        else if (direction == 3 || direction == 4)
        { //Move LEFT

            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector3 newPos = new Vector3(transform.position.x - moveAmount, 0, transform.position.z);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                //stops rooms spawning over each other
                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5; // forces room layout down
            }

        }
        else if (direction == 5)
        { //Move DOWN
            downCounter++;
            if (transform.position.z > minZ)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector3 newPos = new Vector3(transform.position.x, 0, transform.position.z - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                //STOP LEVEL GENERATION
                stopGeneration = true;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
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
