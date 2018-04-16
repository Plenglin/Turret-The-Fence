using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelativePlayerMovement : MonoBehaviour {
    public bool hindered = false;
    public const float hinderedSpeed = 2f;            
    public const float walkSpeed = 6f;            // The speed that the player will move at.
    public const float sprintSpeed = 12f;            // The speed that the player will move at.

    public float jumpSpeed = 10f;
    public float staminaConsumptionRate = 0.2f;
    public float staminaRegenRate = 0.1f;
    public float currentStamina = 1f;
    public Slider staminaSlider;

    public Camera targetCamera;

    private MovementState state, lastState;

    private Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

    void Awake() {
        // Set up references.
        playerRigidbody = GetComponent<Rigidbody>();

        staminaSlider.maxValue = 1;
        staminaSlider.minValue = 0;
    }


    void FixedUpdate() {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        staminaSlider.value = currentStamina;

        // Move the player around the scene.
        Move(h, v);
        state = Walking.inst;

        // Turn the player to face the mouse cursor.
        //Turning();

        // Animate the player.
        //Animating(h, v);
    }

    public bool IsGrounded() {
        RaycastHit hit;
        Vector3 down = -transform.up;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.05f, 0f), down, out hit, 0.1f)) {
            return Vector3.Angle(-down, hit.normal) < 45f;
        }
        return false;
    }

    void Move(float h, float v) {

        if (!IsGrounded()) {
            state = Airborne.inst;
        } else if (hindered) {
            state = Hindered.inst;
        } else {
            state = Walking.inst;
        }

        if (state.CanAffectMovement(this)) {
            Vector3 yAxis = targetCamera.transform.forward;
            yAxis.y = 0;
            yAxis.Normalize();
            Vector3 xAxis = Vector3.Cross(yAxis, Vector3.up);
            xAxis.Normalize();
            Vector3 delta = (v * yAxis - h * xAxis).normalized;
       
            bool attemptingSprint = Input.GetKey(KeyCode.LeftShift);
            bool sprinting = attemptingSprint && currentStamina > 0;
            if (!attemptingSprint) {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }

            float speed;
            if (sprinting) {
                speed = state.GetSprintSpeed(this);
                currentStamina -= staminaConsumptionRate * Time.deltaTime;
            } else {
                speed = state.GetMovementSpeed(this);
            }

            currentStamina = Mathf.Clamp(currentStamina, 0, 1);
            staminaSlider.value = currentStamina;
        
            // Normalise the movement vector and make it proportional to the speed per second.
            Vector3 vel = delta.normalized * speed;
            /*RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit)) {
                hit.
            }*/

            vel.y = playerRigidbody.velocity.y;
            playerRigidbody.velocity = vel;
        }
        if (state.CanJump() && Input.GetKeyDown(KeyCode.Space)) {
            playerRigidbody.velocity += new Vector3(0f, jumpSpeed, 0f);
        }

        //Debug.Log(playerRigidbody.velocity);
    }

    private interface MovementState {
        float GetMovementSpeed(RelativePlayerMovement pm);
        float GetSprintSpeed(RelativePlayerMovement pm);
        bool CanJump();
        void Loop(RelativePlayerMovement pm);
        bool CanAffectMovement(RelativePlayerMovement pm);
    }

    class Walking : MovementState {

        public static readonly Walking inst = new Walking();

        public bool CanJump() {
            return true;
        }

        public bool CanAffectMovement(RelativePlayerMovement pm) {
            return true;
        }

        public float GetMovementSpeed(RelativePlayerMovement pm) {
            return walkSpeed;
        }

        public float GetSprintSpeed(RelativePlayerMovement pm) {
            return sprintSpeed;
        }

        public void Loop(RelativePlayerMovement pm) {
            
        }
    }

    class Airborne : MovementState {

        public static readonly Airborne inst = new Airborne();

        public bool CanAffectMovement(RelativePlayerMovement pm) {
            return false;
        }

        public bool CanJump() {
            return false;
        }

        public float GetMovementSpeed(RelativePlayerMovement pm) {
            return 0f;
        }

        public float GetSprintSpeed(RelativePlayerMovement pm) {
            return 0f;
        }

        public void Loop(RelativePlayerMovement pm) {
            
        }
    }

    class Hindered : MovementState {

        public static readonly Hindered inst = new Hindered();

        public bool CanAffectMovement(RelativePlayerMovement pm) {
            return true;
        }

        public bool CanJump() {
            return false;
        }

        public float GetMovementSpeed(RelativePlayerMovement pm) {
            return hinderedSpeed;
        }

        public float GetSprintSpeed(RelativePlayerMovement pm) {
            return hinderedSpeed;
        }

        public void Loop(RelativePlayerMovement pm) {

        }
    }
}
