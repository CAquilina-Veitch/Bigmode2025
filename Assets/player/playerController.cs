using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    PlayerControls playerControls;

    public PlayerInput Input;
    public Vector2 inputVector;
    public Vector3 moveVector;
    public float moveSpeed = 10;
    
    void Awake()
    {
        Input = GetComponent<PlayerInput>();
    }
    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }
    private void OnMove(InputValue InputValue)
    {
        inputVector = InputValue.Get<Vector2>();
        moveVector.x = inputVector.x;
        moveVector.y = inputVector.y;
        Debug.Log($"X move: {inputVector.x}");
        Debug.Log($"Y move: {inputVector.y}");
    }

    public void Move()
    {
        transform.position = transform.position + (moveVector * Time.deltaTime * moveSpeed);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }
}
