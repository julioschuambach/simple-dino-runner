using UnityEngine;

public class Obstacle : MoveObject
{
    [SerializeField] private GameObject[] obstaclesPrefab;
    private GameObject instantiatePrefab
    {
        get
        {
            return obstaclesPrefab[Random.Range(0, obstaclesPrefab.Length)];
        }
    }

    protected override GameObject GetInstantiatePrefab()
    {
        return instantiatePrefab;
    }
}
