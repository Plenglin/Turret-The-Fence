using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelativePlayerMovement : MonoBehaviour {
    public bool hindered = false;
    public const float hinderedSpeed = 2f;            
    public const float walkSpeed = 6f;            // The speed that the player will move at.
    public const float sprintSpeed = 12f;            // The speed that the player will move at.

    public float staminaConsumptionRate = 0.2f;
    public float staminaRegenRate = 0.1f;
    public float currentStamina = 1f;
    public Slider staminaSlider;

    public Camera targetCamera;

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
        //Turning();

        // Animate the player.
        //Animating(h, v);
    }


    void Move(float h, float v)
    {
        Vector3 yAxis = targetCamera.transform.forward;
        yAxis.y = 0;
        yAxis.Normalize();
        Vector3 xAxis = Vector3.Cross(yAxis, Vector3.up);
        xAxis.Normalize();
        Vector3 delta = (v * yAxis - h * xAxis).normalized;

        float speed;
        bool attemptingSprint = Input.GetKey(KeyCode.LeftShift);
        bool sprinting = attemptingSprint && currentStamina > 0;
        if (!attemptingSprint) {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }
        if (hindered) {  // Hindered
            speed = hinderedSpeed;
        } else if (sprinting) {  // Sprinting
            speed = sprintSpeed;
            currentStamina -= staminaConsumptionRate * Time.deltaTime;
        } else {
            speed = walkSpeed;  // Walking
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, 1);
        staminaSlider.value = currentStamina;
        
        // Normalise the movement vector and make it proportional to the speed per second.
        Vector3 vel = delta.normalized * speed;

        vel.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = vel;
        Debug.Log(playerRigidbody.velocity);
    }
}
