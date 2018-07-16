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

    private Text countDown;

    private TorchSwitch torchSwitch;

    private TimedTorchGoal goal;
    private bool goalReached = false;

	// Use this for initialization
	void Start ()
    {
        timer = onTime;
        audioTimer = audioTimeBetweenTicks;

        torchSwitch = GetComponent<TorchSwitch>();

        countDown = GameObject.FindWithTag("CountdownTimer").GetComponent<Text>();
        countDown.text = "";

        goal = transform.GetChild(3).GetComponent<TimedTorchGoal>();

        torchSwitch.Off();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (goal.GetGoalReached())
        {
            goalReached = true;
            countDown.text = "";
        }
        else goalReached = false;

        if (torchSwitch.GetTorchState()) startTimer = true;


        if (startTimer == true && goalReached == false)
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
                countDown.text = "";
            }
        }
	}


    public bool GetActivated()
    {
        return startTimer;
    }

    public bool GetGoalReached()
    {
        return goalReached;
    }
}
