using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
};
public class MapCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}
    };

    public static List<Vector2Int> GenerateDungeon(MapGenerationData mapData)
    {
        List<MapCrawler> mapCrawlers = new List<MapCrawler>();

        for (int i = 0; i < mapData.numberOfCrawlers; i++)
        {
            mapCrawlers.Add(new MapCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(mapData.iterationMin, mapData.iterationMax);

        for (int i = 0; i < iterations; i++)
        {
            foreach (MapCrawler mapCrawler in mapCrawlers)
            {
                Vector2Int newPos = mapCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }
        return positionsVisited;
    }
}
