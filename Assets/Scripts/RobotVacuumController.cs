using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class RobotVacuumController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 300f; 

    [Header("Collision Settings")]
    private float backOffForce = 3f;

    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private bool isTurning = false;
    private Quaternion targetRotation;

    public Slider speedSlider; 
    public TextMeshProUGUI speedText;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        ChooseRandomDirection();
    }

    void FixedUpdate()
    {
        if (speedSlider != null)
        {
            moveSpeed = speedSlider.value;
            speedText.text = "Speed: " + moveSpeed.ToString("0.0");
        }

        if (speedSlider != null)
        {
            moveSpeed = speedSlider.value; 
        }

        if (isTurning)
        {
            rb.linearVelocity = Vector2.zero;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isTurning = false;
            }
        }
        else
        {
            rb.linearVelocity = moveDirection * moveSpeed;

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
                Quaternion desiredRotation = Quaternion.Euler(0, 0, angle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Va chạm với: " + collision.gameObject.name);

        if (collision.contacts.Length > 0)
        {
            Vector2 contactNormal = collision.contacts[0].normal;

            rb.AddForce(contactNormal * backOffForce, ForceMode2D.Impulse);

            Vector2 reflectedDir = Vector2.Reflect(moveDirection, contactNormal).normalized;

            moveDirection = reflectedDir;

            float targetAngle = Mathf.Atan2(reflectedDir.y, reflectedDir.x) * Mathf.Rad2Deg - 90f;
            targetRotation = Quaternion.Euler(0, 0, targetAngle);

            isTurning = true;
        }
    }

    public void ChooseRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        float rad = angle * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }
}
