using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 2.0f; // Adjust this to control the rotation speed
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector2 touchDelta;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchEndPos = touch.position;
                    touchDelta = touchEndPos - touchStartPos;

                    // Check if the touch is on the right side of the screen (adjust as needed)
                    if (touchStartPos.x >= Screen.width / 4 && touchStartPos.y >= Screen.height / 5)
                    {
                        // Rotate the camera based on the touch delta
                        //float rotationX = touchDelta.y * rotationSpeed * Time.deltaTime;
                        float rotationY = -touchDelta.x * rotationSpeed * Time.deltaTime;

                        // Apply the rotation to the camera
                        transform.Rotate(0, -rotationY, 0);
                    }

                    // Store the current touch position as the start position for the next frame
                    touchStartPos = touch.position;
                    break;
            }
        }
    }
}
