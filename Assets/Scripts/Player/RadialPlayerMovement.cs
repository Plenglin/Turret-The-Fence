using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialPlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;            // The speed that the player will move at.
    public float sprintSpeed = 12f;            // The speed that the player will move at.

    public float staminaConsumptionRate = 0.2f;
    public float staminaRegenRate = 0.1f;
    public float currentStamina = 1f;
    public Slider staminaSlider;

    public RadialCameraFollow radialCamera;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
#if !MOBILE_INPUT
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
#endif

    void Awake()
    {
#if !MOBILE_INPUT
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
#endif

        // Set up references.
        anim = GetComponent<Animator>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

        staminaSlider.maxValue = 1;
        staminaSlider.minValue = 0;
    }


    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        staminaSlider.value = currentStamina;

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        Animating(h, v);
    }

    private bool sprinting = false;


    void Move(float h, float v)
    {
        Vector3 offset = (radialCamera.center.position - this.transform.position).normalized;
        offset.y = 0;
        Vector3 horizontal = Vector3.Cross(offset, Vector3.up);
        Vector3 change = -v * offset + h * horizontal;
        // Set the movement vector based on the axis input.
        movement.Set(change.x, 0f, change.z);

        if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina < 0) {
            sprinting = false;
        } else if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamina > 0) {
            sprinting = true;
        }
        
        if (sprinting) {
            currentStamina -= staminaConsumptionRate * Time.deltaTime;
        } else if (currentStamina < 1f) {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        float speed = sprinting ? sprintSpeed : walkSpeed;
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }


    void Turning()
    {
#if !MOBILE_INPUT
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotatation);
        }
#else

            Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
#endif
    }


    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
