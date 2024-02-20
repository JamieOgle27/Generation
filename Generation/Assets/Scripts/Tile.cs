using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    //private Renderer tileRenderer;#
    public GameObject gameObjectToSpawn;
    public TileArray tileArray;
    public bool layersSpawnSameTile; //If True layers will spawn the same tile, otherwise the tile will be completely random. 

    public int layerIndex;
    //public int tileIndex;

    private void Awake()
    {
        layerIndex = Mathf.Max(Mathf.RoundToInt(Mathf.Abs(this.transform.position.x)), Mathf.RoundToInt(Mathf.Abs(this.transform.position.z)));
    }

    private void Start()
    {
        if (layersSpawnSameTile)
        {
            var rng = new System.Random(layerIndex + WorldGeneration.Instance.seedValue);
            var rand1 = rng.Next();

            gameObjectToSpawn = tileArray.GetRandomTile(layerIndex);
            Instantiate(gameObjectToSpawn, this.transform);
        }
        else if (!layersSpawnSameTile)
        {
            var rng = new System.Random(this.transform.position.GetHashCode() + WorldGeneration.Instance.seedValue);
            var rand1 = rng.Next();

            gameObjectToSpawn = tileArray.GetRandomTile(rand1);
            Instantiate(gameObjectToSpawn, this.transform);
        }
        
    }

}
