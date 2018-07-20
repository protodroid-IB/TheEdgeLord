using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFace : MonoBehaviour
{
    [SerializeField]
    private LightCanonLightFace lightCanonLightFace;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightBullet"))
        {
            lightCanonLightFace.HitFace(gameObject, other.gameObject);

        }
    }
}
