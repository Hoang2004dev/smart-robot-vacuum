using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class RobotVacuumController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("UI (Optional)")]
    public Slider speedSlider;
    public TextMeshProUGUI speedText;

    private Rigidbody2D rb;
    private Vector2 inputDirection;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += ctx => inputDirection = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= ctx => inputDirection = Vector2.zero;
        inputActions.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void FixedUpdate()
    {
        // Điều chỉnh tốc độ từ slider
        if (speedSlider != null)
        {
            moveSpeed = speedSlider.value;
            if (speedText != null)
                speedText.text = "Speed: " + moveSpeed.ToString("0.0");
        }

        rb.linearVelocity = inputDirection * moveSpeed;

        // Xoay hướng robot theo vector di chuyển
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>().normalized;
    }
}
