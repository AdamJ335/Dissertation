using UnityEngine;

/*
Creates a crawler from start, (any direction), and will continue on from other rooms generate
amount of rooms made based off iteration min and iteration max
*/

[CreateAssetMenu(fileName = "MapGenerationData.asset", menuName = "MapGenerationData/Map Data")]
public class MapGenerationData : ScriptableObject
{
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
