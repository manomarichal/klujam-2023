using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust this to control the movement speed
    public float pushForce = 10f;  // Adjust this to control the force applied when pushing
    public Vector3 pushDirection = Vector3.forward;
    private bool _canMove = true;
    private void Update()
    {
        if(!_canMove) return;
        float horizontalInput = Input.GetAxis("Horizontal");  // Get input from the horizontal axis

        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed;  // Create a movement vector based on input and speed

        transform.position += movement * Time.deltaTime;  // Move the character by modifying its position directly
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(tag: $"GameBert")) return;
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        // Check if the colliding object has a Rigidbody component
        if (otherRigidbody != null)
        { 
           

            // Apply the pushing force to the object
            otherRigidbody.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
            _canMove = false;
        }
    }
}