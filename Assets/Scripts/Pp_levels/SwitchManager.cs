using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour {

    public GameObject switchPrefab;
    public int quantSwitch;
    public AudioClip au_right;
    public AudioClip au_wrong;

    private PpChooseLetter levelManager;
    private SwitchMachine[] switches;
    private AskPanel askPanel;

    private AudioSource audio;
    
    private float distance;
    private bool rightAnswer;

    private List<string> strs;
    
    public PpChooseLetter LevelManager { set { levelManager = value; } }
    public AskPanel AskPanel { get { return askPanel; } }
    
    // Use this for initialization
    void Start () {
        
        askPanel = transform.FindChild("slot").GetComponent<AskPanel>();
        askPanel.initPanel();

        audio = GetComponent<AudioSource>();

        //Calculus the distance between switches
        SpriteRenderer sr_aux = switchPrefab.GetComponent<SpriteRenderer>();
        distance = (sr_aux.sprite.rect.width * switchPrefab.transform.localScale.y / sr_aux.sprite.pixelsPerUnit) * 1.3f;

        switches = new SwitchMachine[5];
        switches[0] = (Instantiate(switchPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<SwitchMachine>();
        switches[1] = (Instantiate(switchPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<SwitchMachine>();
        switches[2] = (Instantiate(switchPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<SwitchMachine>();
        switches[3] = (Instantiate(switchPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<SwitchMachine>();
        switches[4] = (Instantiate(switchPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<SwitchMachine>();

        foreach(SwitchMachine sm in switches) {
            sm.transform.parent = this.transform;
            sm.transform.localPosition = Vector3.zero;
            sm.gameObject.SetActive(false);
        }
        
        initSwitches();
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void initSwitches() {

        switches[0].transform.localPosition = Vector3.zero;
        switches[1].transform.localPosition = Vector3.zero;
        switches[2].transform.localPosition = Vector3.zero;
        switches[3].transform.localPosition = Vector3.zero;
        switches[4].transform.localPosition = Vector3.zero;

        if (quantSwitch == 2) {
            switches[0].gameObject.SetActive(true);
            switches[0].transform.Translate(-distance / 2, 0, 0);
            switches[1].gameObject.SetActive(true);
            switches[1].transform.Translate(distance / 2, 0, 0);

        } else if (quantSwitch == 3) {
            switches[0].gameObject.SetActive(true);
            switches[0].transform.Translate(-distance, 0, 0);
            switches[1].gameObject.SetActive(true);
            switches[1].transform.Translate(0, 0, 0);
            switches[2].gameObject.SetActive(true);
            switches[2].transform.Translate(distance, 0, 0);

        } else if (quantSwitch == 4) {
            switches[0].gameObject.SetActive(true);
            switches[0].transform.Translate(-distance * 3/2, 0, 0);
            switches[1].gameObject.SetActive(true);
            switches[1].transform.Translate(-distance/2, 0, 0);
            switches[2].gameObject.SetActive(true);
            switches[2].transform.Translate(distance/2, 0, 0);
            switches[3].gameObject.SetActive(true);
            switches[3].transform.Translate(distance * 3/2, 0, 0);

        } else if (quantSwitch == 5) {
            switches[0].gameObject.SetActive(true);
            switches[0].transform.Translate(-distance*2, 0, 0);
            switches[1].gameObject.SetActive(true);
            switches[1].transform.Translate(-distance, 0, 0);
            switches[2].gameObject.SetActive(true);
            switches[2].transform.Translate(0, 0, 0);
            switches[3].gameObject.SetActive(true);
            switches[3].transform.Translate(distance, 0, 0);
            switches[4].gameObject.SetActive(true);
            switches[4].transform.Translate(distance*2, 0, 0);
        }

        if(levelManager.ResultRightWrong[0] + levelManager.ResultRightWrong[1] == 0) {
            PpSwitchPlayer p = levelManager.player;
            if (quantSwitch % 2 == 1) {
                p.transform.position = new Vector3(0, p.transform.position.y, p.transform.position.z);
                p.initMe(distance, quantSwitch, quantSwitch / 2);
            }
            else {
                p.transform.position = new Vector3(-distance / 2, p.transform.position.y, p.transform.position.z);
                p.initMe(distance, quantSwitch, (quantSwitch / 2) - 1);
            }
        }
        
        int rand = Random.Range(0, quantSwitch);
        strs = GameSettings.getUpLetters(quantSwitch);
        bool hasLetter = strs.Contains(GameSettings.enum2letter(askPanel.letter));
        
        for (int i = 0; i < quantSwitch; i++) {
            if (i == rand && !hasLetter) {
                switches[i].Letter = askPanel.letter;
            } else {
                if(strs[0].Equals(askPanel.letter))
                    strs.RemoveAt(0);
                switches[i].Letter = GameSettings.letter2enum(strs[0]);
                strs.RemoveAt(0);
            }
            switches[i].initMe();
        }
    }

    public void newAsk() {
        askPanel.initPanel();
        initSwitches();
    }

    public void animaResp(bool isRight) {

        if (isRight)
            audio.clip = au_right;
        else
            audio.clip = au_wrong;
        audio.Play();

        askPanel.initAnswAnim(isRight);
    }
    
}
