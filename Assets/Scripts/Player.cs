using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90; // Example adjustment
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetKey(control.characterMoveBack()))
        {
            // Calculate the backward direction based on the character's current forward vector
            // Multiplying by -1 reverses the direction
            Vector3 backwardDirection = -transform.up;

            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            transform.Translate(backwardDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(control.characterMoveForward()))
        {
            // Calculate the backward direction based on the character's current forward vector
            // Multiplying by -1 reverses the direction
            Vector3 forwardDirection = transform.up;

            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            transform.Translate(forwardDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(control.characterMoveLeft()))
        {
            // Calculate the backward direction based on the character's current forward vector
            // Multiplying by -1 reverses the direction
            Vector3 leftDirection = -transform.right;

            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            transform.Translate(leftDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(control.characterMoveRight()))
        {
            // Calculate the backward direction based on the character's current forward vector
            // Multiplying by -1 reverses the direction
            Vector3 rightDirection = transform.right;

            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            transform.Translate(rightDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
