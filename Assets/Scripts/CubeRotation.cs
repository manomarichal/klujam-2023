using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 5f; // Adjust this to control the rotation speed
    public float autoRotationSpeed = 25f; // Adjust this to control the rotation speed

    private Vector3 previousMousePosition;
    private bool isRotating;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartRotation();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopRotation();
        }

        if (isRotating)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - previousMousePosition;

            // Rotate the cube based on mouse movement
            RotateCube(-mouseDelta.y, mouseDelta.x);
        }
        else
        {
            transform.Rotate(Vector3.up, rotationSpeed * 10 * Time.deltaTime);
            transform.Rotate(Vector3.right, autoRotationSpeed * Time.deltaTime);
        }

        previousMousePosition = Input.mousePosition;
    }

    private void StartRotation()
    {
        isRotating = true;
    }

    private void StopRotation()
    {
        isRotating = false;
    }

    private void RotateCube(float mouseY, float mouseX)
    {
        // Rotate the cube based on mouse movement
        transform.Rotate(mouseY * rotationSpeed, mouseX * rotationSpeed, 0f, Space.World);
    }
}