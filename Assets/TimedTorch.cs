using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedTorch : MonoBehaviour
{
    [SerializeField]
    private float onTime = 30f;

    private float timer;


    [SerializeField]
    private AudioSource audioTick;

    private float audioTimeBetweenTicks = 1f;
    private float audioTimer;

    [SerializeField]
    private bool startTimer = false;


    [SerializeField]
    private Text countDown;

    private TorchSwitch torchSwitch;

	// Use this for initialization
	void Start ()
    {
        timer = onTime;
        audioTimer = audioTimeBetweenTicks;

        torchSwitch = GetComponent<TorchSwitch>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (torchSwitch.GetTorchState()) startTimer = true;


        if (startTimer == true)
        {
            timer -= Time.deltaTime;
            audioTimer -= Time.deltaTime;

            countDown.text = ((int)(timer + 1f)).ToString();

            if(audioTimer <= 0f)
            {
                audioTick.Play();
                float audioSpeedRatio = timer / onTime;
                if (audioSpeedRatio <= 0.15f) audioSpeedRatio = 0.15f;
                audioTimer = audioTimeBetweenTicks * audioSpeedRatio;
            }

            if (timer <= 0f)
            {
                startTimer = false;
                torchSwitch.Off();
                audioTimer = audioTimeBetweenTicks;
                timer = onTime;
            }
        }
	}


    public bool GetActivated()
    {
        return startTimer;
    }
}
