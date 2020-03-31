using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PpSwitchPlayer : MonoBehaviour {

    public AudioClip changeSwitch;
    public AudioClip chooseSwitch;
    
    private PpChooseLetter levelManager;
    private float dashDistance;
    private int totalSwitches;
    private int currentSwitch;

    private string left = "Left";
    private string right = "Right";
    private string[] action = { "Fire1", "Jump" };
    private EnumsGame.LETTERS letterSwitch;
    private bool testOnTriggerStay = true;

    private AudioSource audio;

    private bool stopAsk = false;

    public bool StopAsk { get { return stopAsk; } set { stopAsk = value; } }
    public bool TestOnTriggerStay { get { return testOnTriggerStay; } set { testOnTriggerStay = value; } }


    public PpChooseLetter LevelManager { set { levelManager = value; } }
    
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!stopAsk)
            verifyKeyInput();
	}

    private void verifyKeyInput() {
        int moviment = 0;

        if (PpChooseLetter.instance.TimeToResp <= 0.2f) {
            if (Input.GetButtonDown(left) && currentSwitch > 0)
                moviment = -1;
            if (Input.GetButtonDown(right) && currentSwitch < totalSwitches - 1)
                moviment = 1;
        }

        if (moviment != 0) {
            transform.Translate(dashDistance * moviment, 0, 0);
            currentSwitch += moviment;
            audio.clip = changeSwitch;
            audio.Play();
        }

        if (Input.GetButtonUp(action[0]) || Input.GetButtonUp(action[1])) {
            //Debug.Log(letterSwitch);
            PpChooseLetter.instance.putAnswer(letterSwitch);
        }
        
    }

    public void initMe(float dashDistance, int totalSwitches, int currentSwitch) {
        this.dashDistance = dashDistance;
        this.totalSwitches = totalSwitches;
        this.currentSwitch = currentSwitch;
    }

    void OnTriggerEnter2D(Collider2D col) {
        SwitchMachine sm = col.GetComponent<SwitchMachine>();
        if(sm != null) {
            letterSwitch = sm.Letter;
            testOnTriggerStay = false;
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (!testOnTriggerStay)
            return;
        SwitchMachine sm = col.GetComponent<SwitchMachine>();
        if(sm != null) {
            letterSwitch = sm.Letter;
            testOnTriggerStay = false;
        }
    }
    
}
