using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCollisions : MonoBehaviour
{
    private TorchColor torchColorScript;
    private TorchSwitch torchSwitchScript;

	// Use this for initialization
	void Start ()
    {
        torchColorScript = transform.parent.GetComponent<TorchColor>();
        torchSwitchScript = transform.parent.GetComponent<TorchSwitch>();
	}


    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.CompareTag("LightBullet"))
        {

            if (torchSwitchScript.GetTorchState() == false)
            {
                torchSwitchScript.On();
                torchColorScript.ChangeColorState(collider.gameObject.name);
            }

            else
            {
                torchColorScript.ChangeColorState(collider.gameObject.name);
            }
        }
    }
}
