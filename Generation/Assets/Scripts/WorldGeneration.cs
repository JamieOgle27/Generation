using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGeneration : MonoBehaviour
{
    public GameObject player;
    public static WorldGeneration Instance;
    public GameObject groundPlane;
    public GameObject[] groundPlanes;
    public int layersToGenerate;
    private int width;
    private int height;
    private int[,] gridArray;

    [SerializeField]
    public int seedValue;
    public bool randomSeed;
    public bool spawnSameTileInEachlayer;

    public List<Vector3> tilesLiveInScene;
    public int tilesLiveInSceneCount;

    private float travelSinceSpawnDistance;
    public float checkForGenerateDistance;
    private Vector3 lastGeneratePosition;

    public GameObject[] biomesNear;

    private void Awake()
    {
        lastGeneratePosition = transform.position;

        if (randomSeed)
        {
            seedValue = UnityEngine.Random.Range(-2147483647, 2147483647);
        }
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.Random.InitState(seedValue);
        Generate(new Vector3(Mathf.RoundToInt(player.transform.position.x), 0, Mathf.RoundToInt(player.transform.position.z)), layersToGenerate); //We want this to be the location of the player so tiles can be generated as they move

    }

    // Update is called once per frame
    void Update()
    {
        if(travelSinceSpawnDistance > checkForGenerateDistance)
        {
            Generate(new Vector3(Mathf.RoundToInt(player.transform.position.x), 0, Mathf.RoundToInt(player.transform.position.z)), layersToGenerate); //We want this to be the location of the player so tiles can be generated as they move
            lastGeneratePosition = transform.position;
        }

        travelSinceSpawnDistance = Vector3.Distance(transform.position, lastGeneratePosition);

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
        Generate(new Vector3(Mathf.RoundToInt(player.transform.position.x), 0, Mathf.RoundToInt(player.transform.position.z)), layersToGenerate); //We want this to be the location of the player so tiles can be generated as they move
            //For now this is on space, eventually it will just be in update so we don't crash anything
        }
        */
    }

    //This is the main function that will be used to do the generation - called on Start atm, but later we can put this on a button so players can fill out settings, etc first. 
    void Generate(Vector3 origin, int layers)
    {
        //ToDo: Stop tiles from spawning where they already exist
        CheckForBiomes();

        for (int i = 0; i < layers; i++)
        {
            width = i; height = i;
            gridArray = new int[i, i];
            //Debug.Log("Layer: " + i);

            for (int x = -width; x <= gridArray.GetLength(0); x++)
            {
                for (int y = -height; y <= gridArray.GetLength(1); y++)
                {
                    if (Mathf.Abs(x) == width || Mathf.Abs(y) == height)
                    {
                        Vector3 spawnPos = new Vector3(x, 0, y) + origin;
                        groundPlane = GetBiomeForTile(spawnPos);
                        GameObject spawn = Instantiate(groundPlane, spawnPos, groundPlane.transform.rotation);
                        spawn.GetComponent<Tile>().layersSpawnSameTile = spawnSameTileInEachlayer;
                        //Debug.Log(new Vector3(x, 0, y) + origin);
                        
                    }
                }
            }
        }

    }

    void GenerateOuterLayersOnly(Vector3 origin, int layers)
    {

    }

    public void AddTileLiveInScene(Vector3 tile)
    {
        tilesLiveInSceneCount++;
        tilesLiveInScene.Add(tile);
    }

    public void RemoveTileLiveInScene(Vector3 tile)
    {
        //Debug.Log("Tile Removed: " + tile);
        tilesLiveInSceneCount--;
        tilesLiveInScene.Remove(tile);
    }

    private void CheckForBiomes()
    {
        biomesNear = GameObject.FindGameObjectsWithTag("Biome");
    }

    private GameObject GetBiomeForTile(Vector3 spawnPos)
    {
        
        GameObject spawnTile = null;
        float lowestDist = Mathf.Infinity;

        for(int i = 0; i < biomesNear.Length; i++)
        {
            float dist = Vector3.Distance(biomesNear[i].transform.position, spawnPos);
            if (dist < lowestDist)
            {
                lowestDist = dist;
                if (biomesNear[i].name == "FORREST")
                {
                    spawnTile = groundPlanes[1]; //biomesNear needs to be able to match up to the groundPlanes so not matter the order it works.
                }
                if (biomesNear[i].name == "CITY")
                {
                    spawnTile = groundPlanes[0]; //biomesNear needs to be able to match up to the groundPlanes so not matter the order it works.
                }

            }
        }
        Debug.Log(spawnPos);
        Debug.Log(spawnTile.name);
        return spawnTile;

    }
}
