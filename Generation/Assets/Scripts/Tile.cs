using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    //private Renderer tileRenderer;#
    public GameObject gameObjectToSpawn;
    public TileArray tileArray;

    private void Start()
    {
        gameObjectToSpawn = tileArray.GetRandomTile();
        Instantiate(gameObjectToSpawn, transform.position, transform.rotation);
        
    }

}
