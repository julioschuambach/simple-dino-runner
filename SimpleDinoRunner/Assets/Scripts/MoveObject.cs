using UnityEngine;

public abstract class MoveObject : MonoBehaviour
{
    protected Rigidbody2D rigidbody;
    protected bool instantiated;
    [SerializeField] protected Vector3 movementDirection;
    [SerializeField] protected Vector3 instantiatePosition;
    [SerializeField] protected GameObject instantiatePrefab;
    [SerializeField] protected float destroyPosition;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody.velocity = movementDirection;
    }

    protected void InstantiateNewPrefab(GameObject prefab, Vector3 prefabPosition, Quaternion prefabRotation)
    {
        instantiated = true;
        Instantiate(prefab, prefabPosition, prefabRotation);
    }

    private void Update()
    {
        if (transform.position.x < 0.0f && !instantiated)
            InstantiateNewPrefab(instantiatePrefab, GetInstantiatePosition(), transform.rotation);

        if (transform.position.x < destroyPosition)
            Destroy(gameObject);
    }

    protected virtual Vector3 GetInstantiatePosition()
        => instantiatePosition;
}
