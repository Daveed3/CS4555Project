using UnityEngine;
using System.Collections;

public class EnemySpawnLocation
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }

    public EnemySpawnLocation(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
