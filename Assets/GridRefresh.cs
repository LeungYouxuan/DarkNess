using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridRefresh : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap tilemap;
    void Start()
    {
        tilemap.RefreshAllTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
