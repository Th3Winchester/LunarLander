using UnityEngine;
using System;

public class LanderVisuals : MonoBehaviour
{
        [SerializeField] private ParticleSystem MiddleThrusterParticleSystem;
        [SerializeField] private ParticleSystem LeftThrusterParticleSystem;
        [SerializeField] private ParticleSystem RightThrusterParticleSystem;
        
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
