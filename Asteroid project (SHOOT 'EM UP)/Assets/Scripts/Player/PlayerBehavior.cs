using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private InputAction moveAction;
    private Vector2 halfExtents;

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
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        newPos = GameManager.Instance.ClampToBounds(newPos, halfExtents);
        rb.MovePosition(newPos);
    }
}
