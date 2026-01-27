using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light flickeringLight;
    [SerializeField] float minIntensity = 0.5f;
    [SerializeField] float maxIntensity = 1.0f;
    [SerializeField] float flickerSpeed = 5.0f;

    float randomOffset;

    void Start()
    {
        // Initialize random offset for flickering
        randomOffset = Random.Range(0.0f, 100.0f);
        flickeringLight = GetComponent<Light>();
    }

    void Update()
    {
        // Calculate flickering effect using Perlin noise
        float flicker = Mathf.PerlinNoise(Time.time * flickerSpeed + randomOffset, 0.0f);

        // Map the Perlin noise value to the desired intensity range (line formula)
        float intensity = (maxIntensity - minIntensity) * flicker + minIntensity;

        // Apply the flickering intensity to the light
        flickeringLight.intensity = intensity;
    }
}
