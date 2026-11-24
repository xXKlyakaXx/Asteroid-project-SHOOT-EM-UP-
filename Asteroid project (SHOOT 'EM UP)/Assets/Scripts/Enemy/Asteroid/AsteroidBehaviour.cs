using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [Header("AsteroidProperities")]
    [SerializeField] private float minSpeed = 1f, maxSpeed = 7f; // Минимальная и максимальная скорость движения
    [SerializeField] private float minRotation = 10f, maxRotation = 50f; // Минимальная и максимальная скорость вращения
    [SerializeField] private float minScale = 1f, maxScale = 5f; // Минимальный и максимальный размер
    [SerializeField] private int health = 10; // Количество жизней
    [SerializeField] private int scoreValue = 10; // Количество очков 
    [SerializeField] private float maxLifetime = 2f; // Максимальное время жизни

    private float speed; // Cкорость движения
    private float speedRotation; // Cкорость вращения
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
        speedRotation = Random.Range(minRotation, maxRotation);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.down * speed * Time.fixedDeltaTime);
        transform.Rotate(0f, 0f, speedRotation * Time.fixedDeltaTime);
    }

}
