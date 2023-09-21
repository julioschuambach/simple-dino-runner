using UnityEngine;

public class Obstacle : MoveObject
{
    private Player player;
    [SerializeField] private bool scored;
    [SerializeField] private int scoreValue;
    [SerializeField] private GameObject[] obstaclesPrefab;
    private GameObject instantiatePrefab
    {
        get
        {
            return obstaclesPrefab[Random.Range(0, obstaclesPrefab.Length)];
        }
    }

    protected override void Awake()
    {
        base.Awake();

        player = FindObjectOfType<Player>();
    }

    protected override GameObject GetInstantiatePrefab()
    {
        return instantiatePrefab;
    }

    protected override void Update()
    {
        base.Update();

        if (transform.position.x < player.transform.position.x && !scored)
            Score();
    }

    private void Score()
    {
        scored = true;
        GameManager.instance.Score(scoreValue);
    }
}
