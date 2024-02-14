using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class RotateTile : MonoBehaviour
{
   private Tilemap tilemap; 
   private  Vector3Int tilePosition;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilePosition = GetComponent<Vector3Int>();

        RotateTileAtPosition(tilePosition, 90);
    }

    void RotateTileAtPosition(Vector3Int position, float angle)
    {
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, angle), Vector3.one);
        tilemap.SetTransformMatrix(position, matrix);
    }
}

