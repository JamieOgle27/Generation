using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    //private Renderer tileRenderer;

    private void Start()
    {

        Color resultColor = Color.HSVToRGB(UnityEngine.Random.Range(0f, 1f), 1f, 1f);
        GetComponent<Renderer>().material.color = resultColor;  
        
    }

}
