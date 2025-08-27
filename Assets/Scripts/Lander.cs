using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Lander : MonoBehaviour
{
    public event EventHandler OnUpForce;
    public event EventHandler OnBeforeForce;

    private Rigidbody2D landerRigidBody2D;

    private void Awake()
    {
        landerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        
        if (Keyboard.current.wKey.isPressed)
        {
            float force = 700f;
            landerRigidBody2D.AddForce(force * transform.up * Time.deltaTime);
            OnUpForce?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.aKey.isPressed)
        {
            float turnSpeed = +100f;
            landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            float turnSpeed = -100f;
            landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
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

        float maxScoreAmountLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;

        float maxScoreAmountLandingSpeed = 100;
        float landingSpeedScore = (softLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;

        Debug.Log("Landing Angle Score: " + landingAngleScore);
        Debug.Log("Landing Speed Score: " + landingSpeedScore);

        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier());

        Debug.Log("Total Score: " + score);
    }
}
