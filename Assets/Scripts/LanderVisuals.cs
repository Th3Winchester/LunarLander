using UnityEngine;
using System;

public class LanderVisuals : MonoBehaviour
{
        [SerializeField] private ParticleSystem ThrusterParticleSystem;
        
        private Lander lander;          

        private void Awake()
        {
                lander = GetComponent<Lander>();

                lander.OnUpForce += Lander_OnUpForce;
                lander.OnBeforeForce += Lander_OnBeforeForce;

                SetEnableThrusterParticleSystem(ThrusterParticleSystem, false);

        }

        private void Lander_OnBeforeForce(object sender, EventArgs e)
        {
                SetEnableThrusterParticleSystem(ThrusterParticleSystem, false);
        }

        private void Lander_OnUpForce(object sender, EventArgs e)
        {
                SetEnableThrusterParticleSystem(ThrusterParticleSystem, true);
        }

        private void SetEnableThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
        {
                ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
                emissionModule.enabled = enabled;
        }
}
