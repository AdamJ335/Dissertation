using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Z;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "Map";

    RoomInfo currentLoadRoomData;
    Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    bool spawnedEndRoom = false;
    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", 0, 1);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, -1);
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedEndRoom)
            {
                StartCoroutine(SpawnEndRoom());
            }
            else if (spawnedEndRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.removeUselessDoors();
                }
                updatedRooms = true;
            }
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnEndRoom()
    {
        spawnedEndRoom = true; //stops looping and constantly spawning end rooms
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room endRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(endRoom.X, endRoom.Z);
            Destroy(endRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Z == tempRoom.Z);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Z);
        }
    }
    public void LoadRoom(string name, int x, int z)
    {
        if (doesRoomExist(x, z))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Z = z;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!doesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Z))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                0,
                currentLoadRoomData.Z * room.Length
            );

            room.X = currentLoadRoomData.X;
            room.Z = currentLoadRoomData.Z;
            //shows the position of the rooms for debugging
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Z;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }
            loadedRooms.Add(room);
            //room.removeUselessDoors();
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;

        }

    }
    public bool doesRoomExist(int x, int z)
    {
        return loadedRooms.Find(item => item.X == x && item.Z == z) != null;
    }

    public Room FindRoom(int x, int z)
    {
        return loadedRooms.Find(item => item.X == x && item.Z == z);
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;
    }
}
