using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton that manages the height of the waves in the soup
public class WaveManager : MonoBehaviour
{
    [Tooltip("Speed at which the noise moves")]
    public float speed = 1f;
    [Tooltip("Scale of the noise. A smaller value is kind of a zoom on the perlin texture")]
    public float scale = 1f;
    [Tooltip("Maximal height increment")]
    public float maxHeight = 1f;

    public static WaveManager instance;
    private float offset;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveLength(float x, float y)
    {
        // samples perlin noise at coordinates, depending on the time
        return Mathf.PerlinNoise(offset + x / scale, offset + y / scale) * maxHeight;
    }
}
