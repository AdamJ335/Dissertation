using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;
    public int Length;
    public int X;
    public int Z;

    private bool updatedDoors = false;

    public Room(int x, int z)
    {
        X = x;
        Z = z;
    }
    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors = new List<Door>();

    void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            removeUselessDoors();
            updatedDoors = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("ERROR! Play in wrong scene");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.up:
                    topDoor = d;
                    break;
                case Door.DoorType.down:
                    bottomDoor = d;
                    break;
            }
        }
        RoomController.instance.RegisterRoom(this);
    }

    public void removeUselessDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if (getRight() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.left:
                    if (getLeft() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.up:
                    if (getTop() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.down:
                    if (getBottom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public Room getRight()
    {
        if (RoomController.instance.doesRoomExist(X + 1, Z))
        {
            return RoomController.instance.FindRoom(X + 1, Z);
        }
        return null;
    }
    public Room getLeft()
    {
        if (RoomController.instance.doesRoomExist(X - 1, Z))
        {
            return RoomController.instance.FindRoom(X - 1, Z);
        }
        return null;
    }
    public Room getTop()
    {
        if (RoomController.instance.doesRoomExist(X, Z + 1))
        {
            return RoomController.instance.FindRoom(X, Z + 1);
        }
        return null;
    }
    public Room getBottom()
    {
        if (RoomController.instance.doesRoomExist(X, Z - 1))
        {
            return RoomController.instance.FindRoom(X, Z - 1);
        }
        return null;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, 0, Length));
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, 0, Z * Length);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
