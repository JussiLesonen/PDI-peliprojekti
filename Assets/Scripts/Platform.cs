using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject particleEffect;

    [System.Obsolete]
    void Start()
    {
        #region Static platform colors
        if (transform.parent.name == "Level 1")
        {
            GetComponent<Renderer>().material.color = Color.HSVToRGB(0.3f, 0.9f, Random.RandomRange(0.4f, 1));
        }
        else if (transform.parent.name == "Level 2")
        {
            GetComponent<Renderer>().material.color = Color.HSVToRGB(0.6f, 0.9f, Random.RandomRange(0.4f, 1));

            //particleEffect.GetComponent<ParticleSystem>().startColor = new Color();
        }
        #endregion

        #region Moving platforms colors
        if (transform.parent.transform.parent != null)
        {
            if (transform.parent.transform.parent.name == "Level 1")
            {
                GetComponent<Renderer>().material.color = Color.HSVToRGB(0.3f, 0.9f, Random.RandomRange(0.4f, 1));
            }
            else if (transform.parent.transform.parent.name == "Level 2")
            {
                GetComponent<Renderer>().material.color = Color.HSVToRGB(0.6f, 0.9f, Random.RandomRange(0.4f, 1));

                particleEffect.GetComponent<ParticleSystem>().startColor = Color.blue;
            }
        }
        #endregion

        var particleSystem = particleEffect.GetComponent<ParticleSystem>();
        var shape = particleSystem.shape;
        shape.box = new Vector3(transform.localScale.x, transform.localScale.z);
    }
}
