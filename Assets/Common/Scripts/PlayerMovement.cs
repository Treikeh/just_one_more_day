using UnityEngine;

public class PlayerMovement : MonoBehaviour, ISave
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    // Is moved in front of the player to hit LevelLoadTriggers.
    // This way i don't have to do a bunch of fancy vector math to set the palyers position outside of a trigger when loading the players position
    [SerializeField] private Transform levelLoadCollider;
    private Vector2 velocity;
    private Rigidbody2D rb;


    private void OnEnable() { InputManager.Instance.OnMovePressed += MovePressed; }
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
        levelLoadCollider.position = transform.position + (new Vector3(rb.linearVelocityX, rb.linearVelocityY, 0f).normalized * 0.75f);
    }


    // SAVE DATA
    public void Save()
    {
        // Create save data
        SaveData saveData = new()
        {
            position = transform.position,
        };
        // Save data to level manager
        LevelManager.Instance.SetSaveData(Utils.GetSceneId(gameObject), saveData);
    }

    public void Load()
    {
        object obj = LevelManager.Instance.GetSaveData(Utils.GetSceneId(gameObject));
        if (obj is not null and SaveData)
        {
            SaveData saveData = (SaveData)obj;
            transform.position = saveData.position;
        }
    }

    private class SaveData
    {
        public Vector2 position;
    }
}
