using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [Header("LaserProperities")]
    [SerializeField] private float speed = 15f; // Скорость движения
    [SerializeField] private float maxLifetime = 2f; // Максимальное время жизни

    private Rigidbody2D rb; // Переменная для хранения физического тела
    private float lifeTime; // Время жизни лазера

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Проверяем время жизни лазера, на всякий случай, если улетел за пределы экрана и не уничтожился
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifetime)
        {
            Destroy(gameObject);
        }   
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
