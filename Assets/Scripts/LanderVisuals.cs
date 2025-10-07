using UnityEngine;
using System;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem MiddleThrusterParticleSystem;
    [SerializeField] private ParticleSystem LeftThrusterParticleSystem;
    [SerializeField] private ParticleSystem RightThrusterParticleSystem;
    [SerializeField] private GameObject landerExplosionVFX;
        
    private Lander lander;          

    private void Awake()
    {
            lander = GetComponent<Lander>();

            lander.OnUpForce += Lander_OnUpForce;
            lander.OnBeforeForce += Lander_OnBeforeForce;
            lander.OnLeftForce += Lander_OnLeftForce;
            lander.OnRightForce += Lander_OnRightForce;

            SetEnableThrusterParticleSystem(MiddleThrusterParticleSystem, false);
            SetEnableThrusterParticleSystem(LeftThrusterParticleSystem, false);
            SetEnableThrusterParticleSystem(RightThrusterParticleSystem, false);
    }

    private void Start()
    {
        lander.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case Lander.LandingType.TooSteepAngle:
            case Lander.LandingType.TooFastLanding:
            case Lander.LandingType.WrongLandingArea:
                Instantiate(landerExplosionVFX, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                break;
        }
    }

    private void Lander_OnBeforeForce(object sender, EventArgs e)
    {
            SetEnableThrusterParticleSystem(MiddleThrusterParticleSystem, false);
            SetEnableThrusterParticleSystem(LeftThrusterParticleSystem, false);
            SetEnableThrusterParticleSystem(RightThrusterParticleSystem, false);
    }

    private void Lander_OnUpForce(object sender, EventArgs e)
    {
            SetEnableThrusterParticleSystem(MiddleThrusterParticleSystem, true);
            SetEnableThrusterParticleSystem(LeftThrusterParticleSystem, true);
            SetEnableThrusterParticleSystem(RightThrusterParticleSystem, true);
    }

    private void Lander_OnLeftForce(object sender, EventArgs e)
    {
            SetEnableThrusterParticleSystem(LeftThrusterParticleSystem, true);
    }

    private void Lander_OnRightForce(object sender, EventArgs e)
    {
            SetEnableThrusterParticleSystem(RightThrusterParticleSystem, true);
    }

    private void SetEnableThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
            ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
            emissionModule.enabled = enabled;
    }
}
