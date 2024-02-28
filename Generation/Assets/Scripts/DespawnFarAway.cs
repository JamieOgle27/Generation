using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnFarAway : MonoBehaviour
{
    private Tile tile;
    private GameObject child;

    private void Start()
    {
        tile = GetComponent<Tile>();
        child = this.gameObject.transform.GetChild(0).gameObject;

    }
    private void LateUpdate()
    {
        CheckDistance(tile.consistentPosition);
    }
    // Update is called once per frame
    public void CheckDistance(Vector3 consistentPositon)
    {
        //Debug.Log(Vector3.Distance(WorldGeneration.Instance.player.transform.position, child.transform.position);

        if (Vector3.Distance(WorldGeneration.Instance.player.transform.position, child.transform.position ) > 40)
        {
            //Debug.Log("destroy");
            WorldGeneration.Instance.RemoveTileLiveInScene(consistentPositon);
            Destroy(gameObject);
        }
    }
}
