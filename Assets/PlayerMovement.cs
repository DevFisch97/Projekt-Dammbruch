using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D  rb;
    private PlayerControlls playerController;
    private InputAction move;
    private Vector2 moveValue;
    public Animator animator;
    private float coins = 1;
    public Camera mainCamera;
    public Vector2 detectionBoxSize = new Vector2(0.2f, 0.2f);
    public float rotationAngle = 0;
    private GameObject currentTriggerObject;

    void Awake()
    {
        playerController = new PlayerControlls();
    }

    void Start()
    {
        move = playerController.Player.Move;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetFloat("Horizontal", moveValue.x);
        animator.SetFloat("Vertikal", moveValue.y);
        animator.SetFloat("Speed", moveValue.sqrMagnitude);
    }

    void FixedUpdate()
    {   
        rb.position = rb.position+(moveValue*moveSpeed*Time.deltaTime);
    }

    private void OnMove(InputValue inputValue)
    {
        moveValue = inputValue.Get<Vector2>();
    }

    private void OnMouse()
    {
        Debug.Log("Shoot");
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 0f;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        Collider2D detectedCollider = Physics2D.OverlapBox(mouseWorldPosition, detectionBoxSize, rotationAngle);
        Debug.DrawRay(mousePosition, Vector2.up * 0.1f, Color.red, 1f);

        if (detectedCollider != null && detectedCollider.CompareTag("Finish"))
        {
            GameObject clickedObject = detectedCollider.gameObject;

            if(clickedObject == currentTriggerObject)
            {
                coins++;
                Debug.Log(coins);
            }
        }
        else
        {
            Debug.Log("Kein Collider erkannt.");
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         currentTriggerObject = other.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         currentTriggerObject = null;
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
  
