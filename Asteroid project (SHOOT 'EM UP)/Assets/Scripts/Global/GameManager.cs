using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Manager")]
    public int PlayerLevel = 1;
         
    [Header("Play Area Bounds")]
    [Tooltip("Если включено - то границ считаются по камере. Иначе используем ручные значения")]
    [SerializeField] private bool useCameraBounds = true;
    [SerializeField] private float paddingX = 0.5f;
    [SerializeField] private float paddingY = 0.5f;

    [Header("Manual Bounds")]
    [Tooltip("Используем эти значения, если useCameraBound выключен")]
    [SerializeField] private float minX = -6f;
    [SerializeField] private float maxX = 6f;
    [SerializeField] private float minY = -4.5f;
    [SerializeField] private float maxY = 4.5f;

    private Camera cam;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        cam = Camera.main;
    }

    void Update()
    {
        
    }

    public Vector2 ClampToBounds(Vector2 pos, Vector2 halfExtents)
    {
        float BMinX, BMaxX, BMinY, BMaxY;

        if (useCameraBounds && cam != null)
        {
            Vector2 bl = cam.ViewportToWorldPoint(new Vector2(0f, 0f));
            Vector2 tr = cam.ViewportToWorldPoint(new Vector2(1f, 1f));

            BMinX = bl.x + paddingX + halfExtents.x;
            BMaxX = tr.x + paddingX - halfExtents.x;
            BMinY = bl.y + paddingY + halfExtents.y;
            BMaxY = tr.y + paddingY - halfExtents.y;
        }
        else
        {
            BMinX = minX; BMaxX = maxX;
            BMinY = minY; BMaxY = maxY;
        }

        pos.x = Mathf.Clamp(pos.x, BMinX, BMaxX);
        pos.y = Mathf.Clamp(pos.y, BMinY, BMaxY);

        return pos;
    }
}
