using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitBlockColorCollisions : MonoBehaviour
{
    private BlockLightColor blockLightColor;

	// Use this for initialization
	void Start ()
    {
        blockLightColor = transform.parent.GetComponent<BlockLightColor>();
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("LightBullet"))
        {
            blockLightColor.ChangeColorState(collider.gameObject.name);
        }
    }
}
