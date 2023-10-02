using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public VariableJoystick variableJoystick;
    public float groundMaxDistance = 0.1f;
    public float dodgeDistance = 2f; // Adjust the dodge distance as needed
    public float dodgeDuration = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isDodging = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 moveDirection = transform.forward * variableJoystick.Vertical + transform.right * variableJoystick.Horizontal;
        moveDirection.Normalize();

        // Move the player using physics
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    public void PlayerJump()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundMaxDistance);
        // Player jump
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void LeftDodge()
    {
        if (!isDodging)
        {
            isDodging = true;
            // Calculate the dodge direction based on the player's local left
            Vector3 localLeft = -transform.right;
            Vector3 dodgeDirection = Quaternion.Euler(0f, -transform.eulerAngles.y, 0f) * localLeft;

            // Start the dodge coroutine
            StartCoroutine(PerformDodge(dodgeDirection));
        }
    }

    public void RightDodge()
    {
        if (!isDodging)
        {
            isDodging = true;
            // Calculate the dodge direction based on the player's local left
            Vector3 localLeft = transform.right;
            Vector3 dodgeDirection = Quaternion.Euler(0f, -transform.eulerAngles.y, 0f) * localLeft;

            // Start the dodge coroutine
            StartCoroutine(PerformDodge(dodgeDirection));
        }
    }

    private Vector3 CalculateLocalDodgeDirection(Vector3 direction)
    {
        // Calculate the dodge direction relative to the player's rotation
        return transform.TransformDirection(direction);
    }

    private IEnumerator PerformDodge(Vector3 dodgeDirection)
    {
        float elapsedTime = 0f;
        Debug.Log("DodgeDirection: " + dodgeDirection);

        while (elapsedTime < dodgeDuration)
        {
            // Move the player in the local dodge direction
            transform.Translate(dodgeDirection * dodgeDistance * Time.deltaTime, Space.Self);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDodging = false;
    }
}
