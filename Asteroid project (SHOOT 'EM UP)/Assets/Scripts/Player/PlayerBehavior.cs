using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Propeties")]
    public int playerLevel = 1;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Ammo")]
    [SerializeField] private GameObject laserPrefab; // Префаб лазера
    [SerializeField] private Transform[] playerAmmoSpawn; // Массив точек спавна выстрелов
    [SerializeField] private float fireIntervalLaser = 0.25f; // Скорость выстрелов лазером

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private InputAction moveAction;
    private Vector2 halfExtents; // Половина спрайта объекта
    private float nextFireTime; // Следующий интервал выстела

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");

        var sr = GetComponent<SpriteRenderer>();
        halfExtents = sr.bounds.extents;
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        moveInput = moveInput.normalized;


        if(Time.time >= nextFireTime)
        {
            FireLaser();
            nextFireTime = Time.time + fireIntervalLaser;
        }
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        newPos = GameManager.Instance.ClampToBounds(newPos, halfExtents);
        rb.MovePosition(newPos);
    }

    private void FireLaser()        
    {
        foreach (var spawn in playerAmmoSpawn)
        {
            GameObject laser = Instantiate(laserPrefab, spawn.position, spawn.rotation);
        }
    }
}
