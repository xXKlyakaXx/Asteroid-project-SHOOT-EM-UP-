using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [Header("LaserProperities")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float maxLifetime = 3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.up.normalized * speed * Time.fixedDeltaTime);           
    }

    private void OnBecameInvisible()
    {
        // Как только лазер вышел за пределы камеры - уничтожается
        Destroy(gameObject);
    }
}
