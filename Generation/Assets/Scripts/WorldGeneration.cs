using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject groundPlane;
    public int layersToGenerate;
    private int width;
    private int height;
    private int[,] gridArray;

    [SerializeField]
    public int seedValue;


    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(seedValue);
        Generate(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generate(new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), Mathf.RoundToInt(this.transform.position.z))); //We want this to be the location of the player so tiles can be generated as they move
            //For now this is on space, eventually it will just be in update so we don't crash anything
        }
    }

    //This is the main function that will be used to do the generation - called on Start atm, but later we can put this on a button so players can fill out settings, etc first. 
    void Generate(Vector3 origin)
    {
        //ToDo: Stop tiles from spawning where they already exist

        for (int i = 0; i < layersToGenerate; i++)
        {
            width = i; height = i;
            gridArray = new int[i, i];
            Debug.Log("Layer: " + i);

            for (int x = -width; x <= gridArray.GetLength(0); x++)
            {
                for (int y = -height; y <= gridArray.GetLength(1); y++)
                {
                    if (Mathf.Abs(x) == width || Mathf.Abs(y) == height)
                    {
                        Instantiate(groundPlane, new Vector3(x, 0, y) + origin, groundPlane.transform.rotation);
                        //Debug.Log(x + " , " + y);
                    }
                }
            }
        }

    }

}
