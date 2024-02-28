using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileArray : MonoBehaviour
{
    public GameObject[] tiles;


    public GameObject GetRandomTile()
    {
        //Should Only be using this if we're not inputing a seed, will be different every time. 
        int randomNumber = Random.Range(0,tiles.Length);
        return tiles[randomNumber];
    }

    public GameObject GetRandomTile(int seed)
    {

        int randomNumber = seed % tiles.Length;
        return tiles[randomNumber];
    }


}
