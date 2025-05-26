using UnityEngine;

public class PlayerMovement : MonoBehaviour, ISave
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private Vector2 velocity;
    private Rigidbody2D rb;


    private void OnEnable() { InputManager.Instance.OnMovePressed += MovePressed; velocity = Vector2.zero; }
    private void OnDisable() { InputManager.Instance.OnMovePressed -= MovePressed; Save(); }


    private void Start()
    {
        // Get component references
        rb = GetComponent<Rigidbody2D>();
        InputManager.Instance.ChangeActionMap("Player");
        Load();
    }


    private void MovePressed(Vector2 moveDir)
    {
        velocity = moveDir * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = velocity;
    }

    public void SetMovementSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }


    // SAVE LEVEL DATA
    public void Save()
    {
        // Create save data
        LevelSaveData saveData = new()
        {
            position = LevelManager.Instance.playerSpawnPosition != Vector2.zero ? LevelManager.Instance.playerSpawnPosition: transform.position,
        };
        // Save data to level manager
        LevelManager.Instance.SetSaveData(Utils.GetSceneId(gameObject), saveData);
    }

    public void Load()
    {
        LevelSaveData saveData = LevelManager.Instance.GetSaveData(Utils.GetSceneId(gameObject));
        if (saveData != null)
        {
            transform.position = saveData.position;
        }
    }
}
