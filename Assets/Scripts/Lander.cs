using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Lander : MonoBehaviour
{
    public static Lander Instance { get; private set; }

    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs : EventArgs
    {
        public int score;
    }

    private Rigidbody2D landerRigidBody2D;
    private float fuelAmount = 10f;

    private void Awake()
    {
        Instance = this;
        landerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        //Debug.Log("Fuel Amount: " + fuelAmount);
        if (fuelAmount <= 0)
        {
            // No fuel
            return;
        }

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.dKey.isPressed)
        {
            ConsumeFuel();
        }

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
            OnLeftForce?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            float turnSpeed = -100f;
            landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
            OnRightForce?.Invoke(this, EventArgs.Empty);
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
        OnLanded?.Invoke(this, new OnLandedEventArgs 
        {
            score = score 
        });
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out FuelPickup fuelPickup))
        {
            Debug.Log("Picked up fuel.");
            float addFuelAmount = 10f;
            fuelAmount += addFuelAmount;
            fuelPickup.DestroyObject();
        }

        if (collider2D.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            Debug.Log("Picked up coin.");
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroyObject();
        }
    }

    private void ConsumeFuel()
    {
        float fuelConsumption = 1f;
        fuelAmount -= fuelConsumption * Time.deltaTime;
    }
}
