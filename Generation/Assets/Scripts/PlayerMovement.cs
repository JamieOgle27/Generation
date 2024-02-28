using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int moveSpeed = 8; //This has to be consistent between every gameobject with this script attached!!!

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * -moveSpeed, Space.World);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * -moveSpeed, Space.World);
    }
}
