using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour
{
    new private ParticleSystem particleSystem;
	
	public void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
	}
	
	public void Update()
    {
        particleSystem.Simulate(Time.unscaledDeltaTime, true, false);

        if (particleSystem.time >= particleSystem.main.duration)
            Destroy(gameObject);
	}
}
