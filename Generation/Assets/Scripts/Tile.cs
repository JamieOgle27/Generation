using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    //private Renderer tileRenderer;#
    public GameObject gameObjectToSpawn;
    public TileArray[] tileArrays;
    public TileArray tileArray;
    public bool layersSpawnSameTile; //If True layers will spawn the same tile, otherwise the tile will be completely random. 

    public int layerIndex;
    private Vector3 instantiatePosition;
    //public int tileIndex;

    private void Awake()
    {
        layerIndex = Mathf.Max(Mathf.RoundToInt(Mathf.Abs(this.transform.position.x)), Mathf.RoundToInt(Mathf.Abs(this.transform.position.z)));
    }

    private void Start()
    {
        SpawnTile();
    }

    private void SpawnTile()
    {
        float xOffset = Mathf.Round(WorldGeneration.Instance.transform.position.x) - WorldGeneration.Instance.transform.position.x;
        float zOffset = Mathf.Round(WorldGeneration.Instance.transform.position.z) - WorldGeneration.Instance.transform.position.z;
        instantiatePosition = new Vector3(this.transform.position.x - xOffset, this.transform.position.y, this.transform.position.z - zOffset);
        System.Random rng = GetRandomMethod(); //Are tiles random by position or by layer (or more down the line)
        var rand1 = rng.Next();
        var rand2 = rng.Next();
        var rand3 = rng.Next();
        tileArray = GetTileArray(rand1);
        gameObjectToSpawn = tileArray.GetRandomTile(rand2);
        Quaternion spawnRotation = GetRandomRotation(rand3);

        Instantiate(gameObjectToSpawn, instantiatePosition, spawnRotation, this.transform);
        //Debug.Log(instantiatePosition);
    }
    private System.Random GetRandomMethod()
    {
        if (layersSpawnSameTile) //Random system uses layerIndex with a seed to keep tiles in the same layer consistent every time the game is run
        {
            System.Random rng = new System.Random(layerIndex * WorldGeneration.Instance.seedValue);
            return rng;

        }
        else if (!layersSpawnSameTile) //Random system uses position with a seed  to keep tiles in the same position consistent every time the game is run
        {
            float xConsistentPosition = this.transform.position.x - Mathf.RoundToInt(WorldGeneration.Instance.transform.position.x);
            float zConsistentPosition = this.transform.position.z - Mathf.RoundToInt(WorldGeneration.Instance.transform.position.z);
            Vector3 consistentPosition = new Vector3(xConsistentPosition, 0, zConsistentPosition);
            System.Random rng = new System.Random((consistentPosition).GetHashCode() + WorldGeneration.Instance.seedValue);
            Debug.Log(this.transform.position);
            return rng;
        }
        Debug.Log("ERROR: private System.Random GetRandomMethod() failed to dectet the random method, an unseeded System.Random was used instead");
        return new System.Random();
    }
    public Quaternion GetRandomRotation(int seed)
    {
        int rotationOffset = seed % 4 * 90; // 0*90 - 0, 1*90 - 90, 2*90 - 180, 3*90 - 270
        return Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y + rotationOffset, this.transform.rotation.z);
    }
    TileArray GetTileArray(int seed)
    {
        TileArray spawn = tileArrays[seed % tileArrays.Length];
        return spawn;
    }
}
