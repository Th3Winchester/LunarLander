using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D LanderRigidBody2D;

    private void Awake()
    {
        LanderRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            float force = 700f;
            LanderRigidBody2D.AddForce(force * transform.up * Time.deltaTime);
        }

        if (Keyboard.current.aKey.isPressed)
        {
            float turnSpeed = +100f;
            LanderRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            float turnSpeed = -100f;
            LanderRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
        }
    }
}
