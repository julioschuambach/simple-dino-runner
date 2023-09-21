using UnityEngine;

public class Ground : MoveObject
{
    private SpriteRenderer spriteRenderer;
    private float instantiatePositionX
    {
        get
        {
            return transform.position.x + spriteRenderer.size.x;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rigidbody.velocity = movementDirection;
    }

    protected override Vector3 GetInstantiatePosition()
        => new(instantiatePositionX, transform.position.y, transform.position.z);
}
