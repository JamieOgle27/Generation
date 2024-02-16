using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileArray : MonoBehaviour
{
    public GameObject[] tiles;

    public GameObject GetRandomTile()
    {
        int randomNumber = Random.Range(0,tiles.Length);
        return tiles[randomNumber];
    }
}
