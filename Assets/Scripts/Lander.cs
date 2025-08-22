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

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Lander has crashed.");
            return;
        }

        float softLandingVelocityMagnitude = 2f;
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Lander has landed too hard.");
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = .90f;
        if (dotVector < minDotVector)
        {
            Debug.Log("Lander has landed at a too steep angle.");
            return;
        }

        Debug.Log("Lander has landed safely.");
    }
}
