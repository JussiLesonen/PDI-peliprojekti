using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject particleEffect;

    [System.Obsolete]
    void Start()
    {
        var green = Color.HSVToRGB(0.3f, 0.5f, Random.RandomRange(0.4f, 1));
        var blue = Color.HSVToRGB(0.6f, 0.5f, Random.RandomRange(0.4f, 1));
        var red = Color.HSVToRGB(1f, 0.5f, Random.RandomRange(0.4f, 1));
        var yellow = Color.HSVToRGB(0.2f, 0.5f, Random.RandomRange(0.4f, 1));

        var randomRotation = Random.RandomRange(1, 5);

        if (transform.Find("Platform") != null)
        {
            if (randomRotation == 1)
            {
                transform.Find("Platform").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (randomRotation == 2)
            {
                transform.Find("Platform").transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (randomRotation == 3)
            {
                transform.Find("Platform").transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.Find("Platform").transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
        }

        if (transform.parent != null)
        {
            #region Static platform colors
            if (transform.parent.name == "Level 1")
            {
                GetComponent<Renderer>().material.color = green;
                transform.Find("Platform").GetComponent<Renderer>().material.color = green;

                particleEffect.GetComponent<ParticleSystem>().startColor = green;
            }
            else if (transform.parent.name == "Level 2")
            {
                GetComponent<Renderer>().material.color = blue;
                transform.Find("Platform").GetComponent<Renderer>().material.color = blue;

                particleEffect.GetComponent<ParticleSystem>().startColor = blue;
            }
            else if (transform.parent.name == "Level 3")
            {
                GetComponent<Renderer>().material.color = yellow;
                transform.Find("Platform").GetComponent<Renderer>().material.color = yellow;

                particleEffect.GetComponent<ParticleSystem>().startColor = yellow;
            }
            else if (transform.parent.name == "Level 4")
            {
                GetComponent<Renderer>().material.color = red;

                particleEffect.GetComponent<ParticleSystem>().startColor = red;
            }
            #endregion

            #region Moving platforms colors
            if (transform.parent.transform.parent != null)
            {
                if (transform.parent.transform.parent.name == "Level 1")
                {
                    GetComponent<Renderer>().material.color = green;
                    transform.Find("Platform").GetComponent<Renderer>().material.color = green;

                    particleEffect.GetComponent<ParticleSystem>().startColor = green;
                }
                else if (transform.parent.transform.parent.name == "Level 2")
                {
                    GetComponent<Renderer>().material.color = blue;
                    transform.Find("Platform").GetComponent<Renderer>().material.color = blue;

                    particleEffect.GetComponent<ParticleSystem>().startColor = blue;
                }
                else if (transform.parent.transform.parent.name == "Level 3")
                {
                    GetComponent<Renderer>().material.color = yellow;
                    transform.Find("Platform").GetComponent<Renderer>().material.color = yellow;

                    particleEffect.GetComponent<ParticleSystem>().startColor = yellow;
                }
                else if (transform.parent.transform.parent.name == "Level 4")
                {
                    GetComponent<Renderer>().material.color = red;
                    transform.Find("Platform").GetComponent<Renderer>().material.color = red;

                    particleEffect.GetComponent<ParticleSystem>().startColor = red;
                }
            }
            #endregion
        }

        var particleSystem = particleEffect.GetComponent<ParticleSystem>();
        var shape = particleSystem.shape;
        shape.box = new Vector3(transform.localScale.x, transform.localScale.z);
    }
}
