using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Ammo")]
    [SerializeField] private GameObject laserPrefab; // Префаб лазера
    [SerializeField] private Transform ammoSpawn1;
    [SerializeField] private Transform ammoSpawn2;
    [SerializeField] private Transform ammoSpawn3;
    [SerializeField] private float fireIntervalLaser = 10f; // Скорость выстрелов лазером

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

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        moveInput = moveInput.normalized;

        if (nextFireTime >= fireIntervalLaser)
        {
            FireLaser();
            nextFireTime -= fireIntervalLaser;
        }

        nextFireTime += Time.deltaTime;

    }

    void FixedUpdate()
    {
        // Движение корабля игрока при нажатии на кнопки
        Vector2 newPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime; // Расчет новой позиции
        newPos = GameManager.Instance.ClampToBounds(newPos, halfExtents); // Проверяем, что не вышли за границы
        rb.MovePosition(newPos); // Двигаемся согласно новой позиции

        // Стрельба по достижению временного интервала выстрела 
       /*if (Time.time >= nextFireTime)
        {
            FireLaser(); // Выстрел
            nextFireTime = Time.time + fireIntervalLaser;
        }*/
    }

    // Метод который создаёт лазеры на сцене в spawn'ах
    private void FireLaser()        
    {
        // Берём каждую точку спавна и создаём в ней лазер
        /*foreach (var spawn in playerAmmoSpawn)
        {
            GameObject laser = Instantiate(laserPrefab, spawn.position, spawn.rotation);
        }*/
        if (GameManager.Instance.PlayerLevel <= 1)
        {
            CreateLaser(ammoSpawn1);
        }else if(GameManager.Instance.PlayerLevel == 2){
            CreateLaser(ammoSpawn2); 
            CreateLaser(ammoSpawn3);
        }
        else if (GameManager.Instance.PlayerLevel == 3)
        {
            CreateLaser(ammoSpawn1);
            CreateLaser(ammoSpawn2);
            CreateLaser(ammoSpawn3);
        }
    }

    private void CreateLaser(Transform ammoSpawn)
    {
        GameObject laser = Instantiate(laserPrefab, ammoSpawn.position, ammoSpawn.rotation);
    }
}
