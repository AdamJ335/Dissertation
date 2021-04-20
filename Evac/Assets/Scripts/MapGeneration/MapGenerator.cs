using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapGenerationData mapGenerationData;
    private List<Vector2Int> mapRooms;

    private void Start()
    {
        mapRooms = MapCrawlerController.GenerateDungeon(mapGenerationData);
        SpawnRooms(mapRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
        }
    }
}
