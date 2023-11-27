using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject particleEffect;

    [System.Obsolete]
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.HSVToRGB(0.3f, 0.9f, Random.RandomRange(0.4f, 1));

        var particleSystem = particleEffect.GetComponent<ParticleSystem>();
        var shape = particleSystem.shape;
        shape.box = new Vector3(transform.localScale.x, transform.localScale.z);
    }
}
