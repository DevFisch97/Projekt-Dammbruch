using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D  rb;
    private PlayerControlls playerController;
    private InputAction move;
    private Vector2 moveValue;

    void Awake()
    {
        playerController = new PlayerControlls();
    }

    void Start()
    {
        move = playerController.Player.Move;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {   
        rb.position = rb.position+(moveValue*moveSpeed*Time.deltaTime);
    }

    private void OnMove(InputValue inputValue)
    {
        moveValue = inputValue.Get<Vector2>();
    }

    private void OnEnable()
    {
        move = playerController.Player.Move;
        move.Enable();
    }

    private void Osable()
    {
        move.Disable();
    }
}
  
